﻿using Abp.Application.Services;
using Abp.Application.Services.Dto;
using Abp.Domain.Repositories;
using Microsoft.AspNetCore.Mvc;
using Serendip.IK.SKJobs.Dto;
using Serendip.IK.SKJobs.Dto.RequestDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.SKJobs
{
    public class SKJobsAppService : AsyncCrudAppService<SKJobs, SKJobDto, long, PagedJobResultRequestDto, CreateJobsDto, SKJobDto>, ISKJobsAppService
    {
        public SKJobsAppService(IRepository<SKJobs, long> repository) : base(repository)
        {
        }


        #region GetAllPositionForTitle
        public async Task<IList<SKJobsPromoteListDto>> GetAllPositionForTitle(SKJobsPromoteRequestDto sKJobsPromoteRequestDto)
        {
            List<SKJobsPromoteListDto> datas = new List<SKJobsPromoteListDto>();
            var title = await Repository.GetAllListAsync(x => x.ObjId == sKJobsPromoteRequestDto.ObjId);
            var priorty = title[0].Durum;
            var result = await Repository.GetAllListAsync(x => x.BirimObjId == sKJobsPromoteRequestDto.BirimObjId);
            result = result.Where(x => x.Durum > priorty).ToList();

            foreach (var item in result)
            {
                datas.Add(new SKJobsPromoteListDto
                {
                    Durum = item.Durum,
                    Adi = item.Adi
                });
            }

            return datas;
        }
        #endregion


        #region GetAllPositionForUnit
        [HttpGet]
        public async Task<List<SKJobsNameDto>> GetAllPositionForUnit(long unitObjId)
        {
            List<SKJobsNameDto> datas = new List<SKJobsNameDto>();
            var names = await Repository.GetAllListAsync(x => x.BirimObjId == unitObjId);
            foreach (var item in names)
            {
                if (String.IsNullOrEmpty(item.Adi) != true)
                {
                    datas.Add(new SKJobsNameDto
                    {
                        Adi = item.Adi,
                    });
                }
            }
            return datas;
        }
        #endregion
    }
}
