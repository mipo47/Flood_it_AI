using System;

namespace Model
{
	public class WinEventArgs : EventArgs
	{
		public int Turns { get; set; }
        public DateTime TimeStarted { get; set; }
        public DateTime TimeCompleted { get; set; }
        public FilledEventArgs FilledEvent { get; set; }
	}

	public delegate void DelWin(object sender, WinEventArgs args);

    public class Game
	{
		public event DelWin Winner;
        public event DelBoardUpdated BoardUpdated
        {
            add { _board.BoardUpdated += value; }
            remove { _board.BoardUpdated -= value; }
        }
		public int Turns { get; private set; }
        private DateTime _timeOfFirstMove;
		private readonly Board _board;

        public WinEventArgs Result { get; set; }

		public Game(int size) : this(size, size) { }
		public Game(int xSize, int ySize) : this(new Board(xSize, ySize)) { }

        public Game(Board board)
        {
            _board = board;
            _board.BoardFilled += (s, e) =>
            {
                Result = new WinEventArgs
                {
                    TimeCompleted = DateTime.Now,
                    TimeStarted = _timeOfFirstMove,
                    Turns = Turns,
                    FilledEvent = e
                };
                Winner?.Invoke(s, Result);
            };
        }

        public void PickColor(Color color)
		{
            if (Turns == 0)
                _timeOfFirstMove = DateTime.Now;
			Turns++;
			_board.Pick(color);
		}

        public Color[,] GetUpdate()
        {
            return _board.GetCopyOfBoard();
        }

        public void Reset()
        {
            _board.Reset();
            Turns = 0;
        }

        public override string ToString()
        {
            return _board.ToString();
        }
    }
}
