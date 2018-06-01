using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading;
using Model;

namespace Logic
{
    public class AILogicWeight
    {
        public AILogic Logic { get; private set; }
        public int Weight { get; private set; }
        public AILogicWeight(AILogic logic, int weight)
        {
            Logic = logic;
            Weight = weight;
        }
    }
}
