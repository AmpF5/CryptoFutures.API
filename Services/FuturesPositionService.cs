
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
    public FuturesPosition OpenPosition(HttpContext httpContext, FuturesPositionRequestDto requestDto)
    {
        var position = _mapper.Map<Entities.FuturesPosition>(requestDto);
        var positionsFromCookie = _cookieService.GetCookie(httpContext, "FuturesPositions");
        List<FuturesPosition> positions;
        if(positionsFromCookie == null)
        {
        positions = new();
        }
        else
        {
        positions = JsonConvert.DeserializeObject<List<FuturesPosition>>(positionsFromCookie);
        }
        positions.Add(position);
        var serializedPositions = JsonConvert.SerializeObject(positions);
        _cookieService.SetCookie(httpContext, "FuturesPositions", serializedPositions, 7);
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