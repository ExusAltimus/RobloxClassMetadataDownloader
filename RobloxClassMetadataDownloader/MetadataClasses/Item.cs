using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RobloxClassMetadataDownloader.MetadataClasses
{
    public class Item
    {
        [XmlAttribute("class")]
        public string Class { get; set; }
    }
}
