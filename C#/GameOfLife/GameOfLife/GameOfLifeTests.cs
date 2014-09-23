using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;

namespace GameOfLife
{
    [TestFixture]
    class GameOfLifeTests
    {
        [Test]
        public void LiveCellWithFewerThanTwoNeighboursDies()
        {
            var world = new World();
            var locations = new[] { new Location(0, 0, State.Alive), new Location(0, 1, State.Alive) };

            world.Seed(locations);

            world.Tick();

            var locationsOfLiveCells = world.LiveLocations().Count();

            Assert.AreEqual(0, locationsOfLiveCells);
        }

        [Test]
        public void LiveCellWithTwoOrThreeNeighboursLivesOn()
        {
            var world = new World();
            var locations = new[] { 
                new Location(-1, 1, State.Alive), 
                new Location(1, 1, State.Alive), 
                new Location(1, -1, State.Alive) 
            };

            world.Seed(locations);

            world.Tick();

            var liveLocations = world.LiveLocations();
            var locationsOfLiveCells = liveLocations.Count();

            Assert.AreEqual(1, locationsOfLiveCells);
        }

        [Test]
        public void LiveCellWithMoreThanThreeNeigbhoursDies()
        {
            var world = new World();
            var locations = new[] { 
                new Location(-1, 1, State.Alive), 
                new Location(-1, -1, State.Alive), 
                new Location(1, -1, State.Alive),
                new Location(1, 1, State.Alive)
            };

            world.Seed(locations);

            world.Tick();

            var locationsOfLiveCells = world.LiveLocations().Count();

            Assert.AreEqual(0, locationsOfLiveCells);
        }

        [Test]
        public void DeadCellWithThreeNeighboursComesAlive()
        {
            var world = new World();
            var locations = new[] { 
                new Location(-1,1, State.Alive), 
                new Location(-1,-1, State.Alive), 
                new Location(1,0, State.Alive) 
            };

            world.Seed(locations);

            world.Tick();

            var locationsOfLiveCells = world.LiveLocations().Count();

            Assert.AreEqual(1, locationsOfLiveCells);
        }
    }
}
