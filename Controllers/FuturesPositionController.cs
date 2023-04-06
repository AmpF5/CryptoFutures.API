using Microsoft.AspNetCore.Mvc;
using CryptoFutures.API.Services;
using CryptoFutures.API.Models;
using Newtonsoft.Json;
using CryptoFutures.API.Entities;

namespace CryptoFutures.API.Controllers;

[ApiController]
[Route("api/controller")]
public class FuturesPositionController : Controller
{
    private readonly IFuturesPositionService _futuresPositionService;
    private readonly ICookieService _cookieService;

  public FuturesPositionController(IFuturesPositionService futuresPositionService, ICookieService cookieService)
  {
    _futuresPositionService = futuresPositionService;
    _cookieService = cookieService;
  }

  [HttpPost]
  public async Task<IActionResult> OpenPosition([FromBody] FuturesPositionRequestDto requestDto)
  {
    if(!ModelState.IsValid) return BadRequest(ModelState);
    // var positionsFromCookie = _cookieService.GetCookie(HttpContext, "FuturesPositions");
    // List<FuturesPosition> positions;
    // if(positionsFromCookie == null)
    // {
    //   positions = new();
    // }
    // else
    // {
    //   positions = JsonConvert.DeserializeObject<List<FuturesPosition>>(positionsFromCookie);
    // }
    // var position =_futuresPositionService.OpenPosition(requestDto);
    // positions.Add(position);
    // var serializedPositions = JsonConvert.SerializeObject(positions);
    // _cookieService.SetCookie(HttpContext, "FuturesPositions", serializedPositions, 7);
    var position = await _futuresPositionService.OpenPosition(HttpContext, requestDto);
    return Ok(position);
  }

  [HttpGet]
  public IActionResult GetPosition()
  {
    var positionCookieValue = _cookieService.GetCookie(HttpContext, "FuturesPositions");
    if(positionCookieValue == null ) return NotFound();

    var position = JsonConvert.DeserializeObject<List<FuturesPosition>>(positionCookieValue);
    return Ok(position);
  }
}
