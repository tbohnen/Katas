using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace GildedRose.Console
{
    public class Program
    {
        private const int UpperQualityLimit = 50;
        private const int UpperSellInLimit = 10;
        IList<Item> Items;
        static void Main(string[] args)
        {
            System.Console.WriteLine("OMGHAI!");

            var app = new Program();
            app.Run();

            System.Console.ReadKey();
        }

        public void Run()
        {
            Items = new List<Item>
            {
                new Item {Name = "+5 Dexterity Vest", SellIn = UpperSellInLimit, Quality = 20},
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
            foreach (var item in Items)
            {
                if (!IsAgedBrie(item) && !IsBackstagePass(item))
                {
                    DecreaseQualityIfQualityGreaterThanZeroAndNormalItem(item);
                }
                else
                {
                    if (QualityLessThanUpperQualityLimit(item))
                    {
                        IncreaseQualityWithOne(item);

                        ChangeSellInAndQualityOfBackstagePass(item);
                    }
                }

                if (!IsSulfuras(item))
                {
                    ReduceSellInByOne(item);
                }

                if (item.SellIn >= 0) continue;

                if (IsBackstagePass(item))
                {
                    HalfQualityIfGreaterThanZero(item);
                    return;
                }

                if (IsAgedBrie(item))
                {
                    if (item.Quality < UpperQualityLimit)
                    {
                        IncreaseQualityWithOne(item);
                    }
                    continue;
                }

                if (!QualityGreaterThanZero(item)) continue;

                if (!IsSulfuras(item))
                {
                    DecreaseQualityWithOne(item);
                }
            }
        }

        private static void ChangeSellInAndQualityOfBackstagePass(Item item)
        {
            if (IsBackstagePass(item))
            {
                if (item.SellIn <= UpperSellInLimit)
                {
                    if (QualityLessThanUpperQualityLimit(item))
                    {
                        IncreaseQualityWithOne(item);
                    }
                }

                if (item.SellIn >= 6) return;

                if (item.Quality < UpperQualityLimit)
                {
                    IncreaseQualityWithOne(item);
                }
            }
        }

        private static void DecreaseQualityIfQualityGreaterThanZeroAndNormalItem(Item item)
        {
            if (QualityGreaterThanZero(item))
            {
                if (!ItemEquals(item, "Sulfuras, Hand of Ragnaros"))
                {
                    DecreaseQualityWithOne(item);
                }
            }
        }

        private static bool QualityGreaterThanZero(Item item)
        {
            return item.Quality > 0;
        }

        private static bool QualityLessThanUpperQualityLimit(Item item)
        {
            return item.Quality < UpperQualityLimit;
        }

        private static void HalfQualityIfGreaterThanZero(Item item)
        {
            item.Quality = item.Quality - item.Quality;
        }

        private static bool IsSulfuras(Item item)
        {
            return (item.Name == "Sulfuras, Hand of Ragnaros");
        }

        private static bool IsBackstagePass(Item item)
        {
            return (item.Name == "Backstage passes to a TAFKAL80ETC concert");
        }

        private static bool IsAgedBrie(Item item)
        {
            return (item.Name == "Aged Brie");
        }

        private static void ReduceSellInByOne(Item item)
        {
            item.SellIn = item.SellIn - 1;
        }

        private static void IncreaseQualityWithOne(Item item)
        {
            item.Quality = item.Quality + 1;
        }

        private static void DecreaseQualityWithOne(Item item)
        {
            item.Quality = item.Quality - 1;
        }

        private static bool ItemEquals(Item item, string name)
        {
            return (item.Name == name);
        }
    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}
