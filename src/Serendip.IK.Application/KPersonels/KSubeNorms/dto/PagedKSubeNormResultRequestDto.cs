namespace Serendip.IK.KSubeNorms.dto
{
    public class PagedKSubeNormResultRequestDto
    {
        private string _keyword = "";
        public string Keyword
        {
            get
            {
                return _keyword.ToString();
            }
            set
            {
                this._keyword = value != null ? value : "";
            }
        }
        public bool? IsActive { get; set; }
        public long Id { get; set; }
    }
}
