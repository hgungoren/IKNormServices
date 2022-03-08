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

        #region FilterByDepartment
        [HttpGet]
        public async Task<List<IKPromotionFilterDto>> GetKPromotionFilterByDepartment(long departmentObjId)
        {
            var data = await Repository.GetAllListAsync(x => x.DepartmentObjId == departmentObjId);
            var result = ObjectMapper.Map<List<IKPromotionFilterDto>>(data);
            return result;

        }
        #endregion

        #region FilterByDepartmentCount
        [HttpGet]
        public async Task<int> GetKPromotionFilterByDepartmentCount(long departmentObjId)
        {
            var data = await Repository.GetAllListAsync(x => x.DepartmentObjId == departmentObjId);
            return data.Count;

        }
        #endregion

        #region FilterByUnit
        [HttpGet]
        public async Task<List<IKPromotionFilterDto>> GetKPromotionFilterByUnit(long unitObjId)
        {
            var data = await Repository.GetAllListAsync(x => x.UnitObjId == unitObjId);
            var result = ObjectMapper.Map<List<IKPromotionFilterDto>>(data);
            return result;

        }
        #endregion

        #region FilterByUnitCount
        [HttpGet]
        public async Task<int> GetKPromotionFilterByUnitCount(long unitObjId)
        {
            var data = await Repository.GetAllListAsync(x => x.UnitObjId == unitObjId);
            return data.Count;
        }
        #endregion

        #region IsAnyPersonel
        [HttpGet]
        public async Task<bool> IsAnyPersonel(string registirationNumber)
        {
            var data = await Repository.GetAllListAsync(x => x.RegistrationNumber == registirationNumber && x.Statu == IKPromotionType.OnayaGonderildi);

            bool statu = data.Count > 0 ? true : false;
            return statu;
        }
        #endregion

        #region GetAllStatus
        [HttpGet]
        public async Task<IKPromotionStatuDto> GetIKPromotionStatus()
        {
            var data = await Repository.GetAllListAsync();
            var result = data.Select(x => x.Statu).Distinct().ToList();
            return new IKPromotionStatuDto
            {
                Status = result
            };
        }
        #endregion

        #region GetAllTitles
        [HttpGet]
        public async Task<IKPromotionTitleDto> GetIKPromotionTitles()
        {
            var data = await Repository.GetAllListAsync();
            var result = data.Select(x => x.Title).Distinct().ToList();
            return new IKPromotionTitleDto
            {
                Titles = result
            };
        }
        #endregion

        #region GetAllRequestTitles
        [HttpGet]
        public async Task<IKPromotionRequestTitleDto> GetIKPromotionRequestTitles(string title)
        {
            var data = await Repository.GetAllListAsync(x => x.Title == title);
            var result = data.Select(x => x.PromotionRequestTitle).Distinct().ToList();
            return new IKPromotionRequestTitleDto
            {
                PromotionRequestTitles = result
            };
        }
        #endregion

        #region FilterData
        public async Task<List<IKPromotionFilterDto>> GetKPromotionFilterData(PromotionUseFilterDto promotionUseFilterDto)
        {
            var data = promotionUseFilterDto.DepartmentObjId == 0 ? await Repository.GetAllListAsync(x => x.UnitObjId == promotionUseFilterDto.UnitObjId) : await Repository.GetAllListAsync(x => x.DepartmentObjId == promotionUseFilterDto.DepartmentObjId);

            if (promotionUseFilterDto.Statu != 0)
            {
                data = data.Where(x => x.Statu == promotionUseFilterDto.Statu).ToList();
            }
            if (String.IsNullOrEmpty(promotionUseFilterDto.Title) != true)
            {
                data = data.Where(x => x.Title == promotionUseFilterDto.Title).ToList();
            }
            if (String.IsNullOrEmpty(promotionUseFilterDto.PromotionRequestTitle) != true)
            {
                data = data.Where(x => x.PromotionRequestTitle == promotionUseFilterDto.PromotionRequestTitle).ToList();
            }
            if (promotionUseFilterDto.FirstRequestDate != DateTime.MinValue && promotionUseFilterDto.SecondRequestDate != DateTime.MinValue)
            {
                data = data.Where(x => x.RequestDate.Date >= promotionUseFilterDto.FirstRequestDate.Date && x.RequestDate.Date <= promotionUseFilterDto.SecondRequestDate.Date).ToList();
            }

            var result = ObjectMapper.Map<List<IKPromotionFilterDto>>(data);
            return result;

        }
        #endregion

        #region GetPromotion
        public override async Task<IKPromotionDto> GetAsync(EntityDto<long> input)
        {
            var data = await base.GetAsync(input);
            return data;
        }

        #endregion

        #region HiearchyStatu
        public async Task<IKPromotionDto> GetIKPromotionHiearchyStatu(long id)
        {
            var data = await Repository.GetAsync(id);
            var result = ObjectMapper.Map<IKPromotionDto>(data);
            return result;
        }
        #endregion
    }
}
