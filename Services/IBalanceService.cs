namespace CryptoFutures.API.Services;

public interface IBalanceService
{
    public decimal GetBalance();

    public decimal SetBalance();

    public decimal UpdateBalance(decimal amount);
}