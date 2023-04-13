using CryptoFutures.API.Entities;
using CryptoFutures.API.Models;

namespace CryptoFutures.API.Services;

public interface IFuturesPositionService
{
    public Task<FuturesPosition> OpenPosition(HttpContext httpContext, FuturesPositionRequestDto requestDto);

    public FuturesPosition GetPosition(HttpContext httpContext, int positionId);

    public List<FuturesPosition> GetPositions(HttpContext httpContext);

    public FuturesPosition UpdatePositionStopLossOrTakeProfit(HttpContext httpContext, int positionId, decimal stopLoss, decimal takeProfit);

    public FuturesPositionResponseDto ClosePosition(HttpContext httpcontext, int id);
}