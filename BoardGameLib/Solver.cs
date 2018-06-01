using Logic;
using Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UI;

namespace BoardGameLib
{
    public class Solver
    {
        AILogicWeight[] logics;

        public IView OutputView { get; private set; }

        public Solver(IView output = null)
        {
            OutputView = output;

            logics = new AILogicWeight[]
            {
                new AILogicWeight(new IncreaseSurfaceAreaMapLogic(4), 1000),
                new AILogicWeight(new MoveTowardsFarthestNodeLogic(), 100),
                new AILogicWeight(new ClearAColorLogic(), 10000),
                new AILogicWeight(new AvoidColor(), 100),
                //new AILogicWeight(new RandomLogic(), 1),
                //new AILogicWeight(new IncreaseSurfaceAreaGridLogic(), 1),
                //new AILogicWeight(new HighestCount(), 1),
            };
        }

        public void Solve(Game game)
        {
            var board = game.GetUpdate();

            if (OutputView != null)
                OutputView.DisplayBoard(board);

            while (SolveStep(game)) ;
        }

        public bool SolveStep(Game game)
        {
            var board = game.GetUpdate();

            Dictionary<Color, int> colorVote = new Dictionary<Color, int>();
            foreach (AILogicWeight logic in logics)
            {
                var colorsChosen = logic.Logic.ChooseColor(board); //reaches across other thread to get the current Board

                if (colorsChosen.BestMoves.Any()) //if there are any moves returned
                {
                    Color color = colorsChosen.BestMoves.First();
                    if (!colorVote.ContainsKey(color))
                    {
                        colorVote.Add(color, 0);
                    }
                    colorVote[color] += logic.Weight;
                }
            }

            if (colorVote.Count > 0)
            {
                Color highestVote = colorVote.OrderByDescending(keyValuePair => keyValuePair.Value).First().Key;
                game.PickColor(highestVote);

                board = game.GetUpdate();

                if (OutputView != null)
                {
                    OutputView.ShowPickedColor(highestVote);
                    OutputView.DisplayBoard(board);
                }
            }
            else
            {
                return false;
            }

            if (game.Result != null) {
                if (OutputView != null)
                    OutputView.GameOver(game.Result);
                return false;
            }
            return true;
        }
    }
}
