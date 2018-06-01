using Model;
using System;
using Xunit;

namespace BoardGameTest
{
    public class UnitTestGame
    {
        [Theory]
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(3)]
        [InlineData(5)]
        [InlineData(8)]
        public void TestGameCreate(int size)
        {
            var game = new Game(size);
            var board = game.GetUpdate();
            Assert.NotNull(board);
            Assert.Equal(size, board.GetLength(0));
            Assert.Equal(size, board.GetLength(1));
        }

        [Fact]
        public void TestPickColor()
        {
            var game = new Game(3);
            Color[,] board;

            Color colorStart, colorClosest;
            do
            {
                game.Reset();
                board = game.GetUpdate();

                colorStart = board[0, 0];
                colorClosest = board[0, 1];
            } while (colorStart == colorClosest);

            Assert.NotEqual(colorStart, colorClosest);

            game.PickColor(colorClosest);

            board = game.GetUpdate();
            colorStart = board[0, 0];
            colorClosest = board[0, 1];
            Assert.Equal(colorClosest, board[0, 0]);
        }
    }
}
