using BoardGameLib;
using Model;
using System;
using UI;
using Xunit;

namespace BoardGameTest
{
    class TestView : IView
    {
        public WinEventArgs Result;
        public Color[,] Board;
        public Color Color;

        public void DisplayBoard(Color[,] board)
        {
            Board = board;
        }

        public void GameOver(WinEventArgs result)
        {
            Result = result;
        }

        public void ShowPickedColor(Color highestVote)
        {
            Color = highestVote;
        }
    }

    public class UnitTestSolver
    {
        [Fact]
        public void TestSolve()
        {
            var board = new Board(new Color[,] {
                { Color.Red, Color.Blue },
                { Color.Green, Color.Blue },
            });
            var game = new Game(board);

            var colors = game.GetUpdate();
            Assert.NotNull(colors);
            Assert.Equal(2, colors.GetLength(0));
            Assert.Equal(2, colors.GetLength(1));

            var testOutput = new TestView();
            var solver = new Solver(testOutput);

            solver.SolveStep(game);
            Assert.Equal(Color.Blue, testOutput.Color);

            solver.SolveStep(game);
            Assert.Equal(Color.Green, testOutput.Color);

            Assert.NotNull(testOutput.Result);
            Assert.Equal(2, testOutput.Result.Turns);
        }

        [Fact]
        public void TestSolveRandom()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            int size = random.Next(2, 11);
            var game = new Game(size);
            var solver = new Solver();

            solver.Solve(game);

            int maxTurnsPossible = size * size;
            Assert.True(game.Result.Turns < maxTurnsPossible, "Solution takes too many steps");
        }
    }
}
