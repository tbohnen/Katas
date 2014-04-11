using System.Collections.Generic;

namespace YieldReturnKata
{
    public class YieldReturnRunner
    {
        public IEnumerable<int> GetValue()
        {
            for (int i = 0; i < 100; i++)
            {
                yield return i;
            }
        }
    }
}