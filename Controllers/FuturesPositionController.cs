using CryptoFutures.API.Models;
using CryptoFutures.API.Services;
using Microsoft.AspNetCore.Mvc;

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
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var position = await _futuresPositionService.OpenPosition(requestDto);
        return Ok(position);
    }

    [HttpDelete("position/{positionId}")]
    public async Task<IActionResult> ClosePosition(int positionId)
    {
        var position = await _futuresPositionService.ClosePosition(positionId);
        return position is not null ? Ok(position) : BadRequest(ModelState);
    }

    [HttpGet("position/all")]
    public IActionResult GetPositions()
    {
        var positions = _futuresPositionService.GetPositions();
        return positions is not null ? Ok(positions) : NoContent();
    }

    [HttpGet("position/{positionId:int}")]
    public IActionResult GetPosition([FromRoute] int positionId)
    {
        var positionResponseDto = _futuresPositionService.GetPosition(positionId);
        return positionResponseDto is not null ? Ok(positionResponseDto) : BadRequest(ModelState);
    }

    [HttpPut("position/{positionId}")]
    public IActionResult UpdatePositionStopLossOrTakeProfit(int positionId, [FromQuery] decimal stopLoss, [FromQuery] decimal takeProfit)
    {
        var position = _futuresPositionService.UpdatePositionStopLossOrTakeProfit(positionId, stopLoss, takeProfit);
        return position is not null ? Ok(position) : BadRequest(ModelState);
    }

    [HttpDelete("/Cookies")]
    public IActionResult DeleteCookie()
    {
        _cookieService.RemoveCookie(HttpContext);
        return Ok();
    }
}