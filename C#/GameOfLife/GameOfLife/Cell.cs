using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    class Cell
    {
        private readonly int _x;
        private readonly int _y;

        public Cell(int x, int y, State state)
        {
            _x = x;
            _y = y;
            State = state;
        }

        public int X
        {
            get { return _x; }
        }

        public int Y
        {
            get { return _y; }
        }

        public State State { get; set; }
    }
}
