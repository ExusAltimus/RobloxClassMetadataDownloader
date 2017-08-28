using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace RobloxClassMetadataDownloader.ApiDumpClasses
{

    public class RobloxClass
    {
        public string ClassName { get; set; }
        public string SuperClass { get; set; }
        public IList<RobloxProperty> Properties { get; set; }

        public RobloxClass()
        {
            Properties = new List<RobloxProperty>();
        }
    }
}
