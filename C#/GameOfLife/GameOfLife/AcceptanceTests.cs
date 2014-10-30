using System.Collections.Generic;
using System.Linq;
using System.Resources;
using NUnit.Framework;

namespace GameOfLife
{
    [TestFixture]
    public class AcceptanceTests
    {
        //Get the tests and internals of tests alligned
        //A starting point might be to change below to reflect either a dead world or a world with no live cells
        [Test]
        public void NoCellsInSeedMeansWorldIsDeadAfterTick()
        {
            var world = World.EmptyWorld();

            world.Tick();

            Assert.IsFalse(world.LiveCells().Any());
        }

        [Test]
        public void SetupWorldWithBlockPatternAndEnsureWorldStillHasBlockPatternAfterwards()
        //public void WorldWithBlockPatternAfterTickIsSymmetricalBlockPattern()
        //public void WorldWithBlockPatternAfterTickDoesNotChange()
        {
            var cells = new List<Cell>
            {
                new Cell(0, 0, State.Alive),
                new Cell(0, 1, State.Alive),
                new Cell(1, 1, State.Alive),
                new Cell(1, 0, State.Alive)
            };

            //World.Seed
            var world = new World(cells);

            world.Tick();

            //you aren't testing the right thing here but rather, doing a lip service to what you actually should be testing
            Assert.AreEqual(4, world.LiveCells().Count());
        }

        [Test]
        public void SetupWorldWithBlinkerPatternAndEnsureWorldHasOppositeBlinkPatternAfterwards()
        {
            var cells = new List<Cell>
            {
                new Cell(0, 0, State.Alive),
                new Cell(1, 0, State.Alive),
                new Cell(2, 0, State.Alive)
            };

            var world = new World(cells);

            world.Tick();


            var liveCells = world.LiveCells().ToList();
            AssertCellAlive(liveCells, 1, 1);
            AssertCellAlive(liveCells, 1, 0);
            AssertCellAlive(liveCells, 1, -1);
            //Assert cell count
        }



        [Test]
        public void GliderPatternWithOneTickEnsureWorldHasSecondGliderAfterwards()
        {
            var cells = new List<Cell>
            {
                new Cell(0, 0, State.Alive),
                new Cell(1, 0, State.Alive),
                new Cell(2, 0, State.Alive),
                new Cell(1, 2, State.Alive),
                new Cell(2, 1, State.Alive)
            };

            var world = new World(cells);

            world.Tick();

            AssertCellAlive(world.LiveCells(), 0, 1);
            AssertCellAlive(world.LiveCells(), 1, 0);
            AssertCellAlive(world.LiveCells(), 2, 0);
            AssertCellAlive(world.LiveCells(), 2, 1);
            AssertCellAlive(world.LiveCells(), 1, -1);
        }

        [Test]
        // Look at better name
        public void GliderPatternWithTwoTicksHasCorrectResult()
        {
            var cells = new List<Cell>
            {
                new Cell(0, 0, State.Alive),
                new Cell(1, 0, State.Alive),
                new Cell(2, 0, State.Alive),
                new Cell(1, 2, State.Alive),
                new Cell(2, 1, State.Alive)
            };

            var world = new World(cells);

            world.Tick();
            world.Tick();

            AssertCellAlive(world.LiveCells(), 0, 0);
            AssertCellAlive(world.LiveCells(), 1, -1);
            AssertCellAlive(world.LiveCells(), 2, -1);
            AssertCellAlive(world.LiveCells(), 2, 0);
            AssertCellAlive(world.LiveCells(), 2, 1);
        }

        private void AssertCellAlive(IEnumerable<Cell> liveCells, int x, int y)
        {
            Assert.AreEqual(State.Alive, liveCells.Single(c => c.X == x && c.Y == y).State);
        }
    }
}