using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Logic.Moves
{
    /// <summary>
    /// A set of (weighted) colors suggested for a single move
    /// </summary>
    public class SuggestedMove
    {
        private HashSet<MoveWeight> suggestions = new HashSet<MoveWeight>();

        /// <summary>
        /// Orders the possible colors from best to worse
        /// </summary>
        public IEnumerable<MoveWeight> OrderedBest { get { return suggestions.OrderByDescending(move => move.Weight); } }

        public SuggestedMove() { }
        public SuggestedMove(Color color) : this(color, 1) { }
        public SuggestedMove(Color color, int weight)
        {
            AddSuggestion(color, weight);
        }

        public SuggestedMove(IEnumerable<Color> colors)
        {
            foreach (Color color in colors)
            {
                AddSuggestion(color, 1);
            }
        }

        public void AddSuggestion(Color color, int weight)
        {
            suggestions.Add(new MoveWeight(color, weight));
        }

        public int GetWeight(Color color)
        {
            return suggestions.Single(move => move.Color == color).Weight;
        }

        internal void Intersect(SuggestedMove otherSuggestedMove)
        {
            suggestions.Intersect(otherSuggestedMove.suggestions);
        }
    }
}
