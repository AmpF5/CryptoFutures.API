using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace CryptoFutures.API.Services;
public class CookieService : ICookieService
{
    public void SetCookie(HttpContext httpContext, string key, string value, int? expireTime)
    {
        CookieOptions option = new();
        if (expireTime.HasValue)
        {
            option.Expires = DateTime.Now.AddMinutes(expireTime.Value);
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
}