/*
 * This file is part of the Buildings and Habitats object Model (BHoM)
 * Copyright (c) 2015 - 2023, the respective contributors. All rights reserved.
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

using BH.Adapter;
using BH.oM.Base.Attributes;
using System;
using System.ComponentModel;
using BH.oM.Adapters.MSGraph;
using MSGraph_Auth;
using BH.Adapter.HTTP;
using BH.oM.Base;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using BH.oM.Adapter.Commands;
using System.Threading;

namespace BH.Adapter.MSGraph
{
    public partial class MSGraphAdapter : BHoMAdapter
    {
        /***************************************************/
        /****             Public Properties             ****/
        /***************************************************/

        public static string Token { get; set; } = string.Empty;

        public MSGraphSettings Settings { get; set; } = new MSGraphSettings();

        /***************************************************/
        /**** Constructors                              ****/
        /***************************************************/

        [Description("Adapter for MSGraph.")]
        [Output("The created MSGraph adapter.")]
        public MSGraphAdapter(MSGraphSettings graphSettings , bool active = false, bool resetToken = false)
        {
            if (graphSettings == null)
                return;
            Settings = graphSettings;

            App.CreateApplication(graphSettings.ClientId, graphSettings.Tenant, graphSettings.Instance);
            if (!active )
                return;

            if (resetToken)
                Token = string.Empty;

            if (Token != string.Empty)
                return;
            //authenticate TODO check logic here sign out needed etc?
            Authenticate(graphSettings.SignInMethod);
        }
        //TODO dll and runtimes to BHoM Assemblies folder on build
        private async void Authenticate(SignInMethod signInMethod)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            Token = await mainWindow.Authenticate((int)signInMethod);
            if (string.IsNullOrEmpty(Token))
                //ADD BHoM record error
                return;
            mainWindow.Close();

            
        }

        /***************************************************/
        /**** Private Fields                            ****/
        /***************************************************/

        

        /***************************************************/
    }
}

