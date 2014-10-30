using System;
using System.Collections.Generic;
using System.Resources;
using System.Text;
using NUnit.Framework;

namespace GameOfLife
{
    class World
    {
        //Is there a better word for Grid (Bring Cell to Life?)
        private readonly Grid _grid = new Grid();
        private readonly GenerationUpdater _generationUpdater;

        public static World EmptyWorld()
        {
            return new World(new List<Cell>());
        }

        public World(IEnumerable<Cell> cells)
        {
            Seed(cells);
            _generationUpdater = new GenerationUpdater();
        }

        private void Seed(IEnumerable<Cell> cells)
        {
            foreach (var location in cells)
            {
                _grid.BringCellToLife(location);
            }
        }

        public void Tick()
        {
            // Maybe be more functional, return a grid
            _generationUpdater.MoveToNextGeneration(_grid);
        }

        public IEnumerable<Cell> LiveCells()
        {
            return _grid.LiveLocations();
        }
    }
}
