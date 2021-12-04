using System;

namespace Serendip.IK.DamageCompensations.Dto
{
    public class FilterDamageCompensationDto
    {
        public bool ChecktakipNo { get; set; }
        public bool ChecktazminId { get; set; }
        public long? Search { get; set; }
        public DateTime? Start { get; set; }
        public DateTime? Finish { get; set; }
    } 
}
