namespace CryptoFutures.API.Services;

public interface IBalanceService
{
    public decimal GetBalance(HttpContext httpContext);

    public decimal SetBalance(HttpContext httpContext);

    public decimal UpdateBalance(HttpContext httpContext, decimal amount);
}