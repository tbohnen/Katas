using System;
using System.Collections.Generic;
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

                UpdateLocationOnGrid(alive, locationsWithNeighbourCount);
            }
        }

        private void UpdateLocationOnGrid(bool alive, KeyValuePair<Location, int> locationsWithNeighbourCount)
        {
            if (!alive && locationsWithNeighbourCount.Key.State == State.Alive)
            {
                _grid.RemoveAtLocation(locationsWithNeighbourCount.Key);
            }

            if (alive && locationsWithNeighbourCount.Key.State == State.Dead)
            {
                _grid.AddLiveLocationIfNotExists(locationsWithNeighbourCount.Key);
            }
        }

        public IEnumerable<Location> LiveLocations()
        {
            return _grid.LiveLocations();
        }
    }
}
