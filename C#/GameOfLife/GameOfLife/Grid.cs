using System.Collections.Generic;
using System.Linq;

namespace GameOfLife
{
    internal class Grid
    {
        private readonly List<Cell> _locations = new List<Cell>();

        public List<Cell> LiveLocations()
        {
            return _locations.Where(l => l.State == State.Alive).ToList();
        }

        public void AddLiveLocationIfNotExists(Cell cell)
        {
            var exists = _locations.Exists(l => l.X == cell.X && l.Y == cell.Y);

            if (!exists)
            {
                _locations.Add(cell);
            }

            _locations.First(l => l.X == cell.X && l.Y == cell.Y).State = State.Alive;
        }

        public Dictionary<Cell, int> GetAllCellsWithNeighbourCount()
        {
            var cellsWithNeighbourCount = new Dictionary<Cell, int>();
            var deadLocations = GetDeadLocationsWithAtLeastOneLiveNeighbour();
            var allLocations = new List<Cell>();

            allLocations.AddRange(deadLocations);
            allLocations.AddRange(_locations);

            foreach (var location in allLocations)
            {
                var neighbourCount = GetNeighbourCount(location);
                cellsWithNeighbourCount.Add(location, neighbourCount);
            }

            return cellsWithNeighbourCount;
        }

        private IEnumerable<Cell> GetDeadLocationsWithAtLeastOneLiveNeighbour()
        {
            var deadNeighbours = new List<Cell>();

            foreach (var location in _locations)
            {
                deadNeighbours = AddDeadLocationIfLiveLocationDoesNotExist(deadNeighbours, location.X, location.Y + 1);
                deadNeighbours = AddDeadLocationIfLiveLocationDoesNotExist(deadNeighbours, location.X + 1, location.Y + 1);
                deadNeighbours = AddDeadLocationIfLiveLocationDoesNotExist(deadNeighbours, location.X + 1, location.Y);
                deadNeighbours = AddDeadLocationIfLiveLocationDoesNotExist(deadNeighbours, location.X + 1, location.Y - 1);
                deadNeighbours = AddDeadLocationIfLiveLocationDoesNotExist(deadNeighbours, location.X, location.Y - 1);
                deadNeighbours = AddDeadLocationIfLiveLocationDoesNotExist(deadNeighbours, location.X - 1, location.Y - 1);
                deadNeighbours = AddDeadLocationIfLiveLocationDoesNotExist(deadNeighbours, location.X - 1, location.Y);
                deadNeighbours = AddDeadLocationIfLiveLocationDoesNotExist(deadNeighbours, location.X - 1, location.Y + 1);
            }
            return deadNeighbours;
        }

        private List<Cell> AddDeadLocationIfLiveLocationDoesNotExist(List<Cell> deadNeighbours, int x, int y)
        {
            if (!_locations.Any(l => l.X == x && l.Y == y) && !deadNeighbours.Any(l => l.X == x && l.Y == y))
            {
                deadNeighbours.Add(new Cell(x, y, State.Dead));
            }
            return deadNeighbours;
        }

        private int GetNeighbourCount(Cell cell)
        {
            int neighbourCount = 0;

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
            if (_locations.Any(l => l.X == neighbourX && l.Y == neighbourY))
                return neighbourCount + 1;

            return neighbourCount;
        }

        public void RemoveAtLocation(Cell cellToRemove)
        {
            var index = _locations.FindIndex(l => l.X == cellToRemove.X && l.Y == cellToRemove.Y);
            _locations.RemoveAt(index);
        }
    }
}