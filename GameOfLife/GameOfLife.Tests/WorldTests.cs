using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameOfLife.Tests
{
    [TestFixture]
    public class WorldTests
    {
        [Test]
        public void HasEmptyWorld()
        {
            World world = new World(new CellHolder());

            Assert.IsNotNull(world);
        }

        [TestCase(CellState.Alive)]
        [TestCase(CellState.Dead)]
        public void SetCellStateAtLocation(CellState givenCellState)
        {
            World world = new World(new CellHolder());
            var location = new Location(0,0);

            world.SetCellStateAtLocation(location, givenCellState);

            CellState cellState = (CellState)world.GetCellStateAtLocation(location);

            Assert.AreEqual(givenCellState, cellState);
        }

        [Test]
        public void DefaultStateOfCellIsDead()
        {
            World world = new World(new CellHolder());
            var location = new Location(0,0);

            Assert.AreEqual(CellState.Dead, world.GetCellStateAtLocation(location));
        }

    }
}
