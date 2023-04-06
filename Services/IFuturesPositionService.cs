using CryptoFutures.API.Entities;
using CryptoFutures.API.Models;

namespace CryptoFutures.API.Services;

public interface IFuturesPositionService
{
    public FuturesPosition OpenPosition(FuturesPositionRequestDto requestDto);
    public FuturesPositionResponseDto GetPosition();
    public FuturesPositionResponseDto UpdateStopLoss(int id, decimal stopLoss);
    public FuturesPositionRequestDto UpdateTakeProfit(int id, decimal takeProfit);
    public FuturesPositionRequestDto ClosePosition(int id);
}