using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using ApprovalUtilities.SimpleLogger.Writers;

namespace GildedRose.Console
{
    public class Program
    {
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

        private void UpdateQuality()
        {
            foreach (var item in Items)
            {
                if (!IsAgedBrie(item) || !IsBackstagePass(item))
                {
                    if (QualityGreaterThanZero(item))
                    {
                        if (!IsSulfuras(item))
                        {
                            DecreaseQualityWithOne(item);
                        }
                    }
                }
                else
                {
                    if (QualityLessThanFifty(item))
                    {
                        IncreaseQualityWithOne(item);

                        if (IsBackstagePass(item))
                        {
                            if (SellInTenOrLess(item))
                            {
                                if (QualityLessThanFifty(item))
                                {
                                    IncreaseQualityWithOne(item);
                                }
                            }

                            if (item.SellIn < 6)
                            {
                                if (QualityLessThanFifty(item))
                                {
                                    IncreaseQualityWithOne(item);
                                }
                            }
                        }
                    }
                }

                if (!IsSulfuras(item))
                {
                    item.SellIn = item.SellIn - 1;
                }

                if (item.SellIn >= 0) continue;

                if (!IsAgedBrie(item))
                {
                    if (!IsBackstagePass(item))
                    {
                        if (QualityGreaterThanZero(item))
                        {
                            if (ItemNameDoesNotEqual(item, "Sulfuras, Hand of Ragnaros"))
                            {
                                DecreaseQualityWithOne(item);
                            }
                        }
                    }
                    else
                    {
                        item.Quality = item.Quality - item.Quality;
                    }
                }
                else
                {
                    if (item.Quality < 50)
                    {
                        IncreaseQualityWithOne(item);
                    }
                }
            }
        }

        private static bool SellInTenOrLess(Item item)
        {
            return item.SellIn < 11;
        }

        private static bool QualityLessThanFifty(Item item)
        {
            return item.Quality < 50;
        }

        private static bool QualityGreaterThanZero(Item item)
        {
            return item.Quality > 0;
        }

        private static bool IsSulfuras(Item item)
        {
            return ItemNameEquals(item, "Sulfuras, Hand of Ragnaros");
        }

        private static bool IsBackstagePass(Item item)
        {
            return ItemNameEquals(item, "Backstage passes to a TAFKAL80ETC concert");
        }

        private static bool IsAgedBrie(Item item)
        {
            return ItemNameEquals(item, "Aged Brie");
        }

        private static void IncreaseQualityWithOne(Item item)
        {
            item.Quality = item.Quality + 1;
        }

        private static void DecreaseQualityWithOne(Item item)
        {
            item.Quality = item.Quality - 1;
        }

        private static bool ItemNameEquals(Item item, string nameToCompare)
        {
            return item.Name == nameToCompare;
        }

        private static bool ItemNameDoesNotEqual(Item item, string nameToCompare)
        {
            return item.Name != nameToCompare;
        }
    }

    public class Item
    {
        public string Name { get; set; }

        public int SellIn { get; set; }

        public int Quality { get; set; }
    }

}
