using CryptoFutures.API.Enums;

namespace CryptoFutures.API.Models;

public class FuturesPositionRequestDto
{
    public string Symbol { get; set; } = null!;
    public decimal PositionSize { get; set; }
    public OrderType OrderType { get; set; }
    public int Leverage { get; set; }
}