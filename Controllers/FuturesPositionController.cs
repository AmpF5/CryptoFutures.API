using Microsoft.AspNetCore.Mvc;
using CryptoFutures.API.Services;
using CryptoFutures.API.Models;

namespace CryptoFutures.API.Controllers;

[ApiController]
[Route("api/controller")]
public class FuturesPositionController : Controller
{
    private readonly IFuturesPositionService _futuresPositionService;

  public FuturesPositionController(IFuturesPositionService futuresPositionService)
  {
    _futuresPositionService = futuresPositionService;
  }

  [HttpPost]
  public IActionResult OpenPosition([FromBody] FuturesPositionRequestDto requestDto)
  {
    if(!ModelState.IsValid) return BadRequest(ModelState);
    var position =_futuresPositionService.OpenPosition(requestDto);
    return Ok(position);
  }
}
