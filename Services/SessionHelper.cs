using AirBB.Models;
using AirBB.Models.ViewModels;
using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace AirBB.Services
{
    public class SessionHelper : ISessionHelper
    {
        private readonly IHttpContextAccessor _http;

        private const string FILTER_KEY = "SearchFilters";

        public SessionHelper(IHttpContextAccessor http)
        {
            _http = http;
        }

        public void SaveSearchFilters(HomeViewModel model)
        {
            if (_http.HttpContext == null) return;

            string json = JsonSerializer.Serialize(model);
            _http.HttpContext.Session.SetString(FILTER_KEY, json);
        }

        public HomeViewModel? GetSearchFilters()
        {
            if (_http.HttpContext == null) return null;

            string? json = _http.HttpContext.Session.GetString(FILTER_KEY);

            if (string.IsNullOrEmpty(json))
                return null;

            return JsonSerializer.Deserialize<HomeViewModel>(json);
        }

        public void ClearFilters()
        {
            _http.HttpContext?.Session?.Remove(FILTER_KEY);
        }
    }
}
