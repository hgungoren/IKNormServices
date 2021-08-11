using System.Collections.Generic;

namespace Serendip.IK.Files.Dto
{
    public class StorageSizeAndLimitDto
    {
        public int TotalRowLimit { get; set; }
        public List<RowLimitDto> UsageRowLimit { get; set; }
        
    }
}