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
            var locations = new[] { new Cell(0, 0, State.Alive), new Cell(0, 1, State.Alive) };
            var world = new World(locations);

            world.Tick();

            var locationsOfLiveCells = world.LiveCells().Count();

            Assert.AreEqual(0, locationsOfLiveCells);
        }

        [Test]
        public void LiveCellWithTwoOrThreeNeighboursLivesOn()
        {
            var locations = new[] { 
                new Cell(-1, 1, State.Alive), 
                new Cell(1, 1, State.Alive), 
                new Cell(1, -1, State.Alive) 
            };

            var world = new World(locations);

            world.Tick();

            var liveLocations = world.LiveCells();
            var locationsOfLiveCells = liveLocations.Count();

            Assert.AreEqual(1, locationsOfLiveCells);
        }

        [Test]
        public void LiveCellWithMoreThanThreeNeigbhoursDies()
        {
            var locations = new[] { 
                new Cell(-1, 1, State.Alive), 
                new Cell(-1, -1, State.Alive), 
                new Cell(1, -1, State.Alive),
                new Cell(1, 1, State.Alive)
            };

            var world = new World(locations);

            world.Tick();

            var locationsOfLiveCells = world.LiveCells().Count();

            Assert.AreEqual(0, locationsOfLiveCells);
        }

        [Test]
        public void DeadCellWithThreeNeighboursComesAlive()
        {
            var locations = new[] { 
                new Cell(-1,1, State.Alive), 
                new Cell(-1,-1, State.Alive), 
                new Cell(1,0, State.Alive) 
            };

            var world = new World(locations);

            world.Tick();

            var locationsOfLiveCells = world.LiveCells().Count();

            Assert.AreEqual(1, locationsOfLiveCells);
        }
    }
}
