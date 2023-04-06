
using AutoMapper;
using CryptoFutures.API.Entities;
using CryptoFutures.API.Models;
using Newtonsoft.Json;

namespace CryptoFutures.API.Services;

public class FuturesPositionService : IFuturesPositionService
{
    private readonly IMapper _mapper;
    private readonly ICookieService _cookieService;
    public FuturesPositionService(IMapper mapper, ICookieService cookieService)
    {
        _mapper = mapper;
        _cookieService = cookieService;
    }
    public FuturesPosition OpenPosition(FuturesPositionRequestDto requestDto)
    {
        var position = _mapper.Map<Entities.FuturesPosition>(requestDto);
        return position;
    }

    public FuturesPositionRequestDto ClosePosition(int id)
    {
        throw new NotImplementedException();
    }

    public FuturesPositionResponseDto UpdateStopLoss(int id, decimal stopLoss)
    {
        throw new NotImplementedException();
    }

    public FuturesPositionRequestDto UpdateTakeProfit(int id, decimal takeProfit)
    {
        throw new NotImplementedException();
    }

    public FuturesPositionResponseDto GetPosition()
    {
        throw new NotImplementedException();
    }
}