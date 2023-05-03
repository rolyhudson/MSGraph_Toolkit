using BH.oM.Adapters.HTTP;
using BH.oM.Adapters.MSGraph;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;

namespace BH.Engine.Adapters.MSGraph
{
    public static partial class Compute
    {
        public static string GetRequest(GetRequest getrequest, string authToken)
        {
            HttpClient client = new HttpClient();
            
            var request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, getrequest.BaseUrl);
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
    }
}
