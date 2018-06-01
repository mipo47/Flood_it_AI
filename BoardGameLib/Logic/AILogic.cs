using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Model;
using Logic.Moves;

namespace Logic
{
    public abstract class AILogic
    {
        //protected AILogic() { }
        public abstract SuggestedMoves ChooseColor(Color[,] board);
        public abstract void ChoseColor(Color color);
    }
}
