using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class Location
    {
        public Location(int coordinateX, int coordinateY)
        {
            CoordinateX = coordinateX;
            CoordinateY = coordinateY;
        }

        public int CoordinateX { get; set; }

        public int CoordinateY { get; set; }
    }
}
