using CryptoFutures.API.Enums;
namespace CryptoFutures.API.Models;

public class FuturesPositionResponseDto
{
    public int Id { get; set; }
    public string Symbol { get; set; }
    public decimal Price { get; set; }
    public decimal Quanity { get; set; }
    public OrderType OrderType { get; set; }
    // Date when position is closed?
    // public DateTime Date { get; set; } = DateTime.Now;
    public int Leverage { get; set; }
}