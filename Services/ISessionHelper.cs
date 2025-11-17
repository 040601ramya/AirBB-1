using AirBB.Models;
using AirBB.Models.ViewModels;

namespace AirBB.Services
{
    public interface ISessionHelper
    {
        void SaveSearchFilters(HomeViewModel model);
        HomeViewModel? GetSearchFilters();
        void ClearFilters();
    }
}
