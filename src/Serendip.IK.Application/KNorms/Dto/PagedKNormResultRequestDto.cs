﻿using Abp.Application.Services.Dto;
using System;

namespace Serendip.IK.KNorms.Dto
{
    public class PagedKNormResultRequestDto : PagedAndSortedResultRequestDto
    {
        private string _keyword = "";
        public string Keyword
        {
            get
            {
                return _keyword.ToLower();
            }
            set
            {
                this._keyword = value != null ? value : "";
            }
        }

        public bool? IsActive { get; set; }
        public string Id { get; set; } = "0";
        public string BolgeId { get; set; }
        public string Type { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? End { get; set; }
    }
}
