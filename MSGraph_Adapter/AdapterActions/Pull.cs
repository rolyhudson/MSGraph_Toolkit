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

            if (request is IMSGraphRequest)
            {
                m_Results = new List<object>();
                m_ReadComplete = false;
                Read(request as dynamic);
                return new List<object>();
            }

            Engine.Base.Compute.RecordError("This type of request is not supported. Use BH.oM.HTTP.GetRequest");
            return new List<object>();
        }

        /***************************************************/

        private static List<object> m_Results = new List<object>();

        private static bool m_ReadComplete = false;

    }
}
