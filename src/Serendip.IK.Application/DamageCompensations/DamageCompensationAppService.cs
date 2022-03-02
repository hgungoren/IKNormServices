using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Newtonsoft.Json;
using Refit;
using Serendip.IK.DamageCompensations.Dto;
using Serendip.IK.DamageCompensationsEvalutaion;
using Serendip.IK.DamageCompensationsEvalutaion.Dto;
using Serendip.IK.DamageCompensationsFileInfo;
using Serendip.IK.DamageCompensationsFileInfo.Dto;
using Serendip.IK.Ops.Nodes;
using Serendip.IK.Ops.OpsHistories;
using Serendip.IK.Ops.OpsHistories.Dto;
using Serendip.IK.Users;
using Serendip.IK.Utility;
using SuratKargo.Core.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Serendip.IK.DamageCompensations
{


    public class DamageCompensationAppService : AsyncCrudAppService<
    DamageCompensation,
    DamageCompensationDto,
    long,
    PagedDamageCompensationResultRequestDto,
    CreateDamageCompensationDto,
    DamageCompensationDto
    >, IDamageCompensationAppService
    {

        #region Constructor

        private const string SERENDIP_SERVICE_BASE_URL = ApiConsts.K_KARGO_API_URL;
        private const string SERENDIP_K_KCARI_API_URL = ApiConsts.K_CARI_API_URL;

        private const string SERENDIP_K_BIRIM_API_URL = ApiConsts.K_BIRIM_API_URL;
        private const string SERENDIP_K_KSUBE_API_URL = ApiConsts.K_KSUBE_API_URL;

        private IUserAppService _userAppService;
        private IDamageCompensationFileInfoAppService _damageCompensationFileInfoAppService;
        private IOpsHistoryAppService _opsHistoryAppService;
        private readonly IAbpSession _abpSession;
        private IOpsNodeAppService _opsNodeAppService;
        private IDamageCompensationEvalutaionAppService _damageCompensationEvalutaionAppService;

        #endregion

        public DamageCompensationAppService(IRepository<DamageCompensation, long> repository,
             IUserAppService userAppService,
             IAbpSession abpSession,
             IDamageCompensationFileInfoAppService damageCompensationFileInfoAppService,
             IOpsHistoryAppService opsHistoryAppService,
             IOpsNodeAppService opsNodeAppService,
             IDamageCompensationEvalutaionAppService damageCompensationEvalutaionAppService

            ) : base(repository)
        {
            _userAppService = userAppService;
            _abpSession = abpSession;
            _opsHistoryAppService = opsHistoryAppService;
            _damageCompensationFileInfoAppService = damageCompensationFileInfoAppService;
            _opsNodeAppService = opsNodeAppService;
            _damageCompensationEvalutaionAppService = damageCompensationEvalutaionAppService;

        }

        public override async Task<DamageCompensationDto> CreateAsync(CreateDamageCompensationDto input)
        {

            long userId = _abpSession.GetUserId();
            var user = await _userAppService.GetAsync(new EntityDto<long> { Id = userId });

            //eger oluşturulan tazmin kendi oluşturan kişinin kenddi bölgesinde ise tazmin oluşturuldu
            //eger oluşturulan tazmin oluşturan kişinin bölgesinde degilde sürec sahibi bölgesinden farklı gelirse bölge işelmde

            input.DurumUnvan = user.Title;

            if (input.FileTazminDilekcesi == "[]" || input.FileTazminDilekcesi == null)
            {


                var result = ObjectMapper.Map<DamageCompensation>(input);
                var data = ObjectMapper.Map<CreateDamageCompensationDto>(result);
                var createadata = await base.CreateAsync(data);
                long id = Repository.GetAll().Max(x => x.Id);

                UpdataNextStatus dto = new UpdataNextStatus();
                dto.TazminId = id;
                await UpdateDamageStatus(dto);


                createadata = null;
                Thread.Sleep(500);
                FileDbInsert(input, id);
                return createadata;
            }
            else
            {

                if (Convert.ToString(user.CompanyRelationObjId) != input.Surec_Sahibi_Birim_Bolge.Split('-')[0])
                {
                    input.TazminStatu = 4;

                    var result = ObjectMapper.Map<DamageCompensation>(input);
                    var data = ObjectMapper.Map<CreateDamageCompensationDto>(result);
                    var createadata = await base.CreateAsync(data);
                    long id = Repository.GetAll().Max(x => x.Id);
                    Thread.Sleep(500);
                    FileDbInsert(input, id);
                    createadata = null;
                    return createadata;

                }
                else
                {


                    var result = ObjectMapper.Map<DamageCompensation>(input);
                    var data = ObjectMapper.Map<CreateDamageCompensationDto>(result);
                    var createadata = await base.CreateAsync(data);

                    long id = Repository.GetAll().Max(x => x.Id);
                    UpdataNextStatus dto = new UpdataNextStatus();
                    dto.TazminId = id;
                    await UpdateDamageStatus(dto);

                    Thread.Sleep(500);
                    FileDbInsert(input, id);

                    createadata = null;
                    return createadata;

                }
            }

        }


        private void LogHistories(int tazminStatu, string ekleyen, long tazminid)
        {

            OpsHistoryCreateInput opsHistoryCreateInput = new OpsHistoryCreateInput();
            opsHistoryCreateInput.Islem = "Tazmin Bilgileri";
            opsHistoryCreateInput.Islemyapankullanici = ekleyen;
            opsHistoryCreateInput.TazminStatu = Enum.GetName(typeof(TazminStatu), tazminStatu);
            opsHistoryCreateInput.Odemedurumu = "Hayır";
            opsHistoryCreateInput.TazminId = tazminid;





            _opsHistoryAppService.CreateAsync(opsHistoryCreateInput);

        }



        private void FileDbInsert(CreateDamageCompensationDto input, long id)
        {
            //dosya kontrolleri db kaydet
            if (input.FileTazminDilekcesi != "[]" && input.FileTazminDilekcesi != null)
            {
                //json cevir database kaydet
                List<Dto.FileBase64> filestazmindilekce = new List<Dto.FileBase64>();
                filestazmindilekce = JsonConvert.DeserializeObject<List<Dto.FileBase64>>(input.FileTazminDilekcesi);
                for (int i = 0; i < filestazmindilekce.Count; i++)
                {
                    CreateDamageCompensationFileInfoDto createDamageCompensationFileInfoDto = new CreateDamageCompensationFileInfoDto();
                    string[] name = filestazmindilekce[i].name.Split('.');
                    string fileName = $"{name[0]}-{Guid.NewGuid().ToString("N")}";
                    createDamageCompensationFileInfoDto.DosyaAdi = fileName;
                    createDamageCompensationFileInfoDto.DosyaUzantisi = filestazmindilekce[i].type;
                    createDamageCompensationFileInfoDto.DosyaYolu = $@"/HasarTazmin/{fileName}.{name[1]}";
                    createDamageCompensationFileInfoDto.DamageCompensationId = Convert.ToInt32(id);
                    createDamageCompensationFileInfoDto.DosyaTyp = 1;
                    createDamageCompensationFileInfoDto.DosyaActive = true;
                    _damageCompensationFileInfoAppService.CreateAsync(createDamageCompensationFileInfoDto);
                    Thread.Sleep(500);
                    UploadFile(filestazmindilekce[i].base64, $"{fileName}.{name[1]}");

                }
            }
        }



        private void FileDbInsertTwo(string input, long id)
        {
            //dosya kontrolleri db kaydet
            if (input != "[]" && input != null)
            {
                //json cevir database kaydet
                List<Dto.FileBase64> filestazmindilekce = new List<Dto.FileBase64>();
                filestazmindilekce = JsonConvert.DeserializeObject<List<Dto.FileBase64>>(input);

                for (int i = 0; i < filestazmindilekce.Count; i++)
                {
                    CreateDamageCompensationFileInfoDto createDamageCompensationFileInfoDto = new CreateDamageCompensationFileInfoDto();
                    string[] name = filestazmindilekce[i].name.Split('.');
                    string fileName = $"{name[0]}-{Guid.NewGuid().ToString("N")}";

                    createDamageCompensationFileInfoDto.DosyaAdi = fileName;
                    createDamageCompensationFileInfoDto.DosyaUzantisi = filestazmindilekce[i].type;
                    createDamageCompensationFileInfoDto.DosyaYolu = $@"/HasarTazmin/{fileName}.{name[1]}";
                    createDamageCompensationFileInfoDto.DamageCompensationId = Convert.ToInt32(id);
                    createDamageCompensationFileInfoDto.DosyaTyp = 1;
                    createDamageCompensationFileInfoDto.DosyaActive = true;
                    _damageCompensationFileInfoAppService.CreateAsync(createDamageCompensationFileInfoDto);
                    Thread.Sleep(500);
                    UploadFile(filestazmindilekce[i].base64, $"{fileName}.{name[1]}");

                }
            }
        }

        private void UploadFile(string base64Encode, string filename)
        {

            int start = base64Encode.IndexOf("4,") + 2;
            int finsh = base64Encode.Length - start;
            string rep = base64Encode.Substring(start, finsh);
            byte[] bytes = Convert.FromBase64String(rep);
            string fullOutputPath = Directory.GetCurrentDirectory() + @"\wwwroot\DamageFiles\";
            System.IO.File.WriteAllBytes(fullOutputPath + "" + filename + "", Convert.FromBase64String(rep));

        }


        public override async Task<DamageCompensationDto> UpdateAsync(DamageCompensationDto input)
        {
            try
            {
                input.CreatorUserId = input.CreatorUserId;
                return await base.UpdateAsync(input);
            }
            catch (Exception e)
            {
                throw;
            }
        }


        public async Task<string> GetUpdateFileAfter(long id)
        {
            // file servisinde dosya yükleme işleminden sonra bu servise gelip bu metot çalıştırılıp tazmnin statüsü değiştirilecektir.
            long userId = _abpSession.GetUserId();
            var user = await _userAppService.GetAsync(new EntityDto<long> { Id = userId });
            //tazmini bulup tazmin stattüsünü bölge işlemde veyaz tazmin statü oalrak değiştirmemiz lazım 
            var datatazmin = await base.GetAsync(new EntityDto<long> { Id = id });

            if (Convert.ToString(user.CompanyRelationObjId) == datatazmin.Surec_Sahibi_Birim_Bolge.Split('-')[0])
            {
                UpdataNextStatus dto = new UpdataNextStatus();
                dto.TazminId = id;
                await UpdateDamageStatus(dto);
                //tazmin olşturuldu
                //datatazmin.TazminStatu = 3;
            }
            else
            {

                UpdataNextStatus dto = new UpdataNextStatus();
                dto.TazminId = id;
                await UpdateDamageStatus(dto);
                //bolge işlemde
                // datatazmin.TazminStatu = 4;
            }
            await base.UpdateAsync(datatazmin);



            return "";
        }


        public async Task<DamageCompensationDto> GetById(long id)
        {
            var dataall = Repository.GetAll().Where(x => x.TakipNo == id).FirstOrDefault();
            if (dataall != null)
            {
                DamageCompensationDto dto = new DamageCompensationDto();
                dto.TakipNo = "0";
                dto.GonderenKodu = Convert.ToString(dataall.Id);
                return dto;
            }
            else
            {
                try
                {
                    var service = RestService.For<IDamageCompensationApi>(SERENDIP_SERVICE_BASE_URL);
                    var data = await service.GetDamageCompensations(id);
                    if (data != null)
                    {
                        return data;
                    }
                    else
                    {
                        return null;
                    }
                }
                catch (Exception)
                {

                    return null;
                }


            }
        }


        public async Task<List<DamageCompensationGetCariListDto>> GetCariListAsynDamage(string id)
        {
            var service = RestService.For<IDamageCompensationApi>(SERENDIP_K_KCARI_API_URL);
            var data = await service.GetCariListAsynDamage(id);
            return data;
        }



        public async Task<List<DamageCompensationGetBirimListDto>> GetBirimListAsynDamage()
        {
            var service = RestService.For<IDamageCompensationApi>(SERENDIP_K_BIRIM_API_URL);

            var data = await service.GetAllAsync();
            return data;
        }

        public async Task<List<DamageCompensationGetBranchsListDto>> GetBranchsListDamage()
        {
            var service = RestService.For<IDamageCompensationApi>(SERENDIP_K_KSUBE_API_URL);
            var data = await service.GetKSubeListDamageAll();
            return data;
        }
        public async Task<List<DamageCompensationGetBranchsListDto>> GetAreaListDamage()
        {
            var service = RestService.For<IDamageCompensationApi>(SERENDIP_K_KSUBE_API_URL);
            var data = await service.GetKBolgeListDamageAll();
            return data;
        }


        public override async Task<PagedResultDto<DamageCompensationDto>> GetAllAsync(PagedDamageCompensationResultRequestDto input)
        {
            var data = await base.GetAllAsync(input);
            return data;
        }

        public async Task<int> GetDamageLastId()
        {
            try
            {
                var data = Repository.GetAll();
                if (data.Count() == 0)
                {
                    return 1;
                }

                long count = Repository.GetAll().Max(x => x.Id);
                return Convert.ToInt32(count) + Convert.ToInt32(1);
            }
            catch (Exception)
            {
                return 0;
            }
        }


        public async Task<List<GetDamageCompensationAllList>> GetAllDamageCompensation()
        {
            var serviceBolge = RestService.For<IDamageCompensationApi>(SERENDIP_K_KSUBE_API_URL);
            List<GetDamageCompensationAllList> list = new List<GetDamageCompensationAllList>();

            var data = await Repository.GetAllListAsync();
            //var data2 = data.Where(x => x.Surec_Sahibi_Birim_Bolge.Split('-')[0] == "3120000100000000001").ToList();
            List<DamageCompensationGetBranchsListDto> bolgelist = await serviceBolge.GetKBolgeListDamageAll();

            long userId = _abpSession.GetUserId();
            var user = await _userAppService.GetAsync(new EntityDto<long> { Id = userId });


            foreach (var item in data)
            {
                GetDamageCompensationAllList all = new GetDamageCompensationAllList();
                all.TazminNo = item.Id; //ok
                all.TakipNo = item.TakipNo;
                int tazmintipi = Convert.ToInt32(item.Tazmin_Tipi);
                all.TazminTipi = Enum.GetName(typeof(SuratKargo.Core.Enums.TazminTipi), tazmintipi);//ok 
                all.TazminStatusu = Enum.GetName(typeof(SuratKargo.Core.Enums.TazminStatu), item.TazminStatu);//ok
                all.TazminTarihi = item.CreationTime.ToString("yyyy-MM-dd"); //ok

                if (item.Surec_Sahibi_Birim_Bolge == null)
                {
                    all.SurecSahibiBolge = "";//ok
                }
                else
                {
                    string[] itembolgeObjId = item.Surec_Sahibi_Birim_Bolge.Split('-');
                    DamageCompensationGetBranchsListDto bolgeAdi = bolgelist.Where(x => x.ObjId == itembolgeObjId[0]).FirstOrDefault();
                    if (bolgeAdi == null)
                    {
                        all.SurecSahibiBolge = "";//ok
                    }
                    else
                    {
                        all.SurecSahibiBolge = bolgeAdi.Adi;//ok
                    }
                }

                if (item.CreatorUserId != null)
                {
                    var createuser = await _userAppService.GetAsync(new EntityDto<long> { Id = Convert.ToInt64(item.CreatorUserId) });
                    all.EklyenKullanici = createuser.FullName;//ok
                }
                else
                {
                    var edituser = await _userAppService.GetAsync(new EntityDto<long> { Id = Convert.ToInt64(item.LastModifierUserId) });
                    all.EklyenKullanici = edituser.FullName;//ok
                }

                int opsnoderole = await _opsNodeAppService.GetOpsNodesCode();

                // düzenleme btn
                // eğer tazmin eksik evrak statüsündeyse  ve ve kendi oluşturmuşşa aynı bölgede ise

                if (user.CompanyRelationObjId.ToString() == item.Surec_Sahibi_Birim_Bolge.Split('-')[0].ToString()
                    &&
                    item.TazminStatu == TazminStatu.TazminEksikEvrak
                    )
                {
                    all.BtnDuzenle = true;
                }
                else { all.BtnDuzenle = false; }


                //değerlendir btn 
                // opsnode code 0 ve 1 olmaması lazım  kendi bölgesinde olması lazım 

                if (user.CompanyRelationObjId.ToString() == item.Surec_Sahibi_Birim_Bolge.Split('-')[0].ToString())
                {
                    all.BtnDegerlendir = true;
                }
                else { all.BtnDegerlendir = false; }



                all.BtnGoruntule = true;


                all.KargoKabukFisNo = item.KargoKabulFisNo;


                all.WebSiparisKodu = item.Web_Siparis_Kodu;






                list.Add(all);
            }
            return list;
        }

        public async Task<DamageCompensationDto> GetDamageCompenSationById(long id)
        {
            var data = Repository.Get(id);
            var service = RestService.For<IDamageCompensationApi>(SERENDIP_K_KSUBE_API_URL);

            var dataBolge = await service.GetKBolgeListDamageAll();

            string odemetext = "";
            string surecsahibitxt = "";
            string odemeLong = "0";
            string surecsahibiLong = "0";
            if (data.Odeme_Birimi_Bolge != null)
            {
                string[] parcala_Odeme_Birimi_Bolge = data.Odeme_Birimi_Bolge.Split('-');
                string a = parcala_Odeme_Birimi_Bolge[0].ToString();

                DamageCompensationGetBranchsListDto Odeme_Birimi_Bolge = dataBolge.Where(x => x.ObjId == a).FirstOrDefault();
                odemetext = Odeme_Birimi_Bolge.Adi;
                odemeLong = Odeme_Birimi_Bolge.ObjId;
            }

            if (data.Surec_Sahibi_Birim_Bolge != null)
            {
                string[] parcala_Surec_Sahibi_Birim_Bolge = data.Surec_Sahibi_Birim_Bolge.Split('-');
                string b = parcala_Surec_Sahibi_Birim_Bolge[0].ToString();
                DamageCompensationGetBranchsListDto Surec_Sahibi_Birim_Bolge = dataBolge.Where(x => x.ObjId == b).FirstOrDefault();
                surecsahibitxt = Surec_Sahibi_Birim_Bolge.Adi;
                surecsahibiLong = Surec_Sahibi_Birim_Bolge.ObjId;
            }

            // sube id bulma 
            var serviceSube = RestService.For<IDamageCompensationApi>(SERENDIP_K_KSUBE_API_URL);
            List<DamageCompensationGetBranchsListDto> datasube = await serviceSube.GetKSubeListDamageAll();


            //
            // var serviceSube = RestService.For<IDamageCompensationApi>(SERENDIP_K_KSUBE_API_URL);
            //  List<DamageCompensationGetBranchsListDto> datasube = await service.GetKSubeListDamageAll();


            DamageCompensationGetBranchsListDto ilksube = datasube.Where(x => x.ObjId == data.IlkGondericiSube_ObjId).FirstOrDefault();
            DamageCompensationGetBranchsListDto varıssube = datasube.Where(x => x.ObjId == data.VarisSube_ObjId).FirstOrDefault();

            long ilk = (ilksube == null ? 0 : Convert.ToInt64(ilksube.ObjId));
            long varis = (varıssube == null ? 0 : Convert.ToInt64(varıssube.ObjId));

            ///birimi id buılma
            var servicebirim = RestService.For<IDamageCompensationApi>(SERENDIP_K_BIRIM_API_URL);
            List<DamageCompensationGetBirimListDto> databirim = await servicebirim.GetAllAsync();

            long blong = Convert.ToInt64(data.Birimi_ObjId);
            DamageCompensationGetBirimListDto bb = databirim.Where(x => x.ObjId == blong).FirstOrDefault();

            long birim = (bb == null ? 0 : Convert.ToInt64(bb.ObjId));


            DamageCompensationDto dto = new DamageCompensationDto();
            dto.Id = data.Id;
            dto.VarisSube_ObjId = varis;
            dto.AliciKodu = data.AliciKodu;
            dto.IlkGondericiSube_ObjId = ilk;
            dto.AliciUnvan = data.AliciUnvan;
            dto.EvrakSeriNo = data.EvrakSeriNo;
            dto.GonderenKodu = data.GonderenKodu;
            dto.GonderenUnvan = data.GonderenUnvan;
            dto.TakipNo = Convert.ToString(data.TakipNo);
            dto.Sistem_InsertTime = data.Sistem_InsertTime;
            dto.Cikis_Sube_Unvan = data.Cikis_Sube_Unvan;
            dto.VarisSube_ObjId = varis;
            dto.Varis_Sube_Unvan = data.Varis_Sube_Unvan;
            dto.Birimi_ObjId = birim;
            dto.Birimi = data.Birimi;
            dto.Adet = data.Adet;
            dto.TazminStatu = (int)data.TazminStatu;
            dto.TazminStatuAd = Enum.GetName(typeof(TazminStatu), data.TazminStatu);
            dto.Tazmin_Talep_Tarihi = data.Tazmin_Talep_Tarihi;
            dto.Tazmin_Tipi = Enum.GetName(typeof(TazminTipi), data.Tazmin_Tipi);
            dto.Tazmin_Musteri_Tipi = Convert.ToInt32(data.Tazmin_Musteri_Tipi);
            dto.Tazmin_Musteri_Kodu = data.Tazmin_Musteri_Kodu;
            dto.Tazmin_Musteri_Unvan = data.Tazmin_Musteri_Unvan;
            dto.TCK_NO = data.TCK_NO;
            dto.VK_NO = data.VK_NO;
            dto.Odeme_Birimi_Bolge = odemeLong;
            dto.Odeme_Birimi_Bolge_Text = odemetext;
            dto.Talep_Edilen_Tutar = data.Talep_Edilen_Tutar;
            dto.Surec_Sahibi_Birim_Bolge = surecsahibiLong;
            dto.Surec_Sahibi_Birim_Bolge_Text = surecsahibitxt;
            dto.Telefon = data.Telefon;
            dto.Email = data.Email;
            dto.Odeme_Musteri_Tipi = Enum.GetName(typeof(OdemeMusteriTipi), data.Odeme_Musteri_Tipi);
            return dto;
        }

        //filitreleme dto.
        public async Task<List<GetDamageCompensationAllList>> GetDamageCompensationFilter(FilterDamageCompensationDto dto)
        {
            var datalist = Repository.GetAll();


            if (dto.ChecktakipNo == true)
            {
                if (dto.Start != null && dto.Finish != null)
                {
                    datalist = datalist.Where(x => x.TakipNo == dto.Search && x.Tazmin_Talep_Tarihi >= dto.Start && x.Tazmin_Talep_Tarihi <= dto.Finish);
                }
                else if (dto.Start != null && dto.Finish == null)
                {
                    datalist = datalist.Where(x => x.TakipNo == dto.Search && x.Tazmin_Talep_Tarihi >= dto.Start);
                }
                else if (dto.Start == null && dto.Finish != null)
                {
                    datalist = datalist.Where(x => x.TakipNo == dto.Search && x.Tazmin_Talep_Tarihi <= dto.Finish);
                }
                else if (dto.Start == null && dto.Finish == null)
                {
                    datalist = datalist.Where(x => x.TakipNo == dto.Search);
                }
                else
                {
                    datalist = null;
                }
            }


            if (dto.ChecktazminId == true)
            {
                if (dto.Start != null && dto.Finish != null)
                {
                    datalist = datalist.Where(x => x.Id == dto.Search && x.Tazmin_Talep_Tarihi >= dto.Start && x.Tazmin_Talep_Tarihi <= dto.Finish);
                }
                else if (dto.Start != null && dto.Finish == null)
                {
                    datalist = datalist.Where(x => x.Id == dto.Search && x.Tazmin_Talep_Tarihi >= dto.Start);
                }
                else if (dto.Start == null && dto.Finish != null)
                {
                    datalist = datalist.Where(x => x.Id == dto.Search && x.Tazmin_Talep_Tarihi <= dto.Finish);
                }
                else if (dto.Start == null && dto.Finish == null)
                {
                    datalist = datalist.Where(x => x.Id == dto.Search);
                }
                else
                {
                    datalist = null;
                }
            }


            if (dto.ChecktakipNo == false && dto.ChecktazminId == false)
            {
                if (dto.Start != null && dto.Finish != null)
                {
                    datalist = datalist.Where(x => x.Tazmin_Talep_Tarihi >= dto.Start && x.Tazmin_Talep_Tarihi <= dto.Finish);

                }
                else if (dto.Start != null && dto.Finish == null)
                {
                    datalist = datalist.Where(x => x.Tazmin_Talep_Tarihi >= dto.Start);
                }
                else if (dto.Start == null && dto.Finish != null)
                {
                    datalist = datalist.Where(x => x.Tazmin_Talep_Tarihi <= dto.Finish);
                }
                else if (dto.Start == null && dto.Finish == null)
                {
                    datalist = Repository.GetAll();
                }

            }

            var serviceBolge = RestService.For<IDamageCompensationApi>(SERENDIP_K_KSUBE_API_URL);
            List<GetDamageCompensationAllList> list = new List<GetDamageCompensationAllList>();
            List<DamageCompensationGetBranchsListDto> bolgelist = await serviceBolge.GetKBolgeListDamageAll();
            foreach (var item in datalist)
            {
                GetDamageCompensationAllList all = new GetDamageCompensationAllList();
                all.TazminNo = item.Id; //ok
                all.TakipNo = item.TakipNo;
                int tazmintipi = Convert.ToInt32(item.Tazmin_Tipi);
                all.TazminTipi = Enum.GetName(typeof(SuratKargo.Core.Enums.TazminTipi), tazmintipi);//ok
                all.TazminStatusu = Enum.GetName(typeof(SuratKargo.Core.Enums.TazminStatu), item.TazminStatu);//ok
                all.TazminTarihi = item.CreationTime.ToString("yyyy-MM-dd"); //ok

                if (item.Surec_Sahibi_Birim_Bolge == null)
                {
                    all.SurecSahibiBolge = "";//ok
                }
                else
                {
                    string[] itembolgeObjId = item.Surec_Sahibi_Birim_Bolge.Split('-');
                    DamageCompensationGetBranchsListDto bolgeAdi = bolgelist.Where(x => x.ObjId == itembolgeObjId[0]).FirstOrDefault();
                    if (bolgeAdi == null)
                    {
                        all.SurecSahibiBolge = "";//ok
                    }
                    else
                    {
                        all.SurecSahibiBolge = bolgeAdi.Adi;//ok
                    }
                }

                if (item.CreatorUserId != null)
                {
                    var createuser = await _userAppService.GetAsync(new EntityDto<long> { Id = Convert.ToInt64(item.CreatorUserId) });
                    all.EklyenKullanici = createuser.FullName;//ok
                }
                else
                {
                    var edituser = await _userAppService.GetAsync(new EntityDto<long> { Id = Convert.ToInt64(item.LastModifierUserId) });
                    all.EklyenKullanici = edituser.FullName;//ok
                }
                list.Add(all);
            }

            return list;
        }


        // view(görütüleme) method
        public async Task<ViewDto> GetViewById(long id)
        {

            ViewDto resultDto = new ViewDto();
            var data = base.Repository.Get(id);
            if (data == null)
            {
                resultDto.IsError = true;
                resultDto.Meseaj = " " + id + " nolu tazmin hasar bulunamamıştır.";
                return resultDto;
            }
            else
            {
                #region odeme bolge ve surec sahibi
                var service = RestService.For<IDamageCompensationApi>(SERENDIP_K_KSUBE_API_URL);
                var dataBolge = await service.GetKBolgeListDamageAll();

                string odemetext = "";
                string surecsahibitxt = "";
                string odemeLong = "0";
                string surecsahibiLong = "0";
                if (data.Odeme_Birimi_Bolge != null)
                {
                    string[] parcala_Odeme_Birimi_Bolge = data.Odeme_Birimi_Bolge.Split('-');
                    string a = parcala_Odeme_Birimi_Bolge[0].ToString();
                    DamageCompensationGetBranchsListDto Odeme_Birimi_Bolge = dataBolge.Where(x => x.ObjId == a).FirstOrDefault();
                    odemetext = Odeme_Birimi_Bolge.Adi;
                    odemeLong = Odeme_Birimi_Bolge.ObjId;
                }

                if (data.Surec_Sahibi_Birim_Bolge != null)
                {
                    string[] parcala_Surec_Sahibi_Birim_Bolge = data.Surec_Sahibi_Birim_Bolge.Split('-');
                    string b = parcala_Surec_Sahibi_Birim_Bolge[0].ToString();
                    DamageCompensationGetBranchsListDto Surec_Sahibi_Birim_Bolge = dataBolge.Where(x => x.ObjId == b).FirstOrDefault();
                    surecsahibitxt = Surec_Sahibi_Birim_Bolge.Adi;
                    surecsahibiLong = Surec_Sahibi_Birim_Bolge.ObjId;
                }

                #endregion

                resultDto.TakipNo = Convert.ToString(data.TakipNo);
                resultDto.Sistem_InsertTime = Convert.ToString(data.Sistem_InsertTime.ToString("dd-MM-yyyy"));
                resultDto.EvrakSeriNo = data.EvrakSeriNo;
                resultDto.GonderenKodu = data.GonderenKodu;
                resultDto.GonderenUnvan = data.GonderenUnvan;
                resultDto.AliciKodu = data.AliciKodu;
                resultDto.AliciUnvan = data.AliciUnvan;
                resultDto.IlkGondericiSube_ObjId = data.IlkGondericiSube_ObjId;
                resultDto.Cikis_Sube_Unvan = data.Cikis_Sube_Unvan;
                resultDto.VarisSube_ObjId = data.VarisSube_ObjId;
                resultDto.Varis_Sube_Unvan = data.Varis_Sube_Unvan;
                resultDto.Birimi_ObjId = data.Birimi_ObjId;
                resultDto.Birimi = data.Birimi;
                resultDto.Adet = Convert.ToString(data.Adet);
                //resultDto.TazminStatu = Convert.ToString(data.TazminStatu);
                resultDto.TazminStatu = Enum.GetName(typeof(TazminStatu), data.TazminStatu);
                resultDto.Tazmin_Talep_Tarihi = data.Tazmin_Talep_Tarihi.ToString("dd-MM-yyyy");
                resultDto.Tazmin_Tipi = Enum.GetName(typeof(TazminTipi), data.Tazmin_Tipi);
                resultDto.Tazmin_Musteri_Tipi = Enum.GetName(typeof(TazminMusteriTipi), data.Tazmin_Musteri_Tipi);
                resultDto.Tazmin_Musteri_Kodu = data.Tazmin_Musteri_Kodu;
                resultDto.Tazmin_Musteri_Unvan = data.Tazmin_Musteri_Unvan;
                resultDto.Odeme_Musteri_Tipi = Enum.GetName(typeof(OdemeMusteriTipi), data.Odeme_Musteri_Tipi);
                resultDto.TCK_NO = data.TCK_NO;
                resultDto.VK_NO = data.VK_NO;
                resultDto.Odeme_Birimi_Bolge = odemetext;
                resultDto.Talep_Edilen_Tutar = data.Talep_Edilen_Tutar;
                resultDto.Surec_Sahibi_Birim_Bolge = surecsahibitxt;
                resultDto.Telefon = data.Telefon;
                resultDto.Email = data.Email;

                if (Enum.GetName(typeof(TazminDurumu), data.Durumu) == "Biliniyor") { resultDto.Durumu = "1"; } else { resultDto.Durumu = "2"; }

                resultDto.KargoKabulFisNo = data.KargoKabulFisNo;

                return resultDto;
            }
        }


        public async Task<List<EnumViewModel>> GetEnumListById(int id)
        {
            List<EnumViewModel> ff = EnumExtensions.GetEnumList<DamageCompensationWhy>();
            if (id == 1)
            {
                var allowedStatus = new[] { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 };
                var data = ff.Where(o => allowedStatus.Contains(o.id));
                List<EnumViewModel> liste = new List<EnumViewModel>();
                foreach (var item in data)
                {
                    EnumViewModel ev = new EnumViewModel();
                    ev.id = item.id;
                    ev.name = item.name;
                    liste.Add(ev);
                }
                return liste;
            }
            else if (id == 2)
            {
                var allowedStatus = new[] { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20, 21 };
                var data = ff.Where(o => allowedStatus.Contains(o.id));
                List<EnumViewModel> liste = new List<EnumViewModel>();
                foreach (var item in data)
                {
                    EnumViewModel ev = new EnumViewModel();
                    ev.id = item.id;
                    ev.name = item.name;
                    liste.Add(ev);
                }
                return liste;
            }
            else if (id == 3)
            {
                var allowedStatus = new[] { 22 };
                var data = ff.Where(o => allowedStatus.Contains(o.id));
                List<EnumViewModel> liste = new List<EnumViewModel>();
                foreach (var item in data)
                {
                    EnumViewModel ev = new EnumViewModel();
                    ev.id = item.id;
                    ev.name = item.name;
                    liste.Add(ev);
                }
                return liste;
            }
            else if (id == 4)
            {
                var allowedStatus = new[] { 23 };
                var data = ff.Where(o => allowedStatus.Contains(o.id));
                List<EnumViewModel> liste = new List<EnumViewModel>();
                foreach (var item in data)
                {
                    EnumViewModel ev = new EnumViewModel();
                    ev.id = item.id;
                    ev.name = item.name;
                    liste.Add(ev);
                }
                return liste;
            }
            else
            {
                return null;
            }

        }




        public async Task<string> ApprovalBtn(long id)
        {
            // tazmin 
            var data = Repository.Get(id);

            //son değerlendirme
            DamageCompensaitonEvalutaionDto dataeva = await _damageCompensationEvalutaionAppService.GetLastTazminIdRow(id);

            float tutar = 0;
            if (dataeva.EvaTalep_Edilen_Tutar != null || dataeva.EvaTalep_Edilen_Tutar != "0")
            {
                tutar = Convert.ToSingle(dataeva.EvaTalep_Edilen_Tutar);
            }
            else
            {
                tutar = dataeva.EvaOdenecek_Tutar;
            }

            // user bul 
            long userId = _abpSession.GetUserId();
            var user = await _userAppService.GetAsync(new EntityDto<long> { Id = userId });
            int opsnoderole = await _opsNodeAppService.GetOpsNodesCode();


            //if(data.TazminStatu == TazminStatu.TazminOlusturuldu)
            //{
            //    // gelen tazminin statüsünü bolge operasyon müdür yardımcısı statüsüne çek
            //    data.TazminStatu = TazminStatu.OperasyonBolgeMudurYardımcısıOnayında;
            //    Repository.Update(data);
            //    return "Operasyon Bölge Müdür Yardımcısı Onayına Gönderildi.";

            //}
            //else if(data.TazminStatu == TazminStatu.BolgeIslemde)
            //{
            //    //gelen tazmini statüsünü bolge operasyon müdür yardımcısı statüsüne cek
            //    data.TazminStatu = TazminStatu.OperasyonBolgeMudurYardımcısıOnayında;
            //    Repository.Update(data);
            //    return "Operasyon Bölge Müdür Yardımcısı Onayına Gönderildi.";

            //}
            //else if(data.TazminStatu == TazminStatu.OperasyonBolgeMudurYardımcısıOnayında)
            //{
            //    //gelen tazmini bölge müdürü onayına çek 
            //    data.TazminStatu = TazminStatu.BolgeMuduruOnayında;
            //    Repository.Update(data);
            //    return "Bölge Müdür Onayına Gönderildi.";

            //}
            //else if(data.TazminStatu == TazminStatu.BolgeMuduruOnayında)
            //{
            //    // değerlendirmedeki son tutart 1000 tl altı ise tazmini kapat
            //    //eğer degilse gm işleme gönder 
            //     if(tutar < 1000)
            //    {
            //        // tazmini kapat 
            //        data.TazminStatu = TazminStatu.TazminFormuOnaylandi;
            //        Repository.Update(data);
            //        return "Tazmin Formu Onaylandı.";
            //    }
            //    else
            //    {
            //        // tazminin statüsünü gm işlemde statüsüne çek 
            //        data.TazminStatu = TazminStatu.GmIslemde;
            //        Repository.Update(data);
            //        return "Genel Merkez İşleminde.";

            //    }
            //}
            //else if (data.TazminStatu ==  TazminStatu.GmIslemde)
            //{

            //    //tazmin tipi hasar veya kayıp ise gm tazmin müdürü onayına gelcek
            //    if(data.Tazmin_Tipi ==TazminTipi.Hasar || data.Tazmin_Tipi == TazminTipi.Kayıp)
            //    {
            //        //gm tazmin müdürü onayına gelcek
            //        data.TazminStatu = TazminStatu.GmTazminMuduruOnayinda;
            //        Repository.Update(data);
            //        return "Gm Tazmin Müdürü Onayına Gönderildi.";
            //    }
            //}
            //else if(data.TazminStatu == TazminStatu.GmTazminMuduruOnayinda)
            //{
            //    // tazmin statü operayon gmy onayına gelcek
            //    data.TazminStatu = TazminStatu.OperasyonGMYOnayında;
            //    Repository.Update(data);
            //    return "Operasyon GMY Onayına Gönderildi.";
            //}
            //else if(data.TazminStatu == TazminStatu.OperasyonGMYOnayında)
            //{
            //    //tazmin tipi geç veya müşteri memnmuniyeti ise ve müşteri tipi kurumsal statü gm satış müdürü oanyına gelcek
            //    if(data.Tazmin_Tipi==TazminTipi.GecTeslimat || data.Tazmin_Tipi ==TazminTipi.MusteriMemnuniyeti && data.Odeme_Musteri_Tipi == OdemeMusteriTipi.Kurumsal)
            //    {
            //        //tazmin statünü gm satış müdürü onayına  gönder
            //        data.TazminStatu = TazminStatu.GmSatisMuduruOnayında;
            //        Repository.Update(data);
            //        return "Gm Satış Müdürü Onayına Gönderildi.";
            //    }

            //    if (data.Tazmin_Tipi == TazminTipi.GecTeslimat || data.Tazmin_Tipi == TazminTipi.MusteriMemnuniyeti && data.Odeme_Musteri_Tipi == OdemeMusteriTipi.Bireysel) 
            //    {
            //        //tazmin satatünü gm müşteri ilişkileri müdürü onayına gönder
            //        data.TazminStatu = TazminStatu.GmMusteriIliskileriMuduruOnayında;
            //        Repository.Update(data);
            //        return "Gm Müşteri İlişkileri Müdürü Onayına Gödnerildi.";
            //    }

            //}
            //else if(data.TazminStatu ==TazminStatu.GmSatisMuduruOnayında || data.TazminStatu== TazminStatu.GmMusteriIliskileriMuduruOnayında)
            //{
            //    // tazmin statütü satış gmy oanayına cek
            //    data.TazminStatu = TazminStatu.SatisGMYOnayında;
            //    Repository.Update(data);
            //    return "Satış Gmy Onayına Gödnerildi";
            //}
            //else if(data.TazminStatu == TazminStatu.SatisGMYOnayında)
            //{
            //    // tazmini kapat 
            //    data.TazminStatu = TazminStatu.TazminFormuOnaylandi;
            //    Repository.Update(data);
            //    return "Tazmin Formu Onaylandı.";
            //}

            return "false";
        }



        public async Task<string> UpdateDamageStatus(UpdataNextStatus dto)
        {
            //Surecsahibibolge= "3120000100000000009-2"
            //unvan="Bölge Operasyon Uzman Yrd."
            //tazminıd=1

            // user bul 
            long userId = _abpSession.GetUserId();
            var user = await _userAppService.GetAsync(new EntityDto<long> { Id = userId });

            // tazmin datası
            var data = Repository.Get(dto.TazminId);

            // tazmin datası cevirme 
            var datatazmin = await base.GetAsync(new EntityDto<long> { Id = dto.TazminId });

            //surec bolge değişmimi kontrol et
            bool surecsahibibolgekontrol = false;

            if (dto.Surecsahibibolge != null && dto.Surecsahibibolge != "")
            {
                //3120000100000000001 - 0
                if (data.Surec_Sahibi_Birim_Bolge.Split('-')[0] != dto.Surecsahibibolge.Split('-')[0] &&
                    dto.Surecsahibibolge != null &&
                    dto.Surecsahibibolge != "")
                {

                    /// sürec sahibi bolgeyi updae et  
                    /// 
                    datatazmin.Surec_Sahibi_Birim_Bolge = dto.Surecsahibibolge;
                    await base.UpdateAsync(datatazmin);
                    surecsahibibolgekontrol = true;
                }
            }


            //son  degerlendirme cagır 
            DamageCompensaitonEvalutaionDto evadata = await _damageCompensationEvalutaionAppService.GetLastTazminIdRow(dto.TazminId);



            // file dosyasını update yapma 
            if (dto.File != "[]" || dto.File != null)
            {
                FileDbInsertTwo(dto.File, dto.TazminId);
            }

            string yenistatu;
            int nextstatu = 99999;
            //TazminOlusturuldu
            //Bölge Operasyon Uzman Yrd.
            //next statusu 1
            if (Convert.ToString(data.TazminStatu) == "TazminOlusturuldu")
            {
                if (data.DurumUnvan == "Bölge Operasyon Uzman Yrd.")
                {
                    yenistatu = "Bölge Müdür Yrd. - Operasyon";
                    nextstatu = 1;

                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    datatazmin.TazminStatu = 5;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;

                }
                else if (data.DurumUnvan == "Bölge Operasyon Uzmanı")
                {
                    yenistatu = "Bölge Müdür Yrd. - Operasyon";
                    nextstatu = 2;

                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    datatazmin.TazminStatu = 5;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                else if (data.DurumUnvan == "Bölge Müdür Yrd. - Operasyon")
                {
                    yenistatu = "Bölge Müdürü";
                    nextstatu = 3;

                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    datatazmin.TazminStatu = 6;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                else if (data.DurumUnvan == "Bölge Müdürü")
                {
                    //ödeme tutar 1000 altında ve kusurlu birim hayır ise 
                    if (evadata.EvaOdenecek_Tutar <= 1000 && evadata.EvaKusurlu_Birim == "2" &&
                        Enum.GetName(typeof(TazminTipi), data.Tazmin_Tipi) == "1")
                    {
                        yenistatu = "Form Kapatıldı";
                        nextstatu = 44;
                        datatazmin.DurumUnvan = yenistatu;
                        datatazmin.NextStatu = nextstatu;
                        datatazmin.TazminStatu = 17;
                        await base.UpdateAsync(datatazmin);
                        return yenistatu;
                    }
                    //ödeme tutar 1000 altında ve kusurlu birim evet ise 
                    else if (evadata.EvaOdenecek_Tutar <= 1000 && evadata.EvaKusurlu_Birim == "1" &&
                       Enum.GetName(typeof(TazminTipi), data.Tazmin_Tipi) == "1" && evadata.EvaTazmin_Odeme_Durumu == "1")
                    {
                        yenistatu = "GMIslemde";
                        nextstatu = 88;

                        datatazmin.DurumUnvan = yenistatu;
                        datatazmin.NextStatu = nextstatu;
                        datatazmin.TazminStatu = 7;
                        await base.UpdateAsync(datatazmin);
                        return yenistatu;
                    }
                    //ödeme tutar 1000 üstünde ve kusurlu birim hayır ve odenmiyecek ise
                    else if (evadata.EvaOdenecek_Tutar >= 1000 && evadata.EvaKusurlu_Birim == "2" &&
                        evadata.EvaTazmin_Odeme_Durumu == "2" &&
                       Enum.GetName(typeof(TazminTipi), data.Tazmin_Tipi) == "1" &&
                       evadata.EvaTazmin_Odeme_Durumu == "2")
                    {
                        yenistatu = "Form Kapatıldı";
                        nextstatu = 44;

                        datatazmin.DurumUnvan = yenistatu;
                        datatazmin.NextStatu = nextstatu;
                        datatazmin.TazminStatu = 17;
                        await base.UpdateAsync(datatazmin);
                        return yenistatu;
                    }
                    //tazmin tipi kayıp birim  ve odenmiyecek ise
                    else if (Enum.GetName(typeof(TazminTipi), data.Tazmin_Tipi) == "2" && evadata.EvaTazmin_Odeme_Durumu == "2" && evadata.EvaKusurlu_Birim == "1")
                    {
                        yenistatu = "Form Kapatıldı";
                        nextstatu = 44;

                        datatazmin.DurumUnvan = yenistatu;
                        datatazmin.NextStatu = nextstatu;
                        datatazmin.TazminStatu = 17;
                        await base.UpdateAsync(datatazmin);
                        return yenistatu;
                    }

                    //tazmin tipi kayıp birim  ve odenmiyecek ise
                    else if (Enum.GetName(typeof(TazminTipi), data.Tazmin_Tipi) == "2" && evadata.EvaTazmin_Odeme_Durumu == "1" && evadata.EvaKusurlu_Birim == "1")
                    {
                        yenistatu = "GMIslemde";
                        nextstatu = 88;

                        datatazmin.DurumUnvan = yenistatu;
                        datatazmin.NextStatu = nextstatu;
                        datatazmin.TazminStatu = 7;
                        await base.UpdateAsync(datatazmin);
                        return yenistatu;
                    }

                    //tazmin tipi müşteri memnuniyet ve odenmiyecek ise
                    else if (Enum.GetName(typeof(TazminTipi), data.Tazmin_Tipi) == "4" && evadata.EvaTazmin_Odeme_Durumu == "2" && evadata.EvaKusurlu_Birim == "1")
                    {
                        yenistatu = "Form Kapatıldı";
                        nextstatu = 44;

                        datatazmin.DurumUnvan = yenistatu;
                        datatazmin.NextStatu = nextstatu;
                        datatazmin.TazminStatu = 17;
                        await base.UpdateAsync(datatazmin);
                        return yenistatu;
                    }
                    //tazmin tipi müşteri memnuniyet ve odenmiyecek ise
                    else if (Enum.GetName(typeof(TazminTipi), data.Tazmin_Tipi) == "4" && evadata.EvaTazmin_Odeme_Durumu == "1" && evadata.EvaKusurlu_Birim == "1")
                    {
                        yenistatu = "GMIslemde";
                        nextstatu = 88;

                        datatazmin.DurumUnvan = yenistatu;
                        datatazmin.NextStatu = nextstatu;
                        datatazmin.TazminStatu = 7;
                        await base.UpdateAsync(datatazmin);
                        return yenistatu;
                    }

                    //tazmin tipi geç teslimat ve odenmiyecek ise
                    else if (Enum.GetName(typeof(TazminTipi), data.Tazmin_Tipi) == "3" && evadata.EvaTazmin_Odeme_Durumu == "2" && evadata.EvaKusurlu_Birim == "1")
                    {
                        yenistatu = "Form Kapatıldı";
                        nextstatu = 44;

                        datatazmin.DurumUnvan = yenistatu;
                        datatazmin.NextStatu = nextstatu;
                        datatazmin.TazminStatu = 17;
                        await base.UpdateAsync(datatazmin);
                        return yenistatu;
                    }
                    else if (Enum.GetName(typeof(TazminTipi), data.Tazmin_Tipi) == "3" && evadata.EvaTazmin_Odeme_Durumu == "1" && evadata.EvaKusurlu_Birim == "1")
                    {
                        yenistatu = "GMIslemde";
                        nextstatu = 88;

                        datatazmin.DurumUnvan = yenistatu;
                        datatazmin.NextStatu = nextstatu;
                        datatazmin.TazminStatu = 7;
                        await base.UpdateAsync(datatazmin);
                        return yenistatu;
                    }
                    else
                    {
                        yenistatu = "GMIslemde";
                        nextstatu = 88;

                        datatazmin.DurumUnvan = yenistatu;
                        datatazmin.NextStatu = nextstatu;
                        datatazmin.TazminStatu = 7;
                        await base.UpdateAsync(datatazmin);
                        return yenistatu;
                    }
                }
                else if (data.DurumUnvan == "Hasar Tazmin Uzman Yrd.")
                {
                    yenistatu = "Hasar Tazmin Müdür Yrd.";
                    nextstatu = 6;

                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    datatazmin.TazminStatu = 10;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                else if (data.DurumUnvan == "Hasar Tazmin Uzmanı")
                {
                    yenistatu = "Hasar Tazmin Müdür Yrd.";
                    nextstatu = 6;

                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    datatazmin.TazminStatu = 10;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                else if (data.DurumUnvan == "Hasar Tazmin Müdür Yrd.")
                {
                    yenistatu = "Operasyon Müdürü";
                    nextstatu = 7;

                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    datatazmin.TazminStatu = 11;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                else if (data.DurumUnvan == "Operasyon Müdürü")
                {
                    if (evadata.EvaOdenecek_Tutar <= 10000 && evadata.EvaTazmin_Odeme_Durumu == "2")
                    {
                        yenistatu = "Form Kapatıldı";
                        nextstatu = 44;

                        datatazmin.DurumUnvan = yenistatu;
                        datatazmin.NextStatu = nextstatu;
                        datatazmin.TazminStatu = 17;
                        await base.UpdateAsync(datatazmin);
                        return yenistatu;
                    }
                    else if (evadata.EvaOdenecek_Tutar <= 10000 && evadata.EvaTazmin_Odeme_Durumu == "1")
                    {
                        yenistatu = "Form Kapatıldı";
                        nextstatu = 44;

                        datatazmin.DurumUnvan = yenistatu;
                        datatazmin.NextStatu = nextstatu;
                        datatazmin.TazminStatu = 17;
                        await base.UpdateAsync(datatazmin);
                        return yenistatu;
                    }
                    else if (evadata.EvaOdenecek_Tutar >= 10000)
                    {
                        yenistatu = "Genel Müdür Yrd.";
                        nextstatu = 8;

                        datatazmin.DurumUnvan = yenistatu;
                        datatazmin.NextStatu = nextstatu;
                        datatazmin.TazminStatu = 12;
                        await base.UpdateAsync(datatazmin);
                        return yenistatu;
                    }
                    else
                    {
                        yenistatu = "Genel Müdür Yrd.";
                        nextstatu = 8;

                        datatazmin.DurumUnvan = yenistatu;
                        datatazmin.NextStatu = nextstatu;
                        datatazmin.TazminStatu = 12;
                        await base.UpdateAsync(datatazmin);
                        return yenistatu;
                    }
                }
            }

            //statu Bolge İşlemde
            // Bölge Operasyon Uzman Yrd.-- Bölge Operasyon Uzmanı -- Bölge Müdür Yrd. - Operasyon
            //next statusu 99 sabitle  
            if (Convert.ToString(data.TazminStatu) == "BolgeIslemde" && nextstatu == 99)
            {
                if (data.DurumUnvan == "Bölge Operasyon Uzman Yrd")
                {
                    yenistatu = "Bölge Müdür Yrd. - Operasyon";
                    nextstatu = 1;

                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    datatazmin.TazminStatu = 5;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                else if (data.DurumUnvan == "Bölge Operasyon Uzmanı")
                {
                    yenistatu = "Bölge Müdür Yrd. - Operasyon";
                    nextstatu = 2;

                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    datatazmin.TazminStatu = 9;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                else if (data.DurumUnvan == "Bölge Müdür Yrd. - Operasyon")
                {
                    yenistatu = "Bölge Müdürü";
                    nextstatu = 3;

                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    datatazmin.TazminStatu = 5;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                else if (data.DurumUnvan == "Bölge Müdürü")
                {
                    yenistatu = "Hasar Tazmin Uzman Yrd.";
                    nextstatu = 4;

                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    datatazmin.TazminStatu = 8;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
            }



            //statu GM İşlemde
            // Hasar Tazmin Uzman Yrd. -- Hasar Tazmin Uzmanı  -- Hasar Tazmin Müdür Yrd. --
            //next statusu 88 sabitle  
            if (Convert.ToString(data.TazminStatu) == "GMIslemde" && nextstatu == 88)
            {

                //Operasyon Uzman Yrd.
                //Operasyon Uzmanı
                if (data.DurumUnvan == "Hasar Tazmin Uzman Yrd.")
                {
                    yenistatu = "Hasar Tazmin Müdür Yrd.";
                    nextstatu = 6;

                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    datatazmin.TazminStatu = 10;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                else if (data.DurumUnvan == "Hasar Tazmin Uzmanı")
                {
                    yenistatu = "Hasar Tazmin Müdür Yrd.";
                    nextstatu = 6;

                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    datatazmin.TazminStatu = 9;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                else if (data.DurumUnvan == "Hasar Tazmin Müdür Yrd.")
                {
                    yenistatu = "Operasyon Müdürü";
                    nextstatu = 7;

                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    datatazmin.TazminStatu = 11;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
            }




            if (surecsahibibolgekontrol == true)
            {

                if (data.DurumUnvan == "Bölge Operasyon Uzman Yrd.")
                {
                    yenistatu = "Bölge İşlemde";
                    nextstatu = 99;

                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    datatazmin.TazminStatu = 4;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                else if (data.DurumUnvan == "Bölge Operasyon Uzmanı")
                {
                    yenistatu = "Bölge İşlemde";
                    nextstatu = 99;

                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    datatazmin.TazminStatu = 14;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                else if (data.DurumUnvan == "Bölge Müdür Yrd. - Operasyon")
                {
                    yenistatu = "Bölge İşlemde";
                    nextstatu = 99;

                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    datatazmin.TazminStatu = 4;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                else if (data.DurumUnvan == "Bölge Müdürü")
                {
                    yenistatu = "Bölge İşlemde";
                    nextstatu = 99;

                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    datatazmin.TazminStatu = 4;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                else if (data.DurumUnvan == "Hasar Tazmin Uzman Yrd.")
                {
                    yenistatu = "Bölge İşlemde";
                    nextstatu = 99;

                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    datatazmin.TazminStatu = 4;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                else if (data.DurumUnvan == "Hasar Tazmin Uzmanı")
                {
                    yenistatu = "Bölge İşlemde";
                    nextstatu = 99;

                    datatazmin.TazminStatu = 4;
                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                else if (data.DurumUnvan == "Hasar Tazmin Müdür Yrd.")
                {
                    yenistatu = "GMIslemde";
                    nextstatu = 88;

                    datatazmin.TazminStatu = 7;

                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                else if (data.DurumUnvan == "Operasyon Müdürü")
                {
                    yenistatu = "Hasar Tazmin Müdür Yrd.";
                    nextstatu = 7;

                    datatazmin.TazminStatu = 10;
                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                else if (data.DurumUnvan == "Genel Müdür Yrd.")
                {
                    yenistatu = "Operasyon Müdürü";
                    nextstatu = 8;

                    datatazmin.TazminStatu = 11;
                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                else if (data.DurumUnvan == "Satış Müdürü" || data.DurumUnvan == "Müşteri Deneyimi Müdürü")
                {
                    yenistatu = "GMIslemde";
                    nextstatu = 88;

                    datatazmin.TazminStatu = 7;
                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                //genel müdrü yardımcı  ise  operasyon müdürü
                //satis genel müdürü  revize derse bireysel  ise  müşteri deneyimi müdürüne  kurumsal ise satiş müdürü //SORULACAK
                //müşteri deneyimi müdürüne ve satis müdürü revize ederse GMISLEMDE

            }



            if (data.DurumUnvan == "Bölge Operasyon Uzman Yrd.")
            {
                yenistatu = "Bölge Müdür Yrd. - Operasyon";
                nextstatu = 1;


                datatazmin.TazminStatu = 5;
                datatazmin.DurumUnvan = yenistatu;
                datatazmin.NextStatu = nextstatu;
                await base.UpdateAsync(datatazmin);
                return yenistatu;
            }
            else if (data.DurumUnvan == "Bölge Operasyon Uzmanı")
            {
                yenistatu = "Bölge Müdür Yrd. - Operasyon";
                nextstatu = 2;


                datatazmin.TazminStatu = 5;
                datatazmin.DurumUnvan = yenistatu;
                datatazmin.NextStatu = nextstatu;
                await base.UpdateAsync(datatazmin);
                return yenistatu;
            }
            else if (data.DurumUnvan == "Bölge Müdür Yrd. - Operasyon")
            {
                yenistatu = "Bölge Müdürü";
                nextstatu = 3;


                datatazmin.TazminStatu = 6;
                datatazmin.DurumUnvan = yenistatu;
                datatazmin.NextStatu = nextstatu;
                await base.UpdateAsync(datatazmin);
                return yenistatu;
            }
            else if (data.DurumUnvan == "Bölge Müdürü")
            {
                //ödeme tutar 1000 altında ve kusurlu birim hayır ise 
                if (evadata.EvaOdenecek_Tutar <= 1000 && evadata.EvaKusurlu_Birim == "2")
                {
                    yenistatu = "Form Kapatıldı";
                    nextstatu = 44;

                    datatazmin.TazminStatu = 17;
                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                //ödeme tutar 1000 altında ve kusurlu birim evet ise 
                else if (evadata.EvaOdenecek_Tutar <= 1000 && evadata.EvaKusurlu_Birim == "1")
                {
                    yenistatu = "GMIslemde";
                    nextstatu = 88;

                    datatazmin.TazminStatu = 7;
                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                //ödeme tutar 1000 üstünde ve kusurlu birim hayır ve odenmiyecek ise
                else if (evadata.EvaOdenecek_Tutar >= 1000 && evadata.EvaKusurlu_Birim == "2" && evadata.EvaTazmin_Odeme_Durumu == "2")
                {
                    yenistatu = "Form Kapatıldı";
                    nextstatu = 44;

                    datatazmin.TazminStatu = 17;
                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                //tazmin tipi kayıp birim  ve odenmiyecek ise
                else if (Enum.GetName(typeof(TazminTipi), data.Tazmin_Tipi) == "2" && evadata.EvaTazmin_Odeme_Durumu == "2")
                {
                    yenistatu = "Form Kapatıldı";
                    nextstatu = 44;

                    datatazmin.TazminStatu = 17;
                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                //tazmin tipi müşteri memnuniyet ve odenmiyecek ise
                else if (Enum.GetName(typeof(TazminTipi), data.Tazmin_Tipi) == "4" && evadata.EvaTazmin_Odeme_Durumu == "2")
                {
                    yenistatu = "Form Kapatıldı";
                    nextstatu = 44;

                    datatazmin.TazminStatu = 17;
                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                //tazmin tipi geç teslimat ve odenmiyecek ise
                else if (Enum.GetName(typeof(TazminTipi), data.Tazmin_Tipi) == "3" && evadata.EvaTazmin_Odeme_Durumu == "2")
                {
                    yenistatu = "Form Kapatıldı";
                    nextstatu = 44;

                    datatazmin.TazminStatu = 17;
                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                else
                {
                    yenistatu = "GMIslemde";
                    nextstatu = 88;

                    datatazmin.TazminStatu = 7;
                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
            }
            else if (data.DurumUnvan == "Hasar Tazmin Uzman Yrd.")
            {
                yenistatu = "Hasar Tazmin Uzmanı";
                nextstatu = 5;

                datatazmin.TazminStatu = 9;
                datatazmin.DurumUnvan = yenistatu;
                datatazmin.NextStatu = nextstatu;
                await base.UpdateAsync(datatazmin);
                return yenistatu;
            }
            else if (data.DurumUnvan == "Hasar Tazmin Uzmanı")
            {
                yenistatu = "Hasar Tazmin Müdür Yrd.";
                nextstatu = 6;

                datatazmin.TazminStatu = 10;
                datatazmin.DurumUnvan = yenistatu;
                datatazmin.NextStatu = nextstatu;
                await base.UpdateAsync(datatazmin);
                return yenistatu;
            }
            else if (data.DurumUnvan == "Hasar Tazmin Müdür Yrd.")
            {
                yenistatu = "Operasyon Müdürü";
                nextstatu = 7;

                datatazmin.TazminStatu = 11;
                datatazmin.DurumUnvan = yenistatu;
                datatazmin.NextStatu = nextstatu;
                await base.UpdateAsync(datatazmin);
                return yenistatu;
            }
            else if (data.DurumUnvan == "Operasyon Müdürü")
            {
                if (evadata.EvaOdenecek_Tutar <= 10000 && evadata.EvaTazmin_Odeme_Durumu == "2")
                {
                    yenistatu = "Form Kapatıldı";
                    nextstatu = 44;

                    datatazmin.TazminStatu = 17;
                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                else if (evadata.EvaOdenecek_Tutar <= 10000 && evadata.EvaTazmin_Odeme_Durumu == "1")
                {
                    yenistatu = "Form Kapatıldı";
                    nextstatu = 44;

                    datatazmin.TazminStatu = 17;
                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                else if (evadata.EvaOdenecek_Tutar > 10000)
                {

                    yenistatu = "Genel Müdür Yrd.";
                    nextstatu = 8;

                    datatazmin.TazminStatu = 12;
                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
                else
                {
                    yenistatu = "Genel Müdür Yrd.";
                    nextstatu = 8;

                    datatazmin.TazminStatu = 12;
                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }
            }






            if (evadata.EvaOdenecek_Tutar > 10000)
            {
                ///10000 tl üstünce 
                /////1
                // tazmin tipi hasar 
                // tazmin tipi kayıp 
                //tazmin geç teslim   gmy operasyon müdür yardımcısı na tolge bey gitcek
                // odendi yada odenmicek diyip form kapatılacak
                if (Enum.GetName(typeof(TazminTipi), data.Tazmin_Tipi) == "1" || Enum.GetName(typeof(TazminTipi), data.Tazmin_Tipi) == "2" || Enum.GetName(typeof(TazminTipi), data.Tazmin_Tipi) == "3")
                {
                    //yenistatu = "Form Kapatıldı";
                    //nextstatu = 44;   /// SORULACAK

                    yenistatu = "Form Kapatıldı";
                    nextstatu = 44; //ileri statu yok 

                    datatazmin.TazminStatu = 17;
                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;

                }


                //2
                // tazmin tipi müşteri memnüniyeti
                //müşteri tipi bireysel  müşteri deneyimi müdürü  gitcek
                if (Enum.GetName(typeof(TazminTipi), data.Tazmin_Tipi) == "4" && Enum.GetName(typeof(OdemeMusteriTipi), data.Odeme_Musteri_Tipi) == "1")
                {
                    yenistatu = "Müşteri Deneyimi Müdürü";
                    nextstatu = 999; //ileri statu yok 


                    datatazmin.TazminStatu = 16;
                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }

                //3
                // tazmin tipi müşteri memnüniyeti
                //müşteri tipi kurumsal  satiş müdürü  zeynep gitcek
                if (Enum.GetName(typeof(TazminTipi), data.Tazmin_Tipi) == "4" && Enum.GetName(typeof(OdemeMusteriTipi), data.Odeme_Musteri_Tipi) == "2")
                {
                    yenistatu = "Satış Müdürü";
                    nextstatu = 999; //ileri statu yok ;  

                    datatazmin.TazminStatu = 15;
                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }


                //2 ve 3 onaylarsa odeme tipi odenecek  ise satis gmy gitcek hakan kaya  onaylamazsa form kapart
                //satiş müdürü onaylasada onaylamasa form kapatılacak 
                if (data.DurumUnvan == "Satış Müdürü" || data.DurumUnvan == "Müşteri Deneyimi Müdürü" && evadata.EvaTazmin_Odeme_Durumu == "1")
                {
                    //yenistatu = "Form Kapatıldı";
                    //nextstatu = 44;   /// burası sorulacak
                    yenistatu = "Form Kapatıldı";
                    nextstatu = 44;

                    datatazmin.TazminStatu = 17;
                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }

                if (data.DurumUnvan == "Satış Müdürü")
                {
                    yenistatu = "Form Kapatıldı";
                    nextstatu = 44;

                    datatazmin.TazminStatu = 17;
                    datatazmin.DurumUnvan = yenistatu;
                    datatazmin.NextStatu = nextstatu;
                    await base.UpdateAsync(datatazmin);
                    return yenistatu;
                }

            }



            //genel müdürü onayalşadı veya reddtti form kapatılmış



            return "False";

        }



        public async Task<string> RevizeDamageStatus(int id)
        {

            var data = Repository.Get(id);
            // tazmin datası cevirme 
            var datatazmin = await base.GetAsync(new EntityDto<long> { Id = id });

            string yenistatu;
            int nextstatu = 99999;

            if (data.DurumUnvan == "Bölge Operasyon Uzman Yrd.")
            {
                yenistatu = "Bölge İşlemde";
                nextstatu = 99;

                datatazmin.DurumUnvan = yenistatu;
                datatazmin.NextStatu = nextstatu;
                datatazmin.TazminStatu = 4;
                await base.UpdateAsync(datatazmin);
                return yenistatu;
            }
            else if (data.DurumUnvan == "Bölge Operasyon Uzmanı")
            {
                yenistatu = "Bölge İşlemde";
                nextstatu = 99;

                datatazmin.DurumUnvan = yenistatu;
                datatazmin.NextStatu = nextstatu;
                datatazmin.TazminStatu = 14;
                await base.UpdateAsync(datatazmin);
                return yenistatu;
            }
            else if (data.DurumUnvan == "Bölge Müdür Yrd. - Operasyon")
            {
                yenistatu = "Bölge İşlemde";
                nextstatu = 99;

                datatazmin.DurumUnvan = yenistatu;
                datatazmin.NextStatu = nextstatu;
                datatazmin.TazminStatu = 4;
                await base.UpdateAsync(datatazmin);
                return yenistatu;
            }
            else if (data.DurumUnvan == "Bölge Müdürü")
            {
                yenistatu = "Bölge İşlemde";
                nextstatu = 99;

                datatazmin.DurumUnvan = yenistatu;
                datatazmin.NextStatu = nextstatu;
                datatazmin.TazminStatu = 4;
                await base.UpdateAsync(datatazmin);
                return yenistatu;
            }
            else if (data.DurumUnvan == "Hasar Tazmin Uzman Yrd.")
            {
                yenistatu = "Bölge İşlemde";
                nextstatu = 99;

                datatazmin.DurumUnvan = yenistatu;
                datatazmin.NextStatu = nextstatu;
                datatazmin.TazminStatu = 4;
                await base.UpdateAsync(datatazmin);
                return yenistatu;
            }
            else if (data.DurumUnvan == "Hasar Tazmin Uzmanı")
            {
                yenistatu = "Bölge İşlemde";
                nextstatu = 99;

                datatazmin.TazminStatu = 4;
                datatazmin.DurumUnvan = yenistatu;
                datatazmin.NextStatu = nextstatu;
                await base.UpdateAsync(datatazmin);
                return yenistatu;
            }
            else if (data.DurumUnvan == "Hasar Tazmin Müdür Yrd.")
            {
                yenistatu = "GMIslemde";
                nextstatu = 88;

                datatazmin.TazminStatu = 7;

                datatazmin.DurumUnvan = yenistatu;
                datatazmin.NextStatu = nextstatu;
                await base.UpdateAsync(datatazmin);
                return yenistatu;
            }
            else if (data.DurumUnvan == "Operasyon Müdürü")
            {
                yenistatu = "Hasar Tazmin Müdür Yrd.";
                nextstatu = 7;

                datatazmin.TazminStatu = 10;
                datatazmin.DurumUnvan = yenistatu;
                datatazmin.NextStatu = nextstatu;
                await base.UpdateAsync(datatazmin);
                return yenistatu;
            }
            else if (data.DurumUnvan == "Genel Müdür Yrd.")
            {
                yenistatu = "Operasyon Müdürü";
                nextstatu = 8;

                datatazmin.TazminStatu = 11;
                datatazmin.DurumUnvan = yenistatu;
                datatazmin.NextStatu = nextstatu;
                await base.UpdateAsync(datatazmin);
                return yenistatu;
            }
            else if (data.DurumUnvan == "Satış Müdürü" || data.DurumUnvan == "Müşteri Deneyimi Müdürü")
            {
                yenistatu = "GMIslemde";
                nextstatu = 88;

                datatazmin.TazminStatu = 7;
                datatazmin.DurumUnvan = yenistatu;
                datatazmin.NextStatu = nextstatu;
                await base.UpdateAsync(datatazmin);
                return yenistatu;
            }
            else
            {
                return "false";
            }
            //genel müdrü yardımcı  ise  operasyon müdürü
            //satis genel müdürü  revize derse bireysel  ise  müşteri deneyimi müdürüne  kurumsal ise satiş müdürü //SORULACAK
            //müşteri deneyimi müdürüne ve satis müdürü revize ederse GMISLEMDE


        }





        public async Task<string> WebSiparisKontrol(string id)
        {
            var data = Repository.GetAll().Where(x => x.Web_Siparis_Kodu == id).FirstOrDefault();

            if (data == null)
            {
                return "true";
            }
            else
            {
                return "false";
            }


        }











    }
}