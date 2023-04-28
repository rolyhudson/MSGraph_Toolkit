using BH.oM.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace BH.oM.Adapters.MSGraph
{
    public class MSGraphSettings : BHoMObject
    {
        [Description("the content of ClientID with the Application Id for your app registration.")]
        public virtual string ClientId { get; set; } = "";

        [Description("The content of Tenant by the information about the accounts allowed to sign-in in your application.")]
        public virtual string Tenant { get; set; } = "";
        public virtual string Instance { get; set; } = "https://login.microsoftonline.com/";

        public virtual SignInMethod SignInMethod { get; set; } = SignInMethod.AnyAccount;

        public virtual string CacheFolder { get; set; }
    }
}
