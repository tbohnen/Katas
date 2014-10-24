namespace GameOfLife
{
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
}