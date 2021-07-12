using Abp.AutoMapper;

namespace Serendip.IK.KInkaLookUpTables.Dto
{
    [AutoMap(typeof(KInkaLookUpTable))]
    public class CreateKInkaLookUpTableDto
    {
        public string Adi { get; set; }
    }
}
