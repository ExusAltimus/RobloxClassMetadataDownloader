using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Xml.Serialization;
using RobloxClassMetadataDownloader.MetadataClasses;
using System.IO;
using System.Text.RegularExpressions;
using RobloxClassMetadataDownloader.ApiDumpClasses;

namespace RobloxClassMetadataDownloader
{
   
    public class RobloxMetadataParser
    {
        public const string ROBLOX_RAW_METADATA_URL = "http://wiki.roblox.com/index.php?title=API:Class_reference/ReflectionMetadata/raw&action=raw";
        public const string ROBLOX_RAW_API_DUMP_URL = "http://wiki.roblox.com/index.php?title=API:Class_reference/Dump/raw&action=raw";

        public async void DownloadMetadata()
        {
            WebClient client = new WebClient();
            
            string rawMetadata = await client.DownloadStringTaskAsync(ROBLOX_RAW_METADATA_URL);
            // convert string to stream
            byte[] byteArray = Encoding.UTF8.GetBytes(rawMetadata);
            //byte[] byteArray = Encoding.ASCII.GetBytes(contents);
            MemoryStream rawMetadataStream = new MemoryStream(byteArray);

            XmlSerializer deserializer = new XmlSerializer(typeof(Roblox));
            object obj = deserializer.Deserialize(rawMetadataStream);
            Roblox XmlData = (Roblox)obj;
            rawMetadataStream.Close();
        }

        //http://wiki.roblox.com/index.php?title=API:Class_reference/Dump/Help
        private static string[] OrderedAttributes = new[]
        {
            "deprecated",
            "hidden",
            "readonly",
            "writeonly",
            "backend",
            "RobloxPlaceSecurity",
            "LocalUserSecurity",
            "WritePlayerSecurity",
            "RobloxScriptSecurity",
            "RobloxSecurity",
            "PluginSecurity"
        };

        public async Task<IList<RobloxClass>> DownloadApiDump()
        {
            var client = new CookieWebClient();
            client.Headers.Add("user-agent", "Mozilla/4.0 (compatible; MSIE 6.0; Windows NT 5.2; .NET CLR 1.0.3705;)");

            var response = await client.DownloadStringTaskAsync(ROBLOX_RAW_API_DUMP_URL);
            if (response.Contains("setCookie"))
            {
                MatchCollection cookieMatches = Regex.Matches(response, @"setCookie\('(.+)', '(.+)', (.+)\)");
                if (cookieMatches.Count > 0)
                {
                    var name = cookieMatches[0].Groups[1].Value;
                    var value = cookieMatches[0].Groups[2].Value;
                    var expireDays = int.Parse(cookieMatches[0].Groups[3].Value);
                    var cookie = new Cookie(name, value);
                    cookie.Domain = "wiki.roblox.com";
                    cookie.Expires = DateTime.Now.AddDays(expireDays);
                    client.CookieContainer.Add(cookie);
                }
                else
                {
                    throw new Exception("Can't parse cookie");
                }
                
            }

            response = await client.DownloadStringTaskAsync(ROBLOX_RAW_API_DUMP_URL);

            MatchCollection classMatches = Regex.Matches(response, @"Class (\w+)( : (\w+))?");
            IList<RobloxClass> robloxClasses = new List<RobloxClass>();
            foreach (Match match in classMatches)
            {
                string className = match.Groups[1].Value;
                string superClassName = match.Groups[3].Value;
                robloxClasses.Add(new RobloxClass()
                {
                    ClassName = className,
                    SuperClass = superClassName
                });
            }
            foreach (RobloxClass robloxClass in robloxClasses)
            {
                var attributeRegex = OrderedAttributes.Aggregate("", (seed, acc) => seed += $@"(\[{acc}\])?\s?");
                MatchCollection propertyMatches = Regex.Matches(response, $@"Property (\w+) { robloxClass.ClassName }\.(\w+)\s?{ attributeRegex }");
                foreach (Match match in propertyMatches)
                {
                    string dataType = match.Groups[1].Value;
                    string propertyName = match.Groups[2].Value;

                    var robloxProperty = new RobloxProperty()
                    {
                        Name = propertyName,
                        DataType = dataType,
                        Deprecated = !String.IsNullOrEmpty(match.Groups[3].Value),
                        Hidden = !String.IsNullOrEmpty(match.Groups[4].Value),
                        ReadOnly = !String.IsNullOrEmpty(match.Groups[5].Value),
                        WriteOnly = !String.IsNullOrEmpty(match.Groups[6].Value),
                        Backend = !String.IsNullOrEmpty(match.Groups[7].Value),
                        RobloxPlaceSecurity = !String.IsNullOrEmpty(match.Groups[8].Value),
                        LocalUserSecurity = !String.IsNullOrEmpty(match.Groups[9].Value),
                        WritePlayerSecurity = !String.IsNullOrEmpty(match.Groups[10].Value),
                        RobloxScriptSecurity = !String.IsNullOrEmpty(match.Groups[11].Value),
                        RobloxSecurity = !String.IsNullOrEmpty(match.Groups[12].Value),
                        PluginSecurity = !String.IsNullOrEmpty(match.Groups[13].Value)
                    };

                    if (robloxProperty.RobloxPlaceSecurity)
                    {
                        robloxProperty.IdentityLevels = new List<int>() { 3, 4, 5, 6, 7 };
                    }
                    else if (robloxProperty.PluginSecurity)
                    {
                        robloxProperty.IdentityLevels = new List<int>() { 3, 4, 5, 6, 7 };
                    }
                    else if (robloxProperty.LocalUserSecurity)
                    {
                        robloxProperty.IdentityLevels = new List<int>() { 4, 5, 7 };
                    }
                    else if (robloxProperty.RobloxScriptSecurity)
                    {
                        robloxProperty.IdentityLevels = new List<int>() { 4, 7 };
                    }
                    else
                    {
                        robloxProperty.IdentityLevels = new List<int>() { 1, 2, 3, 4, 5, 6, 7 };
                    }

                    robloxClass.Properties.Add(robloxProperty);
                }
            }


            return robloxClasses;
        }

        public string RobloxClassListToLuaLookupTable(IList<RobloxClass> robloxClasses)
        {
            var luaTable = String.Empty;

            foreach (RobloxClass robloxClass in robloxClasses)
            {
                var propertyTable = "\n";
                foreach (RobloxProperty robloxProperty in robloxClass.Properties)
                {

                    propertyTable = propertyTable + $"\t\t\t['{robloxProperty.Name}'] = {{ ['DataType'] = '{robloxProperty.DataType.ToString()}', ['Hidden'] = {robloxProperty.Hidden.ToString().ToLower()}, ['ReadOnly'] = {robloxProperty.ReadOnly.ToString().ToLower()}, ['Deprecated'] = {robloxProperty.Deprecated.ToString().ToLower()}, ['IdentityLevels'] = {{ {String.Join(", ", robloxProperty.IdentityLevels)} }} }},\n";
                }
                var classTable = $"\t['{robloxClass.ClassName}'] = {{\n\t\t['SuperClass'] = '{robloxClass.SuperClass}',\n\t\t['Properties'] = {{ {propertyTable}\n\t\t }}\n\t}},\n";
                luaTable = luaTable + classTable;
            }
            return "{\n" + luaTable + "}";
        }
    }
}
