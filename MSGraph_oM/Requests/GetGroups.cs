using BH.oM.Base;
using System;
using System.Collections.Generic;
using System.Text;
using static System.Net.WebRequestMethods;

namespace BH.oM.Adapters.MSGraph
{
    public class GetGroups : BHoMObject, IMSGraphRequest
    {
        public string URL { get; set; } = "https://graph.microsoft.com/v1.0/groups";
    }
}
