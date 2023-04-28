using BH.Adapter;
using BH.oM.Adapters.MSGraph;
using BH.oM.Base;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;

namespace BH.Adapter.MSGraph
{
    public partial class MSGraphAdapter : BHoMAdapter
    {
        private async Task Read(GetGroups request)
        {

           
        }

        private async Task Read(GetUserPeople request)
        {

            string content = await GetHttpContent(request.URL.Replace("id_userPrincipalName", request.UserId), Token);

            var details = JObject.Parse(content);
            if (details.ContainsKey("value"))
            {
                JArray results = details["value"].ToObject<JArray>();
                using (StreamWriter sw = new StreamWriter(Settings.CacheFolder + "\\People.json", true))
                {
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        writer.WriteStartObject();
                        writer.WritePropertyName("User");
                        writer.WriteValue(request.UserId);
                        writer.WritePropertyName("People");
                        writer.WriteStartArray();
                        foreach (var obj in results)
                        {
                            writer.WriteValue(obj["id"].ToString());
                        }
                        writer.WriteEnd();
                        writer.WriteEndObject();
                        writer.WriteRaw("\n");
                    }
                }
            }
        }

        private async Task Read(GetRooms request)
        {

            
        }

        private async Task Read(GetUser request)
        {

            string content = await GetHttpContent(request.URL.Replace("id_userPrincipalName", request.UserId), Token);
        }

        private async Task Read(GetUserManager request)
        {
            string content = await GetHttpContent(request.URL.Replace("id_userPrincipalName", request.UserId), Token);
            var details = JObject.Parse(content);
            if (details.ContainsKey("id"))
            {
                using (StreamWriter sw = new StreamWriter(Settings.CacheFolder + "\\Manager.json", true))
                {
                    using (JsonWriter writer = new JsonTextWriter(sw))
                    {
                        writer.WriteStartObject();
                        writer.WritePropertyName("User");
                        writer.WriteValue(request.UserId);
                        writer.WritePropertyName("Manager");
                        writer.WriteValue(details["id"].ToString());
                        writer.WriteEndObject();
                        writer.WriteRaw("\n");
                    }
                }
            }

                
        }

        private async Task Read(GetUsers request)
        {
            string content = await GetHttpContent(request.URL, Token);
            ParseUsers(content);
        }

        private async void ParseUsers(string content)
        {
            var details = JObject.Parse(content);
            List<object> objs = new List<object>();
            if (details.ContainsKey("value"))
            {
                JArray results = details["value"].ToObject<JArray>();
                foreach (var obj in results)
                {
                    BHoMObject bHoMObject = BH.Adapters.MSGraph.Convert.ToUser(obj);
                    if (bHoMObject != null)
                        objs.Add(bHoMObject);
                }
            }
            m_Results.AddRange(objs);
            using (StreamWriter sw = new StreamWriter(Settings.CacheFolder+"\\Users.json", true))
            {
                foreach (var obj in objs)
                    sw.WriteLine(JsonConvert.SerializeObject(obj));
            }

            if (details.ContainsKey("@odata.nextLink"))
                ParseUsers(await GetHttpContent(details["@odata.nextLink"].ToString(), Token));
            else
            {

                m_ReadComplete = true;
            }
        }

        private async Task<string> GetHttpContent(string url, string token)
        {
            var httpClient = new System.Net.Http.HttpClient();
            System.Net.Http.HttpResponseMessage response;
            try
            {
                var request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, url);
                //Add the token in Authorization header
                request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
                request.Headers.Add("consistencylevel", "eventual");
                response = await httpClient.SendAsync(request);
                var content = await response.Content.ReadAsStringAsync();

                return content;

            }
            catch (Exception ex)
            {
                Engine.Base.Compute.RecordError("A problem occurred with the request.");
                return "error";
            }
        }
    }
}
