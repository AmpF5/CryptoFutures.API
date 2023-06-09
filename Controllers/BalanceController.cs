using CryptoFutures.API.Services;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;

namespace CryptoFutures.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class BalanceController : Controller
{
    private readonly IBalanceService _balanceService;

    public BalanceController(IBalanceService balanceService)
    {
        _balanceService = balanceService;
    }

    [HttpGet]
    public IActionResult GetBalance()
    {
        var balance = _balanceService.GetBalance();
        return Ok(balance);
    }

    [HttpPut]
    public IActionResult UpdateBalance([FromQuery][Required] decimal amount)
    {
        var balance = _balanceService.UpdateBalance(amount);
        return Ok(balance);
    }

    [HttpPost]
    public IActionResult SetBalance()
    {
        var balance = _balanceService.SetBalance();
        return Ok(balance);
    }
}