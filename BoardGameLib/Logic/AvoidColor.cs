using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;
using Logic.Moves;

namespace Logic
{
    public class AvoidColor : AILogic
    {
        private LinkedList<Color> recentColors = new LinkedList<Color>();
        public override SuggestedMoves ChooseColor(Model.Color[,] board)
        {
            SuggestedMove move = new SuggestedMove();
            foreach(Color color in Enum.GetValues(typeof(Color)))
            {
                if (!recentColors.Contains(color))
                {
                    move.AddSuggestion(color, 100);
                }
            }
            int index = 1;
            foreach (Color color in recentColors)
            {
                move.AddSuggestion(color, index++);
            }

            var moves = new SuggestedMoves();
            moves.AddFirst(move);
            return moves;
        }

        public override void ChoseColor(Color color)
        {
            //remove all instances of color from the list
            while (recentColors.Remove(color)) { }
            recentColors.AddFirst(color);
        }
    }
}
