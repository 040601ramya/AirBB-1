using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;

namespace AirBB.Models.Cookies
{
    public static class CookieHelper
    {
        private const string CookieName = "airbb_res";

        public static List<int> GetReservationIds(HttpRequest request)
        {
            if (!request.Cookies.TryGetValue(CookieName, out var raw) || string.IsNullOrWhiteSpace(raw))
                return new List<int>();

            return raw.Split(',', StringSplitOptions.RemoveEmptyEntries)
                      .Select(s => int.TryParse(s, out var id) ? id : (int?)null)
                      .Where(id => id.HasValue)
                      .Select(id => id!.Value)
                      .Distinct()
                      .ToList();
        }

        public static void SaveReservationIds(HttpResponse response, List<int> ids)
        {
            var value = string.Join(",", ids.Distinct());
            response.Cookies.Append(CookieName, value, new CookieOptions
            {
                Expires = DateTimeOffset.UtcNow.AddDays(7), // 7-day expiry
                HttpOnly = false,
                IsEssential = true,
                SameSite = SameSiteMode.Lax,
                Secure = true
            });
        }
    }
}
