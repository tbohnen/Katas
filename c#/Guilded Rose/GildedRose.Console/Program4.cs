using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Text;

namespace GildedRose.Console
{
    public class Program4
    {
        private const int MaxQuality = 50;
        public IList<Item> Items;

        public static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var program = new Program4();
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

            UpdateQuality();
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

        public void UpdateQuality()
        {
            foreach (var inventoryItem in Items)
            {

                if (NameEquals(inventoryItem, "Aged Brie"))
                {
                    if (QualityLessThanMaxQuality(inventoryItem))
                    {
                        IncreaseQualityByOne(inventoryItem);
                    }

                    ReduceSellInByOne(inventoryItem);

                    continue;
                }


                if (NameEquals(inventoryItem, "Backstage passes to a TAFKAL80ETC concert"))
                {
                    if (QualityLessThanMaxQuality(inventoryItem))
                    {
                        IncreaseQualityByOne(inventoryItem);

                        if (inventoryItem.SellIn < 11)
                        {
                            if (QualityLessThanMaxQuality(inventoryItem))
                            {
                                IncreaseQualityByOne(inventoryItem);
                            }
                        }

                        if (inventoryItem.SellIn < 6)
                        {
                            if (QualityLessThanMaxQuality(inventoryItem))
                            {
                                IncreaseQualityByOne(inventoryItem);
                            }
                        }
                    }
                    else if (inventoryItem.Quality > 0)
                    {
                        ReduceQualityByOne(inventoryItem);
                    }

                    ReduceSellInByOne(inventoryItem);

                    continue;
                }

                if (inventoryItem.Quality > 0)
                {
                    if (!NameEquals(inventoryItem, "Sulfuras, Hand of Ragnaros"))
                    {
                        ReduceQualityByOne(inventoryItem);
                    }
                }

                if (!NameEquals(inventoryItem, "Sulfuras, Hand of Ragnaros"))
                {
                    ReduceSellInByOne(inventoryItem);
                }

                if (inventoryItem.SellIn >= 0) continue;

                inventoryItem.Quality = inventoryItem.Quality - inventoryItem.Quality;
            }
        }

        private static void ReduceSellInByOne(Item inventoryItem)
        {
            inventoryItem.SellIn = inventoryItem.SellIn - 1;
        }

        private static void IncreaseQualityByOne(Item inventoryItem)
        {
            inventoryItem.Quality = inventoryItem.Quality + 1;
        }

        private static void ReduceQualityByOne(Item inventoryItem)
        {
            inventoryItem.Quality = inventoryItem.Quality - 1;
        }

        private static bool QualityLessThanMaxQuality(Item inventoryItem)
        {
            return inventoryItem.Quality < MaxQuality;
        }

        private static bool NameEquals(Item inventoryItem, string name)
        {
            return inventoryItem.Name == name;
        }
    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}
