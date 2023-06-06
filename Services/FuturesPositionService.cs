using AutoMapper;
using CryptoFutures.API.Entities;
using CryptoFutures.API.Models;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CryptoFutures.API.Services;

public class FuturesPositionService : IFuturesPositionService
{
    private readonly IMapper _mapper;
    private readonly ICookieService _cookieService;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IBalanceService _balanceService;

    public FuturesPositionService(IMapper mapper, ICookieService cookieService, IHttpContextAccessor httpContextAccessor, IBalanceService balanceService)
    {
        _mapper = mapper;
        _cookieService = cookieService;
        _httpContextAccessor = httpContextAccessor;
        _balanceService = balanceService;
    }

    public async Task<FuturesPosition> OpenPosition(FuturesPositionRequestDto requestDto)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var positionsFromCookie = _cookieService.GetCookie(httpContext, "FuturesPositions");
        var position = _mapper.Map<FuturesPosition>(requestDto);
        List<FuturesPosition> positions;
        if (positionsFromCookie == null)
        {
            positions = new();
            //_balanceService.SetBalance();
        }
        else
        {
            positions = JsonConvert.DeserializeObject<List<FuturesPosition>>(positionsFromCookie) ?? new List<FuturesPosition>();
            //position.Id = positions.Count;
            if(positions.Count == 0)
            {
                position.Id = 0;
            }
            else
            {
                position.Id = positions[positions.Count - 1].Id + 1;
            }
        }
        // TODO: check if balance >= total price of position
        position.Price = await GetExternalPairPriceAsync();
        position.Quanity = position.PositionSize / position.Price;
        position.Total = position.Price * position.Quanity;
        ////position.PositionSize = position.Quanity * position.Price;
        if (position.Total >= _balanceService.GetBalance()) return null;
        _balanceService.UpdateBalance(-position.Total);
        positions.Add(position);
        var serializedPositions = JsonConvert.SerializeObject(positions);
        _cookieService.SetCookie(httpContext, "FuturesPositions", serializedPositions, 7);
        return position;
    }

    public async Task<FuturesPositionResponseDto> ClosePosition(int positionId)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var positions = GetPositions();
        var position = positions.Find(i => i.Id == positionId);
        if (position is not null)
        {
            //position.Total = 10000;
            var balance = await GetPositionProfit(position);
            _balanceService.UpdateBalance(balance);
            positions.Remove(position);
            //var serializedBalance = JsonConvert.SerializeObject(balance);
            //_cookieService.SetCookie(httpContext, "Balance", serializedBalance, 7);

        }
        else
        {
            return null;
        }

        var serializedPositions = JsonConvert.SerializeObject(positions);
        _cookieService.SetCookie(httpContext, "FuturesPositions", serializedPositions, 7);
        return _mapper.Map<FuturesPositionResponseDto>(position);
    }

    public FuturesPosition UpdatePositionStopLossOrTakeProfit(int positionId, decimal stopLoss, decimal takeProfit)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var positions = GetPositions();
        var position = positions.Find(i => i.Id == positionId);
        //TODO :add validation for stopLoss and takeProfit also
        //FIX :bug when user is not providing stopLoss or takeProfit so its assigns value to 0
        if (position is not null)
        {
            position.StopLoss = stopLoss;
            position.TakeProfit = takeProfit;
        }
        var serializedPositions = JsonConvert.SerializeObject(positions);
        _cookieService.SetCookie(httpContext, "FuturesPositions", serializedPositions, 7);
        return position;
    }

    public FuturesPosition GetPosition(int positionId)
    {
        var positions = GetPositions();
        return positions.Find(i => i.Id == positionId);
    }

    public List<FuturesPosition> GetPositions()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var positionsFromCookie = _cookieService.GetCookie(httpContext, "FuturesPositions");
        if (positionsFromCookie == null) return null;
        var positions = JsonConvert.DeserializeObject<List<FuturesPosition>>(positionsFromCookie);
        return positions;
    }
    private async Task<decimal> GetPositionProfit(FuturesPosition position)
    {
        decimal entryPrice = position.Price;
        decimal exitPrice = await GetExternalPairPriceAsync();
        exitPrice = 35000;
        decimal balance = ((1 / entryPrice) - (1 / exitPrice)) * position.PositionSize;
        // long position
        if (position.OrderType == Enums.OrderType.Long)
        {
            balance = balance * position.Leverage;
        }
        // short position
        if (position.OrderType == Enums.OrderType.Short)
        {
            balance = balance * position.Leverage * (-1);
        }
        return balance * exitPrice;
    }
    public async Task<decimal> GetExternalPairPriceAsync()
    {
        using var httpClient = new HttpClient();
        const string url = "https://api.coingecko.com/api/v3/simple/price?ids=bitcoin&vs_currencies=usd";
        var response = await httpClient.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var data = await response.Content.ReadAsStringAsync();
            var jsonObject = JObject.Parse(data);
            return (decimal)jsonObject["bitcoin"]["usd"];
        }
        else
        {
            throw new Exception($"Failed to get exchange rate. Status code: {response.StatusCode}");
        }
    }
}