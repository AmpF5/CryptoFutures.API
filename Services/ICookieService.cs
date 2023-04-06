namespace CryptoFutures.API.Services;

public interface ICookieService
{
    void SetCookie(HttpContext httpContext, string key, string value, int? expireTime);
    string GetCookie(HttpContext httpContext, string key);
}
