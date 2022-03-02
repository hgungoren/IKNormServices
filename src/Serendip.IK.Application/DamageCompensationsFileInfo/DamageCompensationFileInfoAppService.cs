using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Session;
using Newtonsoft.Json;
using Serendip.IK.DamageCompensations;
using Serendip.IK.DamageCompensations.Dto;
using Serendip.IK.DamageCompensationsFileInfo.Dto;
using Serendip.IK.Users;
using SuratKargo.Core.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
namespace Serendip.IK.DamageCompensationsFileInfo
{
    public class DamageCompensationFileInfoAppService : AsyncCrudAppService<
        DamageCompensationFileInfo,
        DamageCompensationFileInfoDto,
        long,
        PagedDamageCompensationFileInfoResultRequestDto,
        CreateDamageCompensationFileInfoDto,
        DamageCompensationFileInfoDto>, IDamageCompensationFileInfoAppService
    {


        #region Constructor
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        private IUserAppService _userAppService;
        private IAbpSession _abpSession;
        #endregion

        public DamageCompensationFileInfoAppService(IRepository<DamageCompensationFileInfo, long> repository,
            IUnitOfWorkManager unitOfWorkManager,
            IUserAppService userAppService,
            IAbpSession abpSession
       ) : base(repository)
        {
            _unitOfWorkManager = unitOfWorkManager;
            _userAppService = userAppService;
            _abpSession = abpSession;
        

        }

        public override Task<DamageCompensationFileInfoDto> CreateAsync(CreateDamageCompensationFileInfoDto input)
        {
            return base.CreateAsync(input);
        }


        public override Task<DamageCompensationFileInfoDto> UpdateAsync(DamageCompensationFileInfoDto input)
        {

            return base.UpdateAsync(input);
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


        public async Task<string> UpdateFileList(FileInfoDamage input)
        {

            var datalist = Repository.GetAll().Where(x => x.DamageCompensationId == input.TazminId && x.DosyaActive == true).ToList();

            var tazmindilekcesi = datalist.Where(x => x.DosyaTyp == 1).FirstOrDefault();
            // tazmindilekcesi ya dolu gelcek ya null gelcek. Dolu gelirse olan datayı guncelleyip active false cek 
            //sonra yeni gelen data varsa onu insert et 
            //eger tazmin dilekcesi null ise direk insert etmen yeterli

            List<Dto.FileBase64> filestazmindilekce = new List<Dto.FileBase64>();
            filestazmindilekce = JsonConvert.DeserializeObject<List<Dto.FileBase64>>(input.FileTazminDilekcesi);

            for (int i = 0; i < filestazmindilekce.Count; i++)
            {
                //direk kaydet 
                //json cevir database kaydet
             
                CreateDamageCompensationFileInfoDto createDamageCompensationFileInfoDto = new CreateDamageCompensationFileInfoDto();
                string[] name = filestazmindilekce[i].name.Split('.');
                string fileName = $"{name[0]}-{Guid.NewGuid().ToString("N")}";
                var guid = Guid.NewGuid().ToString("N");
                string guidname = "" + name[0] + "-" + guid + "";
                createDamageCompensationFileInfoDto.DosyaAdi = guidname;
                createDamageCompensationFileInfoDto.DosyaUzantisi = filestazmindilekce[i].type;
                createDamageCompensationFileInfoDto.DosyaYolu = @"/HasarTazmin/" + guidname + "." + name[1] + "";//sunucu tarafındaki yol
                createDamageCompensationFileInfoDto.DamageCompensationId = Convert.ToInt32(input.TazminId); // tazmin id
                createDamageCompensationFileInfoDto.DosyaTyp = 1;  // 1 tazmim dilekcesi
                createDamageCompensationFileInfoDto.DosyaActive = true;
                await base.CreateAsync(createDamageCompensationFileInfoDto);
                UploadFile(filestazmindilekce[i].base64, "" + guidname + "." + name[1] + "");

            }

            return "Başarılı";
        }


    }
}
