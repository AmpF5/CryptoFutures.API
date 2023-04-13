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
        var positions = _futuresPositionService.GetPositions(HttpContext);
        return positions is not null ? Ok(positions) : BadRequest(ModelState);
    }

    [HttpGet("position/{positionId:int}")]
    public IActionResult GetPostion([FromRoute] int positionId)
    {
        var positionResponseDto = _futuresPositionService.GetPosition(HttpContext, positionId);
        return positionResponseDto is not null ? Ok(positionResponseDto) : BadRequest(ModelState);
    }

    [HttpPut("position/{positionId}")]
    public IActionResult UpdatePositionStopLossOrTakeProfit(int positionId, [FromQuery] decimal stopLoss, [FromQuery] decimal takeProfit)
    {
        var position = _futuresPositionService.UpdatePositionStopLossOrTakeProfit(HttpContext, positionId, stopLoss, takeProfit);
        return position is not null ? Ok(position) : BadRequest(ModelState);
    }

    [HttpDelete("/Cookies")]
    public IActionResult DeleteCookie()
    {
        _cookieService.RemoveCookie(HttpContext);
        return Ok();
    }
}