using Abp.Application.Services;
using Abp.Domain.Repositories;
using Serendip.IK.DamageCompensationsEvalutaion.Dto;
using Serendip.IK.Users;
using SuratKargo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using System.Threading.Tasks;

namespace Serendip.IK.DamageCompensationsEvalutaion
{
    public class DamageCompensationEvalutaionAppService : AsyncCrudAppService<
        DamageCompensationEvaluation,
        DamageCompensaitonEvalutaionDto,
        long,
        PagedDamageCompensationEvalutaionResultRequestDto,
        CreateDamageCompensationEvalutaionDto,
        DamageCompensaitonEvalutaionDto>, IDamageCompensationEvalutaionAppService
    {

        #region Constructor
        private IUserAppService _userAppService;
        #endregion

        public DamageCompensationEvalutaionAppService(IRepository<DamageCompensationEvaluation, long> repository, IUserAppService userAppService) : base(repository)
        {
            _userAppService = userAppService;

        }

        public override Task<DamageCompensaitonEvalutaionDto> CreateAsync(CreateDamageCompensationEvalutaionDto input)
        {
            return base.CreateAsync(input);
        }

        public object GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public object GetAsync(long ıd)
        {
            throw new NotImplementedException();
        }

        public override Task<DamageCompensaitonEvalutaionDto> UpdateAsync(DamageCompensaitonEvalutaionDto input)
        {
            return base.UpdateAsync(input);
        }


        public async Task<DamageCompensaitonEvalutaionDto> GetLastTazminIdRow(long id)
        {

            var data = base.Repository.GetAll().Where(x => x.TazminId == id).ToList().OrderByDescending(x => x.Id).Take(1).FirstOrDefault(); ;
            DamageCompensaitonEvalutaionDto dto = new DamageCompensaitonEvalutaionDto();
            if (data == null)
            {
                return dto = null;
            }
            else
            {
                dto.TazminId = data.TazminId;
                if (data.EvaTazmin_Tipi == "1")
                {
                    dto.EvaTazmin_Tipi = "Hasar";
                }
                else if (data.EvaTazmin_Tipi == "2")
                {

                    dto.EvaTazmin_Tipi = "Kayıp";
                }
                else if (data.EvaTazmin_Tipi == "3")
                {

                    dto.EvaTazmin_Tipi = "Geç Teslimat";
                }
                else if (data.EvaTazmin_Tipi == "4")
                {

                    dto.EvaTazmin_Tipi = "Müşteri Memnuniyeti";
                }
                else
                {
                    dto.EvaTazmin_Tipi = "";
                }



                dto.EvaTazmin_Nedeni = data.EvaTazmin_Nedeni;
                dto.EvaKargo_Bulundugu_Yer = data.EvaKargo_Bulundugu_Yer;

                dto.EvaKusurlu_Birim = data.EvaKusurlu_Birim == "1" ? "Evet" : "Hayır";
                dto.EvaIcerik_Grubu = data.EvaIcerik_Grubu;
                dto.EvaIcerik = data.EvaIcerik;
                dto.EvaUrun_Aciklama = data.EvaUrun_Aciklama;
                dto.EvaEkleyen_Kullanici = data.EvaEkleyen_Kullanici;
                dto.EvaBolge_Aciklama = data.EvaBolge_Aciklama;
                dto.EvaGm_Aciklama = data.EvaGm_Aciklama;

                if (data.EvaTazmin_Odeme_Durumu == "1")
                {
                    dto.EvaTazmin_Odeme_Durumu = "Ödenecek";
                }
                else if (data.EvaTazmin_Odeme_Durumu == "2")
                {
                    dto.EvaTazmin_Odeme_Durumu = "Ödenemicek";
                }
                else if (data.EvaTazmin_Odeme_Durumu == "3")
                {
                    dto.EvaTazmin_Odeme_Durumu = "Farklı Bir Tutar Ödenecek";
                }
                else
                {
                    dto.EvaTazmin_Odeme_Durumu = "";
                }



                dto.EvaOdenecek_Tutar = data.EvaOdenecek_Tutar;
                dto.EvaTalep_Edilen_Tutar = data.EvaTalep_Edilen_Tutar;
                return dto;
            }
        }



    

       



    }
}
