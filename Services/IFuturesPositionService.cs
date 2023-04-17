using CryptoFutures.API.Entities;
using CryptoFutures.API.Models;

namespace CryptoFutures.API.Services;

public interface IFuturesPositionService
{
    public Task<FuturesPosition> OpenPosition(FuturesPositionRequestDto requestDto);

    public FuturesPosition GetPosition(int positionId);

    public List<FuturesPosition> GetPositions();

    public FuturesPosition UpdatePositionStopLossOrTakeProfit(int positionId, decimal stopLoss, decimal takeProfit);

    public Task<FuturesPositionResponseDto> ClosePosition(int id);
}