using Abp.Application.Navigation;
using System;
using System.Collections.Generic;
using System.Text;

namespace Serendip.IK.Utility
{
    public class TenantMenuSettings
    {
        public List<TenantMenuItem> MenuItems { get; set; } = new List<TenantMenuItem>();
    }

    public class TenantMenuItem
    {
        public string Name { get; set; }

        public int OrderIndex { get; set; }

        public string DisplayName { get; set; }

        public bool IsCustomName { get; set; }

        public bool IsActive { get; set; }

        public UserMenuItem MenuItem { get; set; }
    }
}
