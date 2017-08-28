using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RobloxClassMetadataDownloader.ApiDumpClasses
{
    public class RobloxProperty
    {
        public string Name { get; set; }
        public string DataType { get; set; }
        public bool Deprecated { get; set; }
        public bool Hidden { get; set; }
        public bool ReadOnly { get; set; }
        public bool WriteOnly { get; set; }
        public bool Backend { get; set; }

        public bool RobloxPlaceSecurity { get; set; }
        public bool LocalUserSecurity { get; set; }
        public bool WritePlayerSecurity { get; set; }
        public bool RobloxScriptSecurity { get; set; }
        public bool RobloxSecurity { get; set; }
        public bool PluginSecurity { get; set; }

        public IList<int> IdentityLevels { get; set; }
        public RobloxProperty()
        {
            IdentityLevels = new List<int>();
        }
    }
}
