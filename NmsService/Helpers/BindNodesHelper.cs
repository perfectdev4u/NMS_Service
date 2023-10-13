using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NmsService.Helpers
{
    public static class BindNodesHelper
    {
        public static void BindAllNodes()
        {
            DBDataContext context = new DBDataContext();
            Utility.NodeIdList.Clear();
            var nodes = context.NMS_GetAllNodes().ToList();
            foreach (var item in nodes)
            {
                Utility.NodeIdList.Add(item.NodeId, item.Id);
            }
        }
    }
}
