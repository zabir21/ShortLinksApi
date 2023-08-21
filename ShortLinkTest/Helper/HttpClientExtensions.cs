using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShortLinkTest.Helper
{
    public static class HttpClientExtensions
    {
        public static async Task<T?> GetContent<T>(this HttpContent httpContent)
        {
            var content = await httpContent.ReadAsStringAsync();
            var body = JsonConvert.DeserializeObject<T>(content);

            return body;
        }

        public static void AddAuthorizationBasicHeader(this HttpClient client, string? userName, string? password)
        {
            var bytes = Encoding.UTF8.GetBytes($"{userName}:{password}");
            var credentialBytes = Convert.ToBase64String(bytes);
            client.DefaultRequestHeaders.Add("Authorization", $"Basic {credentialBytes}");
        }
    }
}
