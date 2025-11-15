using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace AirBB.Models.Cookies
{
    public static class CookieExtensions
    {
        public static void SetObject(this IResponseCookies cookies, string key, object value, int days)
        {
            var json = JsonSerializer.Serialize(value);
            cookies.Append(key, json, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(days),
                HttpOnly = true
            });
        }

        public static T? GetObject<T>(this IRequestCookieCollection cookies, string key)
        {
            if (cookies.TryGetValue(key, out var json))
            {
                return JsonSerializer.Deserialize<T>(json);
            }
            return default;
        }
    }
}
