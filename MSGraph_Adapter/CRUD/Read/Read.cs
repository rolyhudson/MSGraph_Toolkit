using BH.Adapter;
using BH.oM.Adapters.HTTP;
using BH.oM.Adapters.MSGraph;
using BH.oM.Base;
using BH.oM.Data.Requests;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Documents;
using System.Xml.Linq;

namespace BH.Adapter.MSGraph
{
    public partial class MSGraphAdapter : BHoMAdapter
    {
        private static List<object> Read(GetRequest getRequest, bool paginate)
        {
            string response = BH.Engine.Adapters.MSGraph.Compute.GetRequest(getRequest, Token);
            
            if (response == null)
                return m_Results;
            response = response.Replace("@odata.", "");
            // check if the response is a valid json
            if (response.StartsWith("{") || response.StartsWith("["))
            {
                var r = Engine.Serialiser.Convert.FromJson(response);
                if (r is CustomObject)
                {
                    CustomObject custom = (CustomObject)r;
                    //get the values
                    if (custom.CustomData.ContainsKey("value"))
                    {
                        //add to results
                        //var v = IList(custom.CustomData["value"]);
                        m_Results.AddRange(((IEnumerable)custom.CustomData["value"]).Cast<CustomObject>().ToList());
                        //list of customs
                    }
                    else
                        m_Results.Add(r);
                    if (custom.CustomData.ContainsKey("nextLink") && paginate)
                    { 
                        var nextlink = custom.CustomData["nextLink"].ToString();
                        GetRequest nextRequest = new GetRequest();
                        nextRequest.BaseUrl = nextlink;
                        //call this function and keep adding results
                        Read(nextRequest, paginate);
                        
                    }
                    
                }
                
                return m_Results;
            }

            else
                return new List<object>() { response };
        }

        /***************************************************/

       

        //private async Task<string> GetHttpContent(string url, string token)
        //{
        //    var httpClient = new System.Net.Http.HttpClient();
        //    System.Net.Http.HttpResponseMessage response;
        //    try
        //    {
        //        var request = new System.Net.Http.HttpRequestMessage(System.Net.Http.HttpMethod.Get, url);
        //        //Add the token in Authorization header
        //        request.Headers.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);
        //        request.Headers.Add("consistencylevel", "eventual");
        //        response = await httpClient.SendAsync(request);
        //        var content = await response.Content.ReadAsStringAsync();

        //        return content;

        //    }
        //    catch (Exception ex)
        //    {
        //        Engine.Base.Compute.RecordError("A problem occurred with the request.");
        //        return "error";
        //    }
        //}
    }
}
