using Newtonsoft.Json;

namespace CryptoFutures.API.Services;

public class BalanceService : IBalanceService
{
    private readonly ICookieService _cookieService;
    public BalanceService(ICookieService cookieService)
    {
        _cookieService = cookieService;
    }
    public decimal GetBalance(HttpContext httpContext)
    {
        var balanceFromCookie = _cookieService.GetCookie(httpContext, "Balance");
        return JsonConvert.DeserializeObject<decimal>(balanceFromCookie);
    }

    public decimal SetBalance(HttpContext httpContext)
    {
        const decimal balance = 50000;
        var balanceSerialized = JsonConvert.SerializeObject(balance);
        _cookieService.SetCookie(httpContext, "Balance", balanceSerialized, 7);
        return balance;
    }

    public decimal UpdateBalance(HttpContext httpContext, decimal amount)
    {
        var balance =  GetBalance(httpContext);
        balance += amount;
        var serializedBalance = JsonConvert.SerializeObject(balance);
        _cookieService.SetCookie(httpContext, "Balance", serializedBalance, 7);
        return balance;
    }
}