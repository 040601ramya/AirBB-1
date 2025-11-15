// File: Services/SessionHelper.cs
using Microsoft.AspNetCore.Http;
using AirBB.Utilities;

namespace AirBB.Services
{
    public interface ISessionHelper
    {
        void Set<T>(string key, T value);
        T? Get<T>(string key);
        void Remove(string key);
    }

    public class SessionHelper : ISessionHelper
    {
        private readonly IHttpContextAccessor _http;
        public SessionHelper(IHttpContextAccessor http) => _http = http;

        public void Set<T>(string key, T value) =>
            _http.HttpContext!.Session.SetObject(key, value);

        public T? Get<T>(string key) =>
            _http.HttpContext!.Session.GetObject<T>(key);

        public void Remove(string key) => _http.HttpContext!.Session.RemoveKey(key);
    }
}
