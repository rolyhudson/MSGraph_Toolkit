using BH.Engine.Analytical;
using BH.Engine.Base;
using BH.oM.Adapters.MSGraph;
using BH.oM.Analytical.Elements;
using BH.oM.Data.Collections;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace BH.Engine.Adapters.MSGraph
{
    public static partial class Convert
    {
        public static TreeNode ToTreeNode(Graph graph, string rootName, string filepath)
        {
            //check graph is space node graph
            List<TreeNode> nodes = new List<TreeNode>();
            //set children on nodes
            Graph clone = graph.DeepClone();
            foreach (var entity in clone.Entities)
            {
                TreeNode node = new TreeNode(); // entity.Value as TreeNode;
                node.Name = entity.Value.Name;
                node.BHoM_Guid = entity.Value.BHoM_Guid;
                nodes.Add(node);
            }

            foreach(TreeNode treeNode in nodes)
            {
                var original = clone.Entity(treeNode.BHoM_Guid);
                foreach(var incoming in clone.Incoming(original))
                {
                    var child = nodes.FirstOrDefault(x => x.BHoM_Guid.Equals(incoming.BHoM_Guid));
                    treeNode.Children.Add(child);
                }
            }

            var options = new JsonSerializerOptions { WriteIndented = true, PropertyNamingPolicy = JsonNamingPolicy.CamelCase };

            var root = nodes.Find(x => x.Name == rootName);


            string gString = System.Text.Json.JsonSerializer.Serialize<TreeNode>(root, options);

            using (StreamWriter sw = new StreamWriter(filepath))
            {
                sw.WriteLine(gString);
            }

            return root;
        }

        
    }
}
