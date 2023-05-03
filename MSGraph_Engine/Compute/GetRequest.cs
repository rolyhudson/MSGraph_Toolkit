using BH.oM.Adapters.HTTP;
using BH.oM.Adapters.MSGraph;
using BH.oM.Data.Requests;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;


namespace BH.Engine.Adapters.MSGraph
{
    public static partial class Compute
    {
        public static string GetRequest(GetRequest getrequest, string authToken)
        {
            HttpClient client = new HttpClient();
            
            var request = new HttpRequestMessage(HttpMethod.Get, getrequest.BaseUrl);
            //Add the token in Authorization header
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
            request.Headers.Add("consistencylevel", "eventual");
            using (HttpResponseMessage response = client.SendAsync(request, HttpCompletionOption.ResponseContentRead).Result)
            {
                if (!response.IsSuccessStatusCode)
                {
                    Engine.Base.Compute.ClearCurrentEvents();
                    Engine.Base.Compute.RecordError($"GET request failed with code {response.StatusCode}: {response.ReasonPhrase}");
                    return null;
                }
                else
                {
                    string result = response.Content.ReadAsStringAsync().Result;
                    return result;
                }
            }
        }

        public static async Task<string> GetRequestAsync(GetRequest getrequest,HttpClient client, string authToken)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, getrequest.BaseUrl);
            //Add the token in Authorization header
            request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", authToken);
            request.Headers.Add("consistencylevel", "eventual");
            using (HttpResponseMessage response = await client.SendAsync(request, HttpCompletionOption.ResponseContentRead).ConfigureAwait(false))
            {
                string result = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    Engine.Base.Compute.ClearCurrentEvents();
                    Engine.Base.Compute.RecordError($"GET request failed with code {response.StatusCode}: {response.ReasonPhrase}");
                    return null;
                }
                else
                {
                    try
                    {
                        var jObject = JObject.Parse(result);
                        jObject["RequestUri"] = response.RequestMessage.RequestUri.ToString();
                        return jObject.ToString();
                    }
                    catch
                    {
                        return result;
                    }
                }
            }
        }
    }
}
