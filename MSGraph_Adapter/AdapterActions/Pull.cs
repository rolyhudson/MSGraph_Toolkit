/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2022, the respective contributors. All rights reserved.
 *
 * Each contributor holds copyright over their respective contributions.
 * The project versioning (Git) records all such contribution source information.
 *                                           
 *                                                                              
 * The BHoM is free software: you can redistribute it and/or modify         
 * it under the terms of the GNU Lesser General Public License as published by  
 * the Free Software Foundation, either version 3.0 of the License, or          
 * (at your option) any later version.                                          
 *                                                                              
 * The BHoM is distributed in the hope that it will be useful,              
 * but WITHOUT ANY WARRANTY; without even the implied warranty of               
 * MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE. See the                 
 * GNU Lesser General Public License for more details.                          
 *                                                                            
 * You should have received a copy of the GNU Lesser General Public License     
 * along with this code. If not, see <https://www.gnu.org/licenses/lgpl-3.0.html>.      
 */
using BH.Engine.Adapters.MSGraph;
using BH.Engine.Base;
using BH.oM.Adapter;
using BH.oM.Adapter.Commands;
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
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BH.Adapter.MSGraph
{
    public partial class MSGraphAdapter
    {
        /***************************************************/
        /**** Public Methods                            ****/
        /***************************************************/

        public override IEnumerable<object> Pull(IRequest request, PullType pullType = PullType.AdapterDefault, ActionConfig actionConfig = null)
        {
            m_Results = new List<object>();
            if (request is GetRequest)
            {
                Read(request as GetRequest, Settings.Paginate);
                return m_Results;
            }
            else if (request is BatchRequest)
                return Pull(request as BatchRequest);
            Engine.Base.Compute.RecordError("This type of request is not supported. Use HTTP.GetRequest");
            return new List<object>();
        }


        public IEnumerable<object> Pull(BatchRequest requests)
        {
            string[] response = new string[requests.Requests.Count];
            List<BHoMObject> result = new List<BHoMObject>();
            using (HttpClient client = new HttpClient() { Timeout = TimeSpan.FromSeconds(Settings.TimeOut) })
            {
                List<GetRequest> getRequests = requests.Requests.OfType<GetRequest>().ToList();
                var tasks = getRequests.Select(x => BH.Engine.Adapters.MSGraph.Compute.GetRequestAsync(x, client, Token));
                response = Task.WhenAll(tasks).GetAwaiter().GetResult();
                client.CancelPendingRequests();
                client.Dispose();
            }
                
            Parallel.ForEach(response, res =>
            {
                if (res == null)
                    return;
                BHoMObject obj = Engine.Serialiser.Convert.FromJson(res) as BHoMObject;
                if (obj == null)
                {
                    Engine.Base.Compute.RecordNote($"{res.GetType()} failed to deserialise to a BHoMObject and is set to null." +
                        $"Perform a request with Compute.GetRequest(string url) if you want the raw output");
                    return; // return is equivalent to `continue` in a Parallel.ForEach
                }
                result.Add(obj);
            });
            return result;
        }

        private static List<object> m_Results = new List<object>();
    }
}

//namespace BH.Adapter.MSGraph
//{
//    public partial class MSGraphAdapter
//    {
//        /***************************************************/
//        /**** Public Methods                            ****/
//        /***************************************************/

//        public override IEnumerable<object> Pull(IRequest request, PullType pullType = PullType.AdapterDefault, ActionConfig actionConfig = null)
//        {

//            if (request is IMSGraphRequest)
//            {
//                m_Results = new List<object>();
//                m_ReadComplete = false;
//                IRead(request as IMSGraphRequest);
//                return new List<object>();
//            }

//            Engine.Base.Compute.RecordError("This type of request is not supported. Use BH.oM.HTTP.GetRequest");
//            return new List<object>();
//        }

//        /***************************************************/

//        

//        private static bool m_ReadComplete = false;

//    }
//}
