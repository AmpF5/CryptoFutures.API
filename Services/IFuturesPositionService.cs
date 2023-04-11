using CryptoFutures.API.Entities;
using CryptoFutures.API.Models;

namespace CryptoFutures.API.Services;

public interface IFuturesPositionService
{
    public Task<FuturesPosition> OpenPosition(HttpContext httpContext, FuturesPositionRequestDto requestDto);
    public FuturesPositionResponseDto GetPosition(HttpContext httpContext, int positionId);
    public FuturesPositionResponseDto UpdateStopLoss(int id, decimal stopLoss);
    public FuturesPositionRequestDto UpdateTakeProfit(int id, decimal takeProfit);
    public FuturesPositionResponseDto ClosePosition(HttpContext httpcontext, int id);
}