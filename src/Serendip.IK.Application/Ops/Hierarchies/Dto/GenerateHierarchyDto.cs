

namespace Serendip.IK.Ops.Hierarchies.Dto
{
    public class GenerateHierarchyDto
    {

        private string tip;

        public long SubeId { get; set; }
        public long BolgeId { get; set; }
        public string Tip
        {
            get
            {
                return tip
                    .Replace("ı", "i")
                    .Replace("ç", "c")
                    .Replace("ş", "s")
                    .Replace("ğ", "g")
                    .Replace("ö", "g")
                    .Replace("ü", "u")
                    .Replace(" ", "")
                    .ToUpper();
            }
            set => tip = value.ToLower();
        }
        public string Pozisyon { get; set; }
    }
}
