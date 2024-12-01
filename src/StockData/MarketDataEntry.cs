namespace StockData
{
    public record MarketDataEntry(DateTime DateTime, decimal Open, decimal High, decimal Low, decimal Close, int Volume)
    {
    }
}
