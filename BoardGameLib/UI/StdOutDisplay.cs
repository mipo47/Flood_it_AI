using System;
using System.Text;
using Model;
using View.Extentions;

namespace UI
{
    public class StdOutDisplay : IView
    {
        public void DisplayBoard(Color[,] board)
        {
            int estimatedLength = (board.GetLength(0) * board.GetLength(1)) * 2;
            StringBuilder sb = new StringBuilder(estimatedLength);
            for (int y = 0; y < board.Height(); y++)
            {
                for (int x = 0; x < board.Width(); x++)
                {
                    sb.Append((char)Board.ColorToLetter(board.GetAt(x, y)));
                    sb.Append(' ');
                }
                sb.AppendLine();
            }
            Console.WriteLine(sb);
        }

        public void GameOver(WinEventArgs e)
        {
            Console.WriteLine("Game Over. It took you {0} turns.", e.Turns);
        }

        public void ShowPickedColor(Color highestVote)
        {
            Console.WriteLine("Pick color: " + highestVote);
        }
    }
}
