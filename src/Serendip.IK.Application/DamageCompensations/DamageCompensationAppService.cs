using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
<<<<<<< HEAD
using Abp.Runtime.Session;
=======
>>>>>>> 347d5cb804807c7f9592e0127018cdd5c84aed0d
using Newtonsoft.Json;
using Refit;
using Serendip.IK.DamageCompensations.Dto;
using Serendip.IK.DamageCompensationsEvalutaion;
using Serendip.IK.DamageCompensationsFileInfo;
using Serendip.IK.DamageCompensationsFileInfo.Dto;
using Serendip.IK.Ops.Nodes;
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
        private IDamageCompensationEvalutaionAppService _damageCompensationEvalutaionAppService;
        private IDamageCompensationFileInfoAppService _damageCompensationFileInfoAppService;
        private readonly IAbpSession _abpSession;

        #endregion

        public DamageCompensationAppService(IRepository<DamageCompensation, long> repository, IUserAppService userAppService, IAbpSession abpSession,
             IDamageCompensationEvalutaionAppService damageCompensationEvalutaionAppService,
             IDamageCompensationFileInfoAppService damageCompensationFileInfoAppService
            ) : base(repository)
        {
            _userAppService = userAppService;
            _abpSession = abpSession;
            _damageCompensationEvalutaionAppService = damageCompensationEvalutaionAppService;
            _damageCompensationFileInfoAppService = damageCompensationFileInfoAppService;

        }

        public override async Task<DamageCompensationDto> CreateAsync(CreateDamageCompensationDto input)
        {

            if (input.FileTazminDilekcesi == "[]")
            {

                input.TazminStatu = 2;
                var result = ObjectMapper.Map<DamageCompensation>(input);
                var data = ObjectMapper.Map<CreateDamageCompensationDto>(result);
                var createadata = await base.CreateAsync(data);
<<<<<<< HEAD
                Thread.Sleep(500);
                FileDbInsert(input);
                // createadata = null;
=======
                FileDbInsert(input);
                createadata = null;
>>>>>>> 347d5cb804807c7f9592e0127018cdd5c84aed0d
                return createadata;

            }
            else
            {
                input.TazminStatu = 3;
                var result = ObjectMapper.Map<DamageCompensation>(input);
                var data = ObjectMapper.Map<CreateDamageCompensationDto>(result);
                var createadata = await base.CreateAsync(data);
                Thread.Sleep(500);
                FileDbInsert(input);
                // createadata = null;
                return createadata;
            }

        }

        private void FileDbInsert(CreateDamageCompensationDto input)
        {
            //dosya kontrolleri db kaydet
            if (input.FileTazminDilekcesi != "[]")
            {
                //json cevir database kaydet
                FileBase64 filestazmindilekce = new FileBase64();
                filestazmindilekce = JsonConvert.DeserializeObject<FileBase64>(input.FileTazminDilekcesi);
                CreateDamageCompensationFileInfoDto createDamageCompensationFileInfoDto = new CreateDamageCompensationFileInfoDto();
                string[] name = filestazmindilekce.name.Split('.');
                string fileName = $"{name[0]}-{Guid.NewGuid().ToString("N")}";

                createDamageCompensationFileInfoDto.DosyaAdi = fileName;
                createDamageCompensationFileInfoDto.DosyaUzantisi = filestazmindilekce.type;
                createDamageCompensationFileInfoDto.DosyaYolu = $@"/HasarTazmin/{fileName}.{name[1]}";
                createDamageCompensationFileInfoDto.DamageCompensationId = Convert.ToInt32(input.TazminId);
                createDamageCompensationFileInfoDto.DosyaTyp = 1;
                createDamageCompensationFileInfoDto.DosyaActive = true;
                _damageCompensationFileInfoAppService.CreateAsync(createDamageCompensationFileInfoDto);
<<<<<<< HEAD
                Thread.Sleep(500);
=======
>>>>>>> 347d5cb804807c7f9592e0127018cdd5c84aed0d
                UploadFile(filestazmindilekce.base64, $"{fileName}.{name[1]}");
            }




            if (input.FileFatura != "[]")
            {
                //json cevir database kaydet
                FileBase64 filestazmindilekce = new FileBase64();
                filestazmindilekce = JsonConvert.DeserializeObject<FileBase64>(input.FileFatura);
                CreateDamageCompensationFileInfoDto createDamageCompensationFileInfoDto = new CreateDamageCompensationFileInfoDto();
                string[] name = filestazmindilekce.name.Split('.');
                var guid = Guid.NewGuid().ToString("N");
                string guidname = "" + name[0] + "-" + guid + "";
                createDamageCompensationFileInfoDto.DosyaAdi = guidname;
                createDamageCompensationFileInfoDto.DosyaUzantisi = filestazmindilekce.type;
                createDamageCompensationFileInfoDto.DosyaYolu = @"/HasarTazmin/" + guidname + "." + name[1] + "";
                createDamageCompensationFileInfoDto.DamageCompensationId = Convert.ToInt32(input.TazminId);
                createDamageCompensationFileInfoDto.DosyaTyp = 2;
                createDamageCompensationFileInfoDto.DosyaActive = true;
                _damageCompensationFileInfoAppService.CreateAsync(createDamageCompensationFileInfoDto);
                Thread.Sleep(500);
                UploadFile(filestazmindilekce.base64, "" + guidname + "." + name[1] + "");
            }

            if (input.FileSevkirsaliye != "[]")
            {
                //json cevir database kaydet
                FileBase64 filestazmindilekce = new FileBase64();
                filestazmindilekce = JsonConvert.DeserializeObject<FileBase64>(input.FileSevkirsaliye);
                CreateDamageCompensationFileInfoDto createDamageCompensationFileInfoDto = new CreateDamageCompensationFileInfoDto();
                string[] name = filestazmindilekce.name.Split('.');
                var guid = Guid.NewGuid().ToString("N");
                string guidname = "" + name[0] + "-" + guid + "";
                createDamageCompensationFileInfoDto.DosyaAdi = guidname;
                createDamageCompensationFileInfoDto.DosyaUzantisi = filestazmindilekce.type;
                createDamageCompensationFileInfoDto.DosyaYolu = @"/HasarTazmin/" + guidname + "." + name[1] + "";
                createDamageCompensationFileInfoDto.DamageCompensationId = Convert.ToInt32(input.TazminId);
                createDamageCompensationFileInfoDto.DosyaTyp = 3;
                createDamageCompensationFileInfoDto.DosyaActive = true;
                _damageCompensationFileInfoAppService.CreateAsync(createDamageCompensationFileInfoDto);
                Thread.Sleep(500);
                UploadFile(filestazmindilekce.base64, "" + guidname + "." + name[1] + "");
            }

            if (input.FileTcVkno != "[]")
            {
                //json cevir database kaydet
                FileBase64 filestazmindilekce = new FileBase64();
                filestazmindilekce = JsonConvert.DeserializeObject<FileBase64>(input.FileTcVkno);
                CreateDamageCompensationFileInfoDto createDamageCompensationFileInfoDto = new CreateDamageCompensationFileInfoDto();
                string[] name = filestazmindilekce.name.Split('.');
                var guid = Guid.NewGuid().ToString("N");
                string guidname = "" + name[0] + "-" + guid + "";
                createDamageCompensationFileInfoDto.DosyaAdi = guidname;
                createDamageCompensationFileInfoDto.DosyaUzantisi = filestazmindilekce.type;
                createDamageCompensationFileInfoDto.DosyaYolu = @"/HasarTazmin/" + guidname + "." + name[1] + "";
                createDamageCompensationFileInfoDto.DamageCompensationId = Convert.ToInt32(input.TazminId);
                createDamageCompensationFileInfoDto.DosyaTyp = 4;
                createDamageCompensationFileInfoDto.DosyaActive = true;
                _damageCompensationFileInfoAppService.CreateAsync(createDamageCompensationFileInfoDto);
                Thread.Sleep(500);
                UploadFile(filestazmindilekce.base64, "" + guidname + "." + name[1] + "");
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

                if ("" + user.CompanyObjId + "" == item.Surec_Sahibi_Birim_Bolge.Split('-')[0].ToString())
                { all.BtnControl = false; }
                else all.BtnControl = true;


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
                resultDto.TazminStatu = Convert.ToString(data.TazminStatu);
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



        //tazmin formu onyalandı kapatıldı
        public void DamageCompensationApproval(long id)
        {

            var input = Repository.Get(id);
            input.TazminStatu = TazminStatu.TazminFormuOnaylandi;
            //var result = ObjectMapper.Map<DamageCompensation>(input);
            //var data = ObjectMapper.Map<CreateDamageCompensationDto>(result);
            var updatadata = Repository.Update(input);



        }


    }
}