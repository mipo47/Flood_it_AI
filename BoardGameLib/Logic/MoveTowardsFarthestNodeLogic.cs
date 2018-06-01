using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Logic.MapModel;
using Logic.Extentions;
using Logic.Moves;

namespace Logic
{
    public class MoveTowardsFarthestNodeLogic : AILogic
    {
        public override SuggestedMoves ChooseColor(Color[,] board)
        {
            return new SuggestedMoves(GetPath(board).Moves.First.Value.OrderedBest.First().Color);
        }

        public override void ChoseColor(Color color) { }

        public SuggestedMoves GetPath(Color[,] board)
        {
            //Get the farthest nodes
            TreeNode head = MapBuilder.BuildTree(board);
            ISet<TreeNode> farthestNodes = new HashSet<TreeNode>();
            int highestDepth = 0;
            foreach (TreeNode node in head.BFS()) //DFS would be better
            {
                int depth = GetDepth(node);
                if (depth > highestDepth)
                {
                    highestDepth = depth;
                    farthestNodes.Clear();
                    farthestNodes.Add(node);
                }
                else if (depth == highestDepth)
                {
                    farthestNodes.Add(node);
                }
            }

            //get the color that would step towards each color
            IDictionary<Color, int> tally = new Dictionary<Color, int>();
            foreach (TreeNode farthestNode in farthestNodes)
            {
                TreeNode currentNode = farthestNode;
                while (currentNode.Parent != head)
                {
                    currentNode = currentNode.Parent;
                }
                if (!tally.ContainsKey(currentNode.Color))
                {
                    tally.Add(currentNode.Color, 1);
                }
                else
                {
                    tally[currentNode.Color]++;
                }
            }
            SuggestedMoves suggestedMoves = new SuggestedMoves();
            suggestedMoves.AddFirst(new SuggestedMove(tally.OrderByDescending(kvp => kvp.Value).Select(n => n.Key)));
            return suggestedMoves;
        }

        private int GetDepth(TreeNode node)
        {
            int depth = 0;
            TreeNode current = node;
            while (current.Parent != null)
            {
                depth++;
                current = current.Parent;
            }
            return depth;
        }
    }
}
