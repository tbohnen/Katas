using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YieldReturnKata;

namespace YieldReturnKata
{
    class Program
    {
        static void Main(string[] args)
        {
            var runner = new YieldReturnRunner();
            foreach (var value in runner.GetValue())
            {
                Console.WriteLine(value);
            } 
            Console.ReadKey();
        }
    }
}
