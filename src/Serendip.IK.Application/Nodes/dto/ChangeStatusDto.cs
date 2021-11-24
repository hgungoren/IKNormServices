﻿using System.Collections.Generic;

namespace Serendip.IK.Nodes.dto
{
    public class ChangeStatuToPassiveDto
    {
        public long PositionId { get; set; }
    }


    public class ChangeStatusDto
    {
        public long Id { get; set; }
        public string Type { get; set; }
        public bool Status { get; set; }
    }

    public class ChangeSelectedDto
    {
        public long Id { get; set; }
        public long PositionId { get; set; }
        public bool Selected { get; set; }
    }
}
