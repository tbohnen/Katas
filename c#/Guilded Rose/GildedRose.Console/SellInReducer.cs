namespace GildedRose.Console
{
    public class SellInReducer
    {
        private readonly StockTypeIdentifier _stockTypeIdentifier;

        public SellInReducer()
        {
            _stockTypeIdentifier = new StockTypeIdentifier();
        }


        private static void ReduceSellInByOne(Item item)
        {
            item.SellIn = item.SellIn - 1;
        }

        public void UpdateSellIn(Item item)
        {
            if (!_stockTypeIdentifier.IsSulfuras(item))
            {
                ReduceSellInByOne(item);
            }


        }
    }
}