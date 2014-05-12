using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using GameOfLife;

namespace GameOfLife.Tests
{
    public class World
    {
        public World(CellHolder cellHolder)
        {
            _cellHolder = cellHolder;
        }

        private CellHolder _cellHolder;

        public void SetCellStateAtLocation(Location location, CellState cellState)
        {
            //This might be a smell, I don't know who's responsibility it is to create the cell if the cell does not exist.
            //For now I will keep the responsibility with the location object but it might move to the world
            //And should not affect design too badly
            var cell = _cellHolder.GetCellAtLocationReturnNewIfDoesNotExist(location);
            cell.CellState = cellState;
        }

        public CellState GetCellStateAtLocation(Location location)
        {
            var cell = _cellHolder.GetCellAtLocationReturnNewIfDoesNotExist(location);

            if (cell == null) return CellState.Dead;

            return cell.CellState;
        }
    }
}
