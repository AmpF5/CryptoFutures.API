using Microsoft.AspNetCore.Mvc;
using CryptoFutures.API.Services;

namespace FuturesPosition.API.Controllers;

[ApiController]
[Route("api/controller")]
public class FuturesPositionController : Controller
{
    private readonly IFuturesPositionService _futuresPositionService;

  public FuturesPositionController(IFuturesPositionService futuresPositionService)
  {
    _futuresPositionService = futuresPositionService;
  }
}
