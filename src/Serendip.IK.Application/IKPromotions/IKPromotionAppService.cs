using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Serendip.IK.Emails;
using Serendip.IK.Emails.Dto;
using Serendip.IK.IKPromotions.Dto;
using SuratKargo.Core.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.IKPromotions
{
    public class IKPromotionAppService : AsyncCrudAppService<IKPromotion, IKPromotionDto, long, PagedIKPromotionResultRequestDto, CreateIKPromotionDto, IKPromotionDto>, IIKPromotionAppService
    {
        private readonly IEmailAppService _emailAppService;
        public IKPromotionAppService(IRepository<IKPromotion, long> repository, IEmailAppService emailAppService) : base(repository)
        {
            _emailAppService = emailAppService;
        }


        #region CreateAsync
        public override Task<IKPromotionDto> CreateAsync(CreateIKPromotionDto input)
        {
            var result = base.CreateAsync(input);
            _emailAppService.SendV2(new EmailDto
            {
                Body = EmailConsts.IKPromotionRequestEmail(
                    new IKPromotionEmailDto
                    {
                        Title = input.Title,
                        FirstAndLastName = $"{input.FirstName} {input.LastName}",
                        RegistrationNumber = input.RegistrationNumber,
                        PositionRequestedForPromotion = input.PromotionRequestTitle,
                        EvaluateLink = "Test"

                    }),
                ToAddress = "emrecan.ayar@suratkargo.com.tr",
                Subject = "Terfi Talep İsteği",

            });
            return result;
        }
        #endregion

        #region GetAllAsync
        public override Task<PagedResultDto<IKPromotionDto>> GetAllAsync(PagedIKPromotionResultRequestDto input)
        {
            var data = base.GetAllAsync(input);
            return data;
        }
        #endregion

        [HttpGet]
        public async Task<List<IKPromotionFilterDto>> GetKPromotionFilterByDepartment(long departmentObjId)
        {
            var data = await Repository.GetAllListAsync(x => x.DepartmentObjId == departmentObjId);
            var result = ObjectMapper.Map<List<IKPromotionFilterDto>>(data);
            return result;

        }

        [HttpGet]
        public async Task<List<IKPromotionFilterDto>> GetKPromotionFilterByUnit(long unitObjId)
        {
            var data = await Repository.GetAllListAsync(x => x.UnitObjId == unitObjId);
            var result = ObjectMapper.Map<List<IKPromotionFilterDto>>(data);
            return result;

        }

        #region IsAnyPersonel
        [HttpGet]
        public async Task<bool> IsAnyPersonel(string registirationNumber)
        {
            var data = await Repository.GetAllListAsync(x => x.RegistrationNumber == registirationNumber && x.Statu == IKPromotionType.OnayaGonderildi);

            bool statu = data.Count > 0 ? true : false;
            return statu;
        }
        #endregion

    }
}