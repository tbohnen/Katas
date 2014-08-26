namespace GildedRose.Console
{
    public class StockTypeIdentifier
    {
        public bool IsSulfuras(Item item)
        {
            return item.Name == "Sulfuras, Hand of Ragnaros";
        }

        public bool IsBackstagePass(Item item)
        {
            return item.Name == "Backstage passes to a TAFKAL80ETC concert";
        }

        public bool IsAgedBrie(Item item)
        {
            return item.Name == "Aged Brie";
        }
    }
}