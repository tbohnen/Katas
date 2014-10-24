using System.Collections.Generic;
using System.Linq;
using System.Resources;
using NUnit.Framework;

namespace GameOfLife
{
    [TestFixture]
    public class AcceptanceTests
    {
        [Test]
        public void NoCellsInSeedMeansWorldIsDeadAfterTick()
        {
            var world = new World(new List<Cell>());

            world.Tick();

            Assert.AreEqual(0,world.LiveCells().Count());
        }

        [Test]
        public void SetupWorldWithBlockPatternAndEnsureWorldStillHasBlockPatternAfterwards()
        {
            var cells = new List<Cell>
            {
                new Cell(0, 0, State.Alive),
                new Cell(0, 1, State.Alive),
                new Cell(1, 1, State.Alive),
                new Cell(1, 0, State.Alive)
            };

            var world = new World(cells);

            world.Tick();

            Assert.AreEqual(4,world.LiveCells().Count());
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
            AssertCellAlive(liveCells, 1,1);
            AssertCellAlive(liveCells, 1,0);
            AssertCellAlive(liveCells, 1,-1);

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
            Assert.AreEqual(State.Alive,liveCells.Single(c => c.X == x && c.Y == y).State);
        }
    }
}