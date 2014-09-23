using System;
using System.Collections.Generic;
using System.Linq;
using System.Resources;
using System.Text;

namespace GameOfLife
{
    class World
    {
        private readonly Grid _grid = new Grid();
        private readonly RuleEgine _ruleEngine = new RuleEgine();

        public void Seed(Location[] locations)
        {
            foreach (var location in locations)
            {
                _grid.AddLiveLocationIfNotExists(location);
            }
        }

        public void Tick()
        {
            var locationsWithNeighbourCounts = _grid.GetAllLocationsWithNeighbourCount();

            foreach (var locationsWithNeighbourCount in locationsWithNeighbourCounts)
            {
                var alive = _ruleEngine.IsAlive(locationsWithNeighbourCount.Value, locationsWithNeighbourCount.Key.State);


                if (!alive && locationsWithNeighbourCount.Key.State == State.Alive)
                {
                    _grid.RemoveAtLocation(locationsWithNeighbourCount.Key);
                }

                if (alive && locationsWithNeighbourCount.Key.State == State.Dead)
                {
                    _grid.AddLiveLocationIfNotExists(locationsWithNeighbourCount.Key);
                }

            }

        }

        public IEnumerable<Location> LiveLocations()
        {
            return _grid.LiveLocations();
        }
    }

    internal class RuleEgine
    {
        public bool IsAlive(int neighbourCount, State state)
        {
            if (state == State.Alive && neighbourCount < 2) return false;

            if (state == State.Alive && (neighbourCount == 2 || neighbourCount == 3)) return true;

            if (state == State.Alive && neighbourCount > 3) return false;

            if (state == State.Dead) return neighbourCount == 3;

            return false;
        }
    }

    internal class Grid
    {
        private readonly List<Location> _locations = new List<Location>();

        public List<Location> LiveLocations()
        {
            return _locations.Where(l => l.State == State.Alive).ToList();
        }

        public void AddLiveLocationIfNotExists(Location location)
        {
            var exists = _locations.Exists(l => l.X == location.X && l.Y == location.Y);

            if (!exists)
            {
                _locations.Add(location);
            }

            _locations.First(l => l.X == location.X && l.Y == location.Y).State = State.Alive;
        }

        public Dictionary<Location, int> GetAllLocationsWithNeighbourCount()
        {
            var locationsWithNeighbourCount = new Dictionary<Location, int>();
            var deadLocations = GetDeadLocationsWithAtLeastOneLiveNeighbour();
            var allLocations = new List<Location>();

            allLocations.AddRange(deadLocations);
            allLocations.AddRange(_locations);

            foreach (var location in allLocations)
            {
                var neighbourCount = GetNeighbourCount(location);
                locationsWithNeighbourCount.Add(location, neighbourCount);
            }

            return locationsWithNeighbourCount;
        }

        private List<Location> GetDeadLocationsWithAtLeastOneLiveNeighbour()
        {
            var deadNeighbours = new List<Location>();

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

        private List<Location> AddDeadLocationIfLiveLocationDoesNotExist(List<Location> deadNeighbours, int x, int y)
        {
            if (!_locations.Any(l => l.X == x && l.Y == y) && !deadNeighbours.Any(l => l.X == x && l.Y == y))
            {
                deadNeighbours.Add(new Location(x, y, State.Dead));
            }
            return deadNeighbours;
        }

        private int GetNeighbourCount(Location location)
        {
            int neighbourCount = 0;

            neighbourCount = IncrementNeighbourCountIfNeighbourExistsAtLocation(neighbourCount, location.X, location.Y + 1);
            neighbourCount = IncrementNeighbourCountIfNeighbourExistsAtLocation(neighbourCount, location.X + 1, location.Y + 1);
            neighbourCount = IncrementNeighbourCountIfNeighbourExistsAtLocation(neighbourCount, location.X + 1, location.Y);
            neighbourCount = IncrementNeighbourCountIfNeighbourExistsAtLocation(neighbourCount, location.X + 1, location.Y - 1);
            neighbourCount = IncrementNeighbourCountIfNeighbourExistsAtLocation(neighbourCount, location.X, location.Y - 1);
            neighbourCount = IncrementNeighbourCountIfNeighbourExistsAtLocation(neighbourCount, location.X - 1, location.Y - 1);
            neighbourCount = IncrementNeighbourCountIfNeighbourExistsAtLocation(neighbourCount, location.X - 1, location.Y);
            neighbourCount = IncrementNeighbourCountIfNeighbourExistsAtLocation(neighbourCount, location.X - 1, location.Y + 1);

            return neighbourCount;
        }

        private int IncrementNeighbourCountIfNeighbourExistsAtLocation(int neighbourCount, int neighbourX, int neighbourY)
        {
            if (_locations.Any(l => l.X == neighbourX && l.Y == neighbourY))
                return neighbourCount + 1;

            return neighbourCount;
        }

        public void RemoveAtLocation(Location locationToRemove)
        {
            var index = _locations.FindIndex(l => l.X == locationToRemove.X && l.Y == locationToRemove.Y);
            _locations.RemoveAt(index);
        }
    }
}
