using CryptoFutures.API.Enums;

namespace CryptoFutures.API.Entities;
public class FuturesPosition
{
    public int Id { get; set; }
    public string Symbol { get; set; }
    public decimal Price { get; set; }
    public decimal Quanity { get; set; }
    public decimal Total { get; set; }
    public OrderType OrderType { get; set; }
    public DateTime Date { get; set; } = DateTime.Now;
    public int Leverage { get; set; }
    public decimal? StopLoss { get; set; }
    public decimal? TakeProfit { get; set; }
}
