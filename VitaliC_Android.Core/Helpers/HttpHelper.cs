using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace VitaliC_Android.Core.Helpers
{
    public class HttpHelper<T>
    {
        public async Task<T> GetAsync(string url)
        {
            var result = "";

            using (var httpClient = new HttpClient())
            {
                var response = await httpClient.GetAsync(url);
                result = await response.Content.ReadAsStringAsync();
            }

            return JsonConvert.DeserializeObject<T>(result);
        }
    }
}
