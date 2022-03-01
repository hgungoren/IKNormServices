using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Serendip.IK.Emails.Dto
{
    public class IKPromotionEmailDto
    {
        public string RegistrationNumber { get; set; }
        public string FirstAndLastName { get; set; }
        public string Title { get; set; }
        public string PositionRequestedForPromotion { get; set; }
        public string EvaluateLink { get; set; }
    }
}
