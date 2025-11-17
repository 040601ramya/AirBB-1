using Microsoft.AspNetCore.Http;

namespace AirBB.Models.Cookies
{
    public static class CookieHelper
    {
        public static void SetCookie(HttpContext context, string key, string value, int? expireTime)
        {
            if (context == null) return;

            CookieOptions option = new CookieOptions();

            if (expireTime.HasValue)
                option.Expires = DateTimeOffset.Now.AddMinutes(expireTime.Value);
            else
                option.Expires = DateTimeOffset.Now.AddMinutes(10);

            context.Response?.Cookies?.Append(key, value, option);
        }

        public static string? GetCookie(HttpContext context, string key)
        {
            return context?.Request?.Cookies?[key];
        }

        public static void DeleteCookie(HttpContext context, string key)
        {
            context?.Response?.Cookies?.Delete(key);
        }
    }
}
