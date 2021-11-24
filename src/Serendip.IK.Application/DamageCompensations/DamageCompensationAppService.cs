using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Collections.Extensions;
using Abp.Domain.Repositories;
using Abp.Extensions;
using Refit;
using Serendip.IK.DamageCompensations.Dto;
using Serendip.IK.Users;
using SuratKargo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
        private const string SERENDIP_K_KSUBE_API_URL = ApiConsts.K_SUBE_API_URL;

        private IUserAppService _userAppService;


        #endregion


        public DamageCompensationAppService(IRepository<DamageCompensation, long> repository, IUserAppService userAppService) : base(repository)
        {
            _userAppService = userAppService;

        }






        public override Task<DamageCompensationDto> CreateAsync(CreateDamageCompensationDto input)
        {

            input.TazminStatu = 2;
            return base.CreateAsync(input);
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
            var service = RestService.For<IDamageCompensationApi>(SERENDIP_SERVICE_BASE_URL);
            var data = await service.GetDamageCompensations(id);
            return data;

        }


        public async Task<List<DamageCompensationGetCariListDto>> GetCariListAsynDamage(string id)
        {
            var service = RestService.For<IDamageCompensationApi>(SERENDIP_K_KCARI_API_URL);
            var data = await service.GetCariListAsynDamage(id);
            return data;
        }





        public async Task<List<DamageCompensationGetBirimListDto>> GetBirimListAsynDamage()
        {
            try
            {
                var service = RestService.For<IDamageCompensationApi>(SERENDIP_K_BIRIM_API_URL);
                var data = await service.GetAllAsync();
                return data;
            }
            catch (Exception e)
            {

                throw;
            }


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


        public async Task<int> GetDamageLastId()
        {
            long count = Repository.GetAll().Max(x => x.Id);

            Repository.GetAll();

            return Convert.ToInt32(count) + Convert.ToInt32(1);

        }


        public async Task<List<GetDamageCompensationAllList>> GetAllDamageCompensation()
        {
            var serviceBolge = RestService.For<IDamageCompensationApi>(SERENDIP_K_KSUBE_API_URL);

            List<GetDamageCompensationAllList> list = new List<GetDamageCompensationAllList>();
            var data = await Repository.GetAllListAsync();

            List<DamageCompensationGetBranchsListDto> bolgelist = await serviceBolge.GetKBolgeListDamageAll();


            foreach (var item in data)
            {
                GetDamageCompensationAllList all = new GetDamageCompensationAllList();
                all.TazminNo = item.Id;   //ok
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
                odemeLong =Odeme_Birimi_Bolge.ObjId;
            }

            if (data.Surec_Sahibi_Birim_Bolge != null)
            {
                string[] parcala_Surec_Sahibi_Birim_Bolge = data.Surec_Sahibi_Birim_Bolge.Split('-');
                string b = parcala_Surec_Sahibi_Birim_Bolge[0].ToString();
                DamageCompensationGetBranchsListDto Surec_Sahibi_Birim_Bolge = dataBolge.Where(x => x.ObjId == b).FirstOrDefault();
                surecsahibitxt = Surec_Sahibi_Birim_Bolge.Adi;
                surecsahibiLong= Surec_Sahibi_Birim_Bolge.ObjId;
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

            long blong = Convert.ToInt64( data.Birimi_ObjId);
            DamageCompensationGetBirimListDto bb = databirim.Where(x => x.ObjId == blong).FirstOrDefault();

            long birim= (bb == null ? 0 : Convert.ToInt64(bb.ObjId));


            DamageCompensationDto dto = new DamageCompensationDto();
            dto.TakipNo = Convert.ToString(data.TakipNo);
            dto.Sistem_InsertTime = data.Sistem_InsertTime;
            dto.EvrakSeriNo = data.EvrakSeriNo;
            dto.GonderenKodu = data.GonderenKodu;
            dto.GonderenUnvan = data.GonderenUnvan;
            dto.AliciKodu = data.AliciKodu;
            dto.AliciUnvan = data.AliciUnvan;

            dto.IlkGondericiSube_ObjId = ilk;

            dto.Cikis_Sube_Unvan = data.Cikis_Sube_Unvan;
            dto.VarisSube_ObjId = varis;
           
            dto.Varis_Sube_Unvan = data.Varis_Sube_Unvan;
            dto.Birimi_ObjId = birim;
            dto.Birimi = data.Birimi;
            dto.Adet = data.Adet;
            dto.TazminStatu = (int)data.TazminStatu;
            dto.TazminStatuAd= Enum.GetName(typeof(TazminStatu), data.TazminStatu);

            dto.Tazmin_Talep_Tarihi = data.Tazmin_Talep_Tarihi;
            dto.Tazmin_Tipi = Enum.GetName(typeof(TazminTipi), data.Tazmin_Tipi);
            dto.Tazmin_Musteri_Tipi = Convert.ToInt32(data.Tazmin_Musteri_Tipi);
            dto.Tazmin_Musteri_Kodu = data.Tazmin_Musteri_Kodu;
            dto.Tazmin_Musteri_Unvan = data.Tazmin_Musteri_Unvan;
            dto.Odeme_Musteri_Tipi = Enum.GetName(typeof(OdemeMusteriTipi), data.Odeme_Musteri_Tipi); ;
            dto.TCK_NO = data.TCK_NO;
            dto.VK_NO = data.VK_NO;
            dto.Odeme_Birimi_Bolge = odemeLong;
            dto.Odeme_Birimi_Bolge_Text= odemetext;

            dto.Talep_Edilen_Tutar = data.Talep_Edilen_Tutar;
            dto.Surec_Sahibi_Birim_Bolge = surecsahibiLong;
            dto.Surec_Sahibi_Birim_Bolge_Text = surecsahibiLong;
            dto.Telefon = data.Telefon;
            dto.Email = data.Email;
            dto.Id = data.Id;


            return dto;
        }



        //filitreleme
        public async Task<List<GetDamageCompensationAllList>> GetDamageCompensationFilter(bool checktakipNo,bool checktazminID,long? search,DateTime? start,DateTime? finish)
        {


            var datalist = Repository.GetAll();
            if (checktakipNo == true)
            {
                if (start !=null && finish !=null)
                {
                    datalist = datalist.Where(x => x.TakipNo == search && x.Tazmin_Talep_Tarihi >=start && x.Tazmin_Talep_Tarihi <=finish);
                }
                else if(start !=null && finish == null)
                {
                    datalist = datalist.Where(x => x.TakipNo == search && x.Tazmin_Talep_Tarihi >= start);
                }
                else if(start == null && finish != null)
                {
                    datalist = datalist.Where(x => x.TakipNo == search &&  x.Tazmin_Talep_Tarihi <= finish);
                }
                else if(start ==null && finish == null)
                {
                    datalist = datalist.Where(x => x.TakipNo == search);
                }
                else
                {
                    datalist = null;  
                }

            }


            if (checktazminID == true)
            {
                if (start != null && finish != null)
                {
                    datalist = datalist.Where(x => x.Id == search && x.Tazmin_Talep_Tarihi >= start && x.Tazmin_Talep_Tarihi <= finish);
                }
                else if (start != null && finish == null)
                {
                    datalist = datalist.Where(x => x.Id == search && x.Tazmin_Talep_Tarihi >= start);
                }
                else if (start == null && finish != null)
                {
                    datalist = datalist.Where(x => x.Id == search && x.Tazmin_Talep_Tarihi <= finish);
                }
                else if (start == null && finish == null)
                {
                    datalist = datalist.Where(x => x.Id == search);
                }
                else
                {
                    datalist = null;
                }

            }


            if (checktakipNo == false && checktazminID == false)
            {
                if (start !=null && finish !=null)
                {
                    datalist = datalist.Where(x=>x.Tazmin_Talep_Tarihi >= start && x.Tazmin_Talep_Tarihi <= finish);

                }
                else if(start != null && finish == null)
                {
                    datalist = datalist.Where(x => x.Tazmin_Talep_Tarihi >= start);
                }
                else if(start ==null && finish !=null)
                {
                    datalist = datalist.Where(x => x.Tazmin_Talep_Tarihi <= finish);
                }
                else if(start==null  && finish == null)
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
                all.TazminNo = item.Id;   //ok
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





    }
}
