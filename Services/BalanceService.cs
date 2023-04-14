using Newtonsoft.Json;

namespace CryptoFutures.API.Services;

public class BalanceService : IBalanceService
{
    private readonly ICookieService _cookieService;
    private readonly IHttpContextAccessor _httpContextAccessor;

    public BalanceService(ICookieService cookieService, IHttpContextAccessor httpContextAccessor)
    {
        _cookieService = cookieService;
        _httpContextAccessor = httpContextAccessor;
    }

    public decimal GetBalance()
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var balanceFromCookie = _cookieService.GetCookie(httpContext, "Balance");
        return JsonConvert.DeserializeObject<decimal>(balanceFromCookie);
    }

    public decimal SetBalance()
    {
        var httpContext = _httpContextAccessor.HttpContext;

        const decimal balance = 50000;
        var balanceSerialized = JsonConvert.SerializeObject(balance);
        _cookieService.SetCookie(httpContext, "Balance", balanceSerialized, 7);
        return balance;
    }

    public decimal UpdateBalance(decimal amount)
    {
        var httpContext = _httpContextAccessor.HttpContext;
        var balance = GetBalance();
        balance += amount;
        var serializedBalance = JsonConvert.SerializeObject(balance);
        _cookieService.SetCookie(httpContext, "Balance", serializedBalance, 7);
        return balance;
    }
}