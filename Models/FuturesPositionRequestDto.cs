namespace CryptoFutures.API.Models;

public class FuturesPositionRequestDto
{
    public string Symbol { get; set; }
    public decimal Quanity { get; set; }
    public OrderType OrderType { get; set; }
    public int Leverage { get; set; }
}