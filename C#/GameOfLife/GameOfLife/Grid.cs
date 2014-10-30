using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    internal class Grid
    {
        private readonly List<Cell> _cells = new List<Cell>();
        private readonly NeighbourCounter _neighbourCounter;

        public Grid()
        {
            _neighbourCounter = new NeighbourCounter();
        }

        public IEnumerable<Cell> LiveLocations()
        {
            return _cells.Where(l => l.State == State.Alive).ToList();
        }

        public Dictionary<Cell, int> GetAllCellsWithNeighbourCount()
        {
            return _neighbourCounter.GetAllCellsWithNeighbourCount(_cells);
        }

        public void ChangeCellState(Cell cell, bool shouldLive)
        {
            if (!shouldLive)
            {
                KillCellIfExists(cell);
            }

            if (shouldLive)
            {
                BringCellToLife(cell);
            }
        }

        public void BringCellToLife(Cell cell)
        {
            var exists = _cells.Exists(l => l.X == cell.X && l.Y == cell.Y);

            if (!exists)
            {
                _cells.Add(cell);
            }

            _cells.First(l => l.X == cell.X && l.Y == cell.Y).State = State.Alive;
        }

        private void KillCellIfExists(Cell cellToKill)
        {
            if (!CellExists(cellToKill)) return;

            KillCell(cellToKill);
        }

        private void KillCell(Cell cellToKill)
        {
            var index = _cells.FindIndex(l => l.X == cellToKill.X && l.Y == cellToKill.Y);
            _cells.RemoveAt(index);
        }

        private bool CellExists(Cell cellToKill)
        {
            return _cells.Exists(l => l.X == cellToKill.X && l.Y == cellToKill.Y);
        }
    }
}