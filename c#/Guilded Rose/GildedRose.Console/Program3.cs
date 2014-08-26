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
            foreach (var item in Items)
            {
                if (!IsAgedBrie(item) && !IsBackstagePass(item))
                {
                    if (QualityGreaterThanZero(item) && !IsSulfuras(item))
                    {
                        UpdateItemQualityWith(item, -1);
                    }
                }
                else
                {
                    if (QualitySmallerThanUpperLimit(item))
                    {
                        UpdateItemQualityWith(item, 1);

                        if (IsBackstagePass(item))
                        {
                            if (SellInSmallerThanEqualToUpperLimit(item) && QualitySmallerThanUpperLimit(item))
                            {
                                UpdateItemQualityWith(item, 1);
                            }

                            if (item.SellIn < 6 && QualitySmallerThanUpperLimit(item))
                            {
                                UpdateItemQualityWith(item, 1);
                            }
                        }
                    }
                }

                if (!IsSulfuras(item))
                {
                    UpdateSellInWith(item, -1);
                }

                if (item.SellIn >= 0) continue;

                if (!IsAgedBrie(item))
                {
                    if (!IsBackstagePass(item) && item.Quality > 0)
                    {
                        if (!IsSulfuras(item))
                        {
                            UpdateItemQualityWith(item, -1);
                        }
                    }
                    else
                    {
                        UpdateItemQualityWith(item, -item.Quality);
                    }
                }
                else
                {
                    if (QualitySmallerThanUpperLimit(item))
                    {
                        UpdateItemQualityWith(item, 1);
                    }
                }
            }
        }

        private static void UpdateSellInWith(Item item, int numberToUpdateWith)
        {
            item.SellIn = item.SellIn + numberToUpdateWith;
        }

        private static bool QualitySmallerThanUpperLimit(Item item)
        {
            return item.Quality < UpperQualityLimit;
        }

        private static bool SellInSmallerThanEqualToUpperLimit(Item item)
        {
            return item.SellIn <= UpperSellInLimit;
        }

        private static bool QualityGreaterThanZero(Item item)
        {
            return item.Quality > 0;
        }

        private static void UpdateItemQualityWith(Item item, int qualityToAdd)
        {
            item.Quality = item.Quality + qualityToAdd;
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
    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}
