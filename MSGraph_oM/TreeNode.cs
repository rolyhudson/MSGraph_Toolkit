using BH.oM.Base;
using System;
using System.Collections.Generic;
using System.Text;

namespace BH.oM.Adapters.MSGraph
{
    public class TreeNode : BHoMObject
    {
        public virtual List<TreeNode> Children { get; set; } = new List<TreeNode>();
    }
}
