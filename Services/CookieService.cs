namespace CryptoFutures.API.Services;

public class CookieService : ICookieService
{
    public void SetCookie(HttpContext httpContext, string key, string value, int? expireTime)
    {
        CookieOptions option = new();
        if (expireTime.HasValue)
        {
            option.Expires = DateTime.Now.AddDays(expireTime.Value);
        }
        else
        {
            option.Expires = DateTime.Now.AddDays(1);
        }
        httpContext.Response.Cookies.Append(key, value, option);
    }

    public string GetCookie(HttpContext httpContext, string key)
    {
        return httpContext.Request.Cookies[key];
    }

    public void RemoveCookie(HttpContext httpContext)
    {
        httpContext.Response.Cookies.Delete("FuturesPositions");
        httpContext.Response.Cookies.Delete("Balance");
    }
}