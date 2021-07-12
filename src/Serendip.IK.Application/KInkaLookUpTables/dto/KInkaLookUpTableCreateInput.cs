using Abp.AutoMapper;

namespace Serendip.IK.KInkaLookUpTables.Dto
{
    [AutoMap(typeof(KInkaLookUpTable))]
    public class KInkaLookUpTableCreateInput
    {
        public string Adi { get; set; }
    }
}
