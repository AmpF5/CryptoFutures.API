using CryptoFutures.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace CryptoFutures.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BalanceController : Controller
{
    private readonly IBalanceService _balanceService;
    private readonly ICookieService _cookieService;

    public BalanceController(IBalanceService balanceService, ICookieService cookieService)
    {
        _balanceService = balanceService;
        _cookieService = cookieService;
    }
}