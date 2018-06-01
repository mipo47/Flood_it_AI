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
    public class ClearAColorLogic : AILogic
    {
        public override SuggestedMoves ChooseColor(Color[,] board)
        {
            MapNode head = MapBuilder.BuildMap(board);
            ISet<MapNode> firstLayer = head.GetNeighbors();
            List<Color> possibleColorsToClear = firstLayer.Select(node => node.Color).ToList();
            
            IEnumerator<MapNode> breathFirstSearch = head.BFS().GetEnumerator();

            while(breathFirstSearch.MoveNext() && possibleColorsToClear.Count > 0)
            {
                MapNode currentNode = breathFirstSearch.Current;
                if (!firstLayer.Contains(currentNode))
                {
                    //can't wipe out that color, it is behind the first layer
                    possibleColorsToClear.Remove(currentNode.Color);
                }
            }

            SuggestedMove move = new SuggestedMove(possibleColorsToClear);
            SuggestedMoves moves = new SuggestedMoves();
            moves.AddFirst(move);
            return moves;
        }

        public override void ChoseColor(Color color) {}
    }
}
