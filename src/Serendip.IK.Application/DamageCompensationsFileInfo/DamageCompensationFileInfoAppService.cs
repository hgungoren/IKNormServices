using Abp.Application.Services;
using Abp.Domain.Repositories;
using Newtonsoft.Json;
using Serendip.IK.DamageCompensations.Dto;
using Serendip.IK.DamageCompensationsFileInfo.Dto;
using Serendip.IK.Users;
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
        DamageCompensationFileInfoDto>,IDamageCompensationFileInfoAppService
    {


        #region Constructor


        private IUserAppService _userAppService;


        #endregion




        public DamageCompensationFileInfoAppService(IRepository<DamageCompensationFileInfo, long> repository, IUserAppService userAppService) : base(repository)
        {
            _userAppService = userAppService;

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



        public async  Task<string>  UpdateFileList(FileInfoDamage input) 
        {
            var datalist = Repository.GetAll().Where(x => x.DamageCompensationId == input.TazminId  && x.DosyaActive==true).ToList();

            var tazmindilekcesi = datalist.Where(x => x.DosyaTyp == 1).FirstOrDefault();
            // tazmindilekcesi ya dolu gelcek ya null gelcek. Dolu gelirse olan datayı guncelleyip active false cek 
            //sonra yeni gelen data varsa onu insert et 
            //eger tazmin dilekcesi null ise direk insert etmen yeterli
        

            if (input.FileTazminDilekcesi != null)
            {
                if(tazmindilekcesi == null)
                {
                    //direk kaydet 
                    //json cevir database kaydet
                    FileBase64 filestazmindilekce = new FileBase64();
                    filestazmindilekce = JsonConvert.DeserializeObject<FileBase64>(input.FileTazminDilekcesi);
                    CreateDamageCompensationFileInfoDto createDamageCompensationFileInfoDto = new CreateDamageCompensationFileInfoDto();
                    string[] name = filestazmindilekce.name.Split('.');
                    var guid = Guid.NewGuid().ToString("N");
                    string guidname = "" + name[0] + "-" + guid + "";
                    createDamageCompensationFileInfoDto.DosyaAdi = guidname;
                    createDamageCompensationFileInfoDto.DosyaUzantisi = filestazmindilekce.type;
                    createDamageCompensationFileInfoDto.DosyaYolu = @"/HasarTazmin/" + guidname + "." + name[1] + "";//sunucu tarafındaki yol
                    createDamageCompensationFileInfoDto.DamageCompensationId = Convert.ToInt32(input.TazminId); // tazmin id
                    createDamageCompensationFileInfoDto.DosyaTyp = 1;  // 1 tazmim dilekcesi
                    createDamageCompensationFileInfoDto.DosyaActive = true;
                    await base.CreateAsync(createDamageCompensationFileInfoDto);
                    UploadFile(filestazmindilekce.base64, "" + guidname + "." + name[1] + "");

                }
                else
                {
                    //db kayıtı pasife cek öyle kaydet
                    DamageCompensationFileInfoDto updatedata = new DamageCompensationFileInfoDto();
                    updatedata.Id = tazmindilekcesi.Id;
                    updatedata.DamageCompensationId = tazmindilekcesi.DamageCompensationId;
                    updatedata.DosyaAdi = tazmindilekcesi.DosyaAdi;
                    updatedata.DosyaYolu = tazmindilekcesi.DosyaYolu;
                    updatedata.CreationTime = tazmindilekcesi.CreationTime;
                    updatedata.CreatorUserId=tazmindilekcesi.CreatorUserId;
                    updatedata.LastModificationTime = DateTime.Now;
                    updatedata.DosyaActive = false;
                    updatedata.DosyaTyp = 1;
                    updatedata.DosyaUzantisi = tazmindilekcesi.DosyaUzantisi;
                    await base.UpdateAsync(updatedata);


                    //json cevir database kaydet
                    FileBase64 filestazmindilekce = new FileBase64();
                    filestazmindilekce = JsonConvert.DeserializeObject<FileBase64>(input.FileTazminDilekcesi);
                    CreateDamageCompensationFileInfoDto createDamageCompensationFileInfoDto = new CreateDamageCompensationFileInfoDto();
                    string[] name = filestazmindilekce.name.Split('.');
                    var guid = Guid.NewGuid().ToString("N");
                    string guidname = "" + name[0] + "-" + guid + "";
                    createDamageCompensationFileInfoDto.DosyaAdi = guidname;
                    createDamageCompensationFileInfoDto.DosyaUzantisi = filestazmindilekce.type;
                    createDamageCompensationFileInfoDto.DosyaYolu = @"/HasarTazmin/" + guidname + "." + name[1] + "";//sunucu tarafındaki yol
                    createDamageCompensationFileInfoDto.DamageCompensationId = Convert.ToInt32(input.TazminId); // tazmin id
                    createDamageCompensationFileInfoDto.DosyaTyp = 1;  // 1 tazmim dilekcesi
                    createDamageCompensationFileInfoDto.DosyaActive = true;
                    await base.CreateAsync(createDamageCompensationFileInfoDto);
                    UploadFile(filestazmindilekce.base64, "" + guidname + "." + name[1] + "");

                }
              
            }


           





            string test = "Başarılı";

            return test;
        }


    }
}
