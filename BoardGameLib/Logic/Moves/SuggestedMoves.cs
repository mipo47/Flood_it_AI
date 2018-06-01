using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Logic.Moves
{
    /// <summary>
    /// A series of multiple moves<br/>
    /// Each turn can have multiple suggested moves for it
    /// </summary>
    public class SuggestedMoves
    {
        /// <summary>
        /// The list of moves suggested for each turn
        /// </summary>
        public LinkedList<SuggestedMove> Moves { get; private set; }
        /// <summary>
        /// Gets the best Color for each turn based on weight
        /// </summary>
        public IEnumerable<Color> BestMoves
        {
            get
            {
                foreach(SuggestedMove move in Moves)
                {
                    IEnumerable<MoveWeight> bests = move.OrderedBest;
                    if (bests.Count() > 0)
                    {
                        yield return bests.First().Color;
                    }
                    else //if there aren't any suggested moves for the current turn, stop
                    {
                        yield break;
                    }
                }
            }
        }

        public SuggestedMoves() 
        {
            Moves = new LinkedList<SuggestedMove>();
        }

        public SuggestedMoves(Color color) : this()
        {
            Moves.AddLast(new SuggestedMove(color));
        }

        public void AddLast(SuggestedMove move)
        {
            Moves.AddLast(move);
        }

        public void AddFirst(SuggestedMove move)
        {
            Moves.AddFirst(move);
        }

        public SuggestedMove First()
        {
            return Moves.First.Value;
        }

        public void Intersect(SuggestedMoves moves)
        {
            IEnumerator<SuggestedMove> thisEnumerator = Moves.GetEnumerator();
            IEnumerator<SuggestedMove> otherEnumerator = moves.Moves.GetEnumerator();
            while (thisEnumerator.MoveNext() && otherEnumerator.MoveNext())
            {
                SuggestedMove thisSuggestedMove = thisEnumerator.Current;
                SuggestedMove otherSuggestedMove = otherEnumerator.Current;
                thisSuggestedMove.Intersect(otherSuggestedMove);
            }
            moves.Moves = null; //trash the other data
        }
    }
}
