using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Model;

namespace UI
{
    public interface IView
    {
        void DisplayBoard(Color[,] board);
        void GameOver(WinEventArgs winEvent);
        void ShowPickedColor(Color highestVote);
    }
}
