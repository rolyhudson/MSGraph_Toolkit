using BH.oM.Base;
using BH.oM.Data.Requests;
using System;
using System.Collections.Generic;
using System.Text;

namespace BH.oM.Adapters.MSGraph
{
    public interface IMSGraphRequest : IRequest
    {
        string URL { get; set; }
    }
}
