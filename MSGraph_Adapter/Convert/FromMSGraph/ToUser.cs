using BH.oM.Adapters.MSGraph;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BH.Adapters.MSGraph
{
    public static partial class Convert
    {
        /***************************************************/
        /****           Public Methods                  ****/
        /***************************************************/
        public static User ToUser(JToken token)
        {
            User user = new User();

            user.Id = token["id"].ToString();
            user.BHoM_Guid = Guid.Parse(user.Id);
            user.Surname = token.Value<string>("surname") ?? "";
            user.DisplayName = token.Value<string>("displayName") ?? "";
            user.GivenName = token.Value<string>("givenName") ?? "";
            user.JobTitle = token.Value<string>("jobTitle") ?? "";
            user.CompanyName = token.Value<string>("companyName") ?? "";
            user.Department = token.Value<string>("department") ?? "";
            user.OfficeLocation = token.Value<string>("officeLocation") ?? "";
            user.Profession = token.Value<string>("profession") ?? "";
            return user;
        }
    }
}
