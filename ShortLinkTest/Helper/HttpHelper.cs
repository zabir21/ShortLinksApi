using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortLinkTest.Helper
{
    internal class HttpHelper
    {
        public static StringContent GetJsonHttpContent(object items)
        {
            return new StringContent(JsonConvert.SerializeObject(items), Encoding.UTF8, "application/json");
        }

        internal static class Urls
        {
            public readonly static string AddShortLink = "/api/shortlinks";
            public readonly static string GetFullUrl = "/api/shortlinks";
            public readonly static string GetAllFullUrl = "/api/shortlinks";
            public readonly static string DeleteShortLink = "/api/shortlinks";
            public readonly static string CreateTagForShortLink = "/api/shortlinks/tags";
            public readonly static string DeleteTagFromShortLink = "/api/shortlinks/tags";
            public readonly static string GetShortLinksWithTag = "/api/shortlinks/tags";
        }
    }
}
