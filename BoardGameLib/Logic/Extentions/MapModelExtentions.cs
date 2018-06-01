using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Logic.MapModel;

namespace Logic.Extentions
{
    static class MapModelExtentions
    {
        public static IEnumerable<MapNode> BFS(this MapNode head)
        {
            Queue<MapNode> frontLine = new Queue<MapNode>();
            ISet<MapNode> visited = new HashSet<MapNode>();

            frontLine.Enqueue(head);
            while (frontLine.Count > 0)
            {
                MapNode visiting = frontLine.Dequeue();
                yield return visiting;
                visited.Add(visiting);

                foreach (MapNode neighbor in visiting.GetNeighbors())
                {
                    if (!visited.Contains(neighbor))
                        frontLine.Enqueue(neighbor);
                }
            }
        }

        public static IEnumerable<TreeNode> BFS(this TreeNode head)
        {
            Queue<TreeNode> frontLine = new Queue<TreeNode>();
            ISet<TreeNode> visited = new HashSet<TreeNode>();

            frontLine.Enqueue(head);
            while (frontLine.Count > 0)
            {
                TreeNode visiting = frontLine.Dequeue();
                yield return visiting;
                visited.Add(visiting);

                foreach (TreeNode neighbor in visiting.GetChildern())
                {
                    if (!visited.Contains(neighbor))
                        frontLine.Enqueue(neighbor);
                }
            }
        }

    }
}
