using Microsoft.AspNetCore.Http;
using System.Text.Json;

namespace AirBB.Models.Session
{
    public interface ISessionHelper
    {
        void Set<T>(string key, T value);
        T? Get<T>(string key);
        void Remove(string key);
    }

    public class SessionHelper : ISessionHelper
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public SessionHelper(IHttpContextAccessor httpContextAccessor)
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public void Set<T>(string key, T value)
        {
            var json = JsonSerializer.Serialize(value);
            _httpContextAccessor.HttpContext?.Session.SetString(key, json);
        }

        public T? Get<T>(string key)
        {
            var json = _httpContextAccessor.HttpContext?.Session.GetString(key);
            return json == null ? default : JsonSerializer.Deserialize<T>(json);
        }

        public void Remove(string key)
        {
            _httpContextAccessor.HttpContext?.Session.Remove(key);
        }
    }
}
