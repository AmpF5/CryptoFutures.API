
using System.Reflection.Metadata.Ecma335;
using AutoMapper;
using CryptoFutures.API.Entities;
using CryptoFutures.API.Models;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

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
    public async Task<FuturesPosition> OpenPosition(HttpContext httpContext, FuturesPositionRequestDto requestDto)
    {
        var positionsFromCookie = _cookieService.GetCookie(httpContext, "FuturesPositions");
        var position = _mapper.Map<Entities.FuturesPosition>(requestDto);
        List<FuturesPosition> positions;
        if(positionsFromCookie == null)
        {
        positions = new();
        }
        else
        {
        positions = JsonConvert.DeserializeObject<List<FuturesPosition>>(positionsFromCookie) ?? new List<FuturesPosition>();
        position.Id = positions.Count;
        }
        position.Price = await GetExternalPairPriceAsync();
        position.Total = position.Price * position.Quanity;
        positions.Add(position);
        var serializedPositions = JsonConvert.SerializeObject(positions);
        _cookieService.SetCookie(httpContext, "FuturesPositions", serializedPositions, 7);
        return position;
    }

    public FuturesPositionResponseDto ClosePosition(HttpContext httpContext, int id)
    {
        var positionsFromCookie = _cookieService.GetCookie(httpContext, "FuturesPositions");
        if(positionsFromCookie == null) return null;
        var positions = JsonConvert.DeserializeObject<List<FuturesPosition>>(positionsFromCookie);
        if(positions == null || positions.Count == 0    ) return null;
        // FIXME: - now its checking id by position in list but if u delete middle positon the order gonna chage
        // fix it so its checking position.id instead of position in list 
        var position = positions.FirstOrDefault(i => i.Id == id);
        if(position is not null) positions.Remove(position);
        var serializedPositions = JsonConvert.SerializeObject(positions);
        _cookieService.SetCookie(httpContext, "FuturesPositions", serializedPositions, 7);
        return _mapper.Map<FuturesPositionResponseDto>(position);
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
    public async Task<decimal> GetExternalPairPriceAsync()
    {
        using var httpClient = new HttpClient();
        const string url = "https://api.coingecko.com/api/v3/simple/price?ids=bitcoin&vs_currencies=usd";
        var response = await httpClient.GetAsync(url);
        if(response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(data);
            return(decimal)jsonObject["bitcoin"]["usd"];
        }
        else
        {
            throw new Exception($"Failed to get exchange rate. Status code: {response.StatusCode}");
        }
    }
}