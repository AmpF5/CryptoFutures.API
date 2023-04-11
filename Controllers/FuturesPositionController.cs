using Microsoft.AspNetCore.Mvc;
using CryptoFutures.API.Services;
using CryptoFutures.API.Models;
using Newtonsoft.Json;
using CryptoFutures.API.Entities;

namespace CryptoFutures.API.Controllers;

[ApiController]
[Route("api/[controller]")]
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
    var position = await _futuresPositionService.OpenPosition(HttpContext, requestDto);
    return Ok(position);
  }
  [HttpDelete]
  public IActionResult ClosePosition(int positionId)
  {
    var position = _futuresPositionService.ClosePosition(HttpContext, positionId);
    return position is not null ? Ok(position) : BadRequest(ModelState);
  }

  [HttpGet("position/all")]
  public IActionResult GetPositions()
  {
    // TODO moved it to the service
    var positionCookieValue = _cookieService.GetCookie(HttpContext, "FuturesPositions");
    if(positionCookieValue == null ) return NotFound();

    var position = JsonConvert.DeserializeObject<List<FuturesPosition>>(positionCookieValue);
    return Ok(position);
  }
  [HttpGet("position/{positionId:int}")]
  public IActionResult GetPostion([FromRoute] int positionId)
  {
    var positionResponseDto = _futuresPositionService.GetPosition(HttpContext, positionId);
    return positionResponseDto is not null ? Ok(positionResponseDto) : BadRequest(ModelState);
  }
  [HttpPut]
  public IActionResult UpdateStopLoss(int positionId, decimal stopLoss)
  {
    var position = _futuresPositionService.UpdateStopLoss(positionId, stopLoss);
    return Ok();
  }
  [HttpDelete("/Cookies")]
  public IActionResult DeleteCookie()
  {
    _cookieService.RemoveCookie(HttpContext);
    return Ok();
  }
}
