using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    internal class NeighbourCounter
    {

        //Should I make cells a private variable or should I pass it through as arguments?
        //Thinking about clean code, it makes sense that all classes should use a private variable called cells
        //Which will point to high cohesion?

        //There is a lot that can be done with this class like maybe removing dry?
        private List<Cell> _cells;

        public Dictionary<Cell, int> GetAllCellsWithNeighbourCount(List<Cell> cells)
        {
            _cells = cells;
            var cellsWithNeighbourCount = new Dictionary<Cell, int>();
            var deadCells = GetDeadCellsWithAtLeastOneNeighbourToSeeIfTheyShouldComeAlive();
            var allCells = new List<Cell>();

            allCells.AddRange(deadCells);
            allCells.AddRange(cells);

            GetNeighbourCountForAllCells(allCells, cellsWithNeighbourCount);

            return cellsWithNeighbourCount;
        }

        private void GetNeighbourCountForAllCells(List<Cell> allCells, Dictionary<Cell, int> cellsWithNeighbourCount)
        {
            foreach (var cell in allCells)
            {
                var neighbourCount = GetNeighbourCount(cell);
                cellsWithNeighbourCount.Add(cell, neighbourCount);
            }
        }

        private List<Cell> GetDeadCellsWithAtLeastOneNeighbourToSeeIfTheyShouldComeAlive()
        {
            var deadNeighbours = new List<Cell>();

            foreach (var location in _cells)
            {
                deadNeighbours = AddDeadCellIfLiveCellDoesNotExist(deadNeighbours, location.X, location.Y + 1);
                deadNeighbours = AddDeadCellIfLiveCellDoesNotExist(deadNeighbours, location.X + 1, location.Y + 1);
                deadNeighbours = AddDeadCellIfLiveCellDoesNotExist(deadNeighbours, location.X + 1, location.Y);
                deadNeighbours = AddDeadCellIfLiveCellDoesNotExist(deadNeighbours, location.X + 1, location.Y - 1);
                deadNeighbours = AddDeadCellIfLiveCellDoesNotExist(deadNeighbours, location.X, location.Y - 1);
                deadNeighbours = AddDeadCellIfLiveCellDoesNotExist(deadNeighbours, location.X - 1, location.Y - 1);
                deadNeighbours = AddDeadCellIfLiveCellDoesNotExist(deadNeighbours, location.X - 1, location.Y);
                deadNeighbours = AddDeadCellIfLiveCellDoesNotExist(deadNeighbours, location.X - 1, location.Y + 1);
            }
            return deadNeighbours;
        }

        private List<Cell> AddDeadCellIfLiveCellDoesNotExist(List<Cell> deadNeighbours, int x, int y)
        {
            if (!_cells.Any(l => l.X == x && l.Y == y) && !deadNeighbours.Any(l => l.X == x && l.Y == y))
            {
                deadNeighbours.Add(new Cell(x, y, State.Dead));
            }
            return deadNeighbours;
        }

        private int GetNeighbourCount(Cell cell)
        {
            var neighbourCount = 0;

            neighbourCount = IncrementNeighbourCountIfNeighbourExistsAtLocation(neighbourCount, cell.X, cell.Y + 1);
            neighbourCount = IncrementNeighbourCountIfNeighbourExistsAtLocation(neighbourCount, cell.X + 1, cell.Y + 1);
            neighbourCount = IncrementNeighbourCountIfNeighbourExistsAtLocation(neighbourCount, cell.X + 1, cell.Y);
            neighbourCount = IncrementNeighbourCountIfNeighbourExistsAtLocation(neighbourCount, cell.X + 1, cell.Y - 1);
            neighbourCount = IncrementNeighbourCountIfNeighbourExistsAtLocation(neighbourCount, cell.X, cell.Y - 1);
            neighbourCount = IncrementNeighbourCountIfNeighbourExistsAtLocation(neighbourCount, cell.X - 1, cell.Y - 1);
            neighbourCount = IncrementNeighbourCountIfNeighbourExistsAtLocation(neighbourCount, cell.X - 1, cell.Y);
            neighbourCount = IncrementNeighbourCountIfNeighbourExistsAtLocation(neighbourCount, cell.X - 1, cell.Y + 1);

            return neighbourCount;
        }

        private int IncrementNeighbourCountIfNeighbourExistsAtLocation(int neighbourCount, int neighbourX, int neighbourY)
        {
            if (_cells.Any(l => l.X == neighbourX && l.Y == neighbourY))
                return neighbourCount + 1;

            return neighbourCount;
        }
    }
}