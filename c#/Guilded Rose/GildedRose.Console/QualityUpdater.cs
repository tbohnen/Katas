namespace GildedRose.Console
{
    public class QualityUpdater
    {
        private readonly StockTypeIdentifier _stockTypeIdentifier = new StockTypeIdentifier();

        private const int MaximumQuality = 50;

        public void UpdateQuality(Item item)
        {
            if (_stockTypeIdentifier.IsAgedBrie(item))
            {
                UpdateAgedBrie(item);
                return;
            }

            if (_stockTypeIdentifier.IsBackstagePass(item))
            {
                UpdateBackstagePass(item);
                return;
            }

            if (_stockTypeIdentifier.IsSulfuras(item))
            {
                UpdateSulfuras(item);
                return;
            }

            if (_stockTypeIdentifier.IsConjured(item))
            {
                UpdateConjured(item);
                return;
            }

            UpdateNormalItem(item);
        }

        private void UpdateConjured(Item item)
        {
            if (QualityGreaterThanZero(item))
            {
                ReduceQualityByOne(item);
                ReduceQualityByOne(item);
            }
        }

        private static void UpdateNormalItem(Item item)
        {
            if (QualityGreaterThanZero(item))
            {
                ReduceQualityByOne(item);
            }
        }

        private void UpdateSulfuras(Item item)
        {
            if (!SellInGreaterThanZero(item)) return;

            ReduceQualityByOne(item);
        }

        private static void UpdateBackstagePass(Item item)
        {
            if (!QualityLessThanMaximumQuality(item)) return;

            if (!SellInLessThan(item, 11))
            {
                IncreaseQualityByOne(item);
                return;
            }

            IncreaseQualityByTwo(item);
        }

        private static void UpdateAgedBrie(Item item)
        {
            if (QualityLessThanMaximumQuality(item))
            {
                IncreaseQualityByOne(item);
            }
        }

        private static bool SellInLessThan(Item item, int value)
        {
            return item.SellIn < value;
        }

        private static void IncreaseQualityByTwo(Item item)
        {
            item.Quality = item.Quality + 2;
        }

        private static void IncreaseQualityByOne(Item item)
        {
            item.Quality = item.Quality + 1;
        }

        private static void ReduceQualityByOne(Item item)
        {
            item.Quality = item.Quality - 1;
        }

        private static bool QualityLessThanMaximumQuality(Item item)
        {
            return item.Quality < MaximumQuality;
        }

        private static bool SellInGreaterThanZero(Item item)
        {
            return item.SellIn < 0;
        }

        private static bool QualityGreaterThanZero(Item item)
        {
            return item.Quality > 0;
        }
    }
}