using BH.oM.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BH.oM.Adapters.MSGraph
{
    public class User : BHoMObject
    {
        public virtual string DisplayName { get; set; }
        public virtual string GivenName { get; set; }
        public virtual string Surname { get; set; }
        public virtual string JobTitle { get; set; }
        public virtual string CompanyName { get; set; }
        public virtual string Department { get; set; }
        public virtual string OfficeLocation { get; set; }
        public virtual string Profession { get; set; }
        public virtual string Id { get; set; }
    }
}
