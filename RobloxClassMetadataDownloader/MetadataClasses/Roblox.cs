using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace RobloxClassMetadataDownloader.MetadataClasses
{
    [Serializable]
    [XmlRoot("roblox")]
    public class Roblox
    {
        [XmlArray("Item")]
        IList<Item> Items { get; set; }

    }
}
