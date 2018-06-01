using System;
using System.Collections.Generic;
using System.Linq;
using Model;
using Logic;
using UI;

namespace BoardGameLib
{
    class Demo
    {
        const int BOARD_SIZE = 4;

        [STAThread]
        public static void Main(string[] args)
        {
            var guiDisplay = new StdOutDisplay();
            var game = new Game(BOARD_SIZE, BOARD_SIZE);
            var solver = new Solver(guiDisplay);

            solver.Solve(game);
            
            Console.WriteLine("Press any key to exit");
            Console.ReadKey(true);
        }
    }
}
