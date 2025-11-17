using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace AirBB.Models.Cookies
{
    public static class CookieExtensions
    {
        public static void SetObject<T>(this IResponseCookies cookies, string key, T value, int days = 7)
        {
            var options = new CookieOptions
            {
                Expires = DateTimeOffset.Now.AddDays(days),
                HttpOnly = false,
                IsEssential = true,
                Secure = false,
                SameSite = SameSiteMode.Lax
            };

            string json = JsonSerializer.Serialize(value);
            cookies.Append(key, json, options);
        }

        public static T? GetObject<T>(this IRequestCookieCollection cookies, string key)
        {
            if (cookies.TryGetValue(key, out string? value))
            {
                return string.IsNullOrEmpty(value)
                    ? default
                    : JsonSerializer.Deserialize<T>(value);
            }
            return default;
        }
    }
}
