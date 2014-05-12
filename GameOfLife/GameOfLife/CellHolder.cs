using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GameOfLife
{
    public class CellHolder
    {
        private Dictionary<Location, Cell> _cells = new Dictionary<Location,Cell>();

        public Cell GetCellAtLocationReturnNewIfDoesNotExist(Location location)
        {
            var cellAtLocation = _cells
                .FirstOrDefault(cell => 
                    cell.Key.CoordinateX == location.CoordinateX &&
                    cell.Key.CoordinateY == location.CoordinateY)
                    .Value;

            cellAtLocation = CreateNewCellIfCellDoesNotExistAtLocation(location, cellAtLocation);

            return cellAtLocation;
        }

        private Cell CreateNewCellIfCellDoesNotExistAtLocation(Location location, Cell cellAtLocation)
        {
            if (cellAtLocation == null)
            {
                cellAtLocation = new Cell();
                _cells.Add(location, cellAtLocation);
            }
            return cellAtLocation;
        }
    }
}
