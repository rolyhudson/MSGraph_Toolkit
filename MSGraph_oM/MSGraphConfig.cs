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
using BH.oM.Adapter;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BH.oM.Adapters.MSGraph
{
    [Description("Define configuration settings for pulling MSGraph data using the MSGraph Adapter")]
    public class MSGraphConfig : ActionConfig
    {
        [Description("Endpoint for MSGraph queries. ")]
        public virtual string GraphEndpoint { get; set; } = "https://graph.microsoft.com/v1.0/users";

        [Description("the content of ClientID with the Application Id for your app registration.")]
        public virtual string ClientId { get; set; } = "";

        [Description("The content of Tenant by the information about the accounts allowed to sign-in in your application.")]
        public virtual string Tenant { get; set; } = "";
        public virtual string Instance { get; set; } = "https://login.microsoftonline.com/";
    }
}
