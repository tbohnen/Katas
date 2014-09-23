using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace GildedRose.Console
{
    public class Program
    {
        public IList<Item> Items;
        private readonly SellInReducer _sellInReducer = new SellInReducer();
        private readonly QualityUpdater _qualityUpdater = new QualityUpdater();

        public static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var program = new Program();
            program.Run();

            System.Console.ReadKey();

        }

        public void Run()
        {
            Items = new List<Item>
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = 10, Quality = 20},
                new Item {Name = "Aged Brie", SellIn = 2, Quality = 0},
                new Item {Name = "Elixir of the Mongoose", SellIn = 5, Quality = 7},
                new Item {Name = "Sulfuras, Hand of Ragnaros", SellIn = 0, Quality = 80},
                new Item
                {
                    Name = "Backstage passes to a TAFKAL80ETC concert",
                    SellIn = 15,
                    Quality = 20
                },
                new Item {Name = "Conjured Mana Cake", SellIn = 3, Quality = 6}
            };

            UpdateItems();
            WriteOutput();
        }

        private void WriteOutput()
        {
            var sb = new StringBuilder();

            foreach (var item in Items)
            {
                sb.AppendFormat("{0}:{1}:{2}{3}", item.Name, item.Quality, item.SellIn, Environment.NewLine);
            }

            File.WriteAllText(String.Format("{0}\\output.txt", AppDomain.CurrentDomain.BaseDirectory), sb.ToString());
        }

        public void UpdateItems()
        {
            foreach (var item in Items)
            {
                _sellInReducer.UpdateSellIn(item);
                _qualityUpdater.UpdateQuality(item);
            }
        }

    }
}
