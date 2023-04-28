using BH.oM.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BH.oM.Adapters.MSGraph
{
    public class GetUser : BHoMObject, IMSGraphRequest
    {
        public string URL { get; set; } = "https://graph.microsoft.com/v1.0/users/id_userPrincipalName";

        public string UserId { get; set; }

    }
}
