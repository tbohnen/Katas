using System.Collections.Generic;

namespace GameOfLife
{
    class GenerationUpdater
    {
        private readonly RuleEgine _ruleEngine = new RuleEgine();

        public void MoveToNextGeneration(Grid grid)
        {
            var cellsWithNeighbourCount = grid.GetAllCellsWithNeighbourCount();

            foreach (var cellWithNeighbourCount in cellsWithNeighbourCount)
            {
                var cell = cellWithNeighbourCount.Key;
                var neigbhourCount = cellWithNeighbourCount.Value;

                var lives = _ruleEngine.Lives(neigbhourCount, cell.State);

                grid.ChangeCellState(cell, lives);
            }
        }

    }
}