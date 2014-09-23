using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    class Location
    {
        private readonly int _x;
        private readonly int _y;

        public Location(int x, int y, State state)
        {
            _x = x;
            _y = y;
            _state = state;
        }

        public int X
        {
            get { return _x; }
        }

        public int Y
        {
            get { return _y; }
        }

        private State _state;

        public State State
        {
            get { return _state; }
            set { _state = value; }
        }

    }

    internal enum State
    {
        Dead = 0,
        Alive = 1
    }
}
