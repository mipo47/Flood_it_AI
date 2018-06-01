using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;

namespace Logic.Moves
{
    /// <summary>
    /// A move and its weight
    /// </summary>
    public class MoveWeight
    {
        public Color Color { get; private set; }
        public int Weight { get; private set; }

        public MoveWeight(Color color, int weight)
        {
            Color = color;
            Weight = weight;
        }

        public override int GetHashCode()
        {
            return Color.GetHashCode();
        }
    }
}
