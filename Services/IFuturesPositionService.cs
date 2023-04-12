using CryptoFutures.API.Entities;
using CryptoFutures.API.Models;

namespace CryptoFutures.API.Services;

public interface IFuturesPositionService
{
    public Task<FuturesPosition> OpenPosition(HttpContext httpContext, FuturesPositionRequestDto requestDto);
    public FuturesPosition GetPosition(HttpContext httpContext, int positionId);
    public FuturesPosition UpdateStopLoss(HttpContext httpContext, int positionId, decimal stopLoss);
    public FuturesPosition UpdateTakeProfit(HttpContext httpContext, int positionId, decimal takeProfit);
    public FuturesPositionResponseDto ClosePosition(HttpContext httpcontext, int id);
}