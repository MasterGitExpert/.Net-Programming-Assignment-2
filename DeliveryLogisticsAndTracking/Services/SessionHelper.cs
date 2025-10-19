using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Threading.Tasks;

namespace DeliveryLogisticsAndTracking.Services
{
    public static class SessionHelper
    {
        private static ProtectedSessionStorage? _sessionStorage;

        public static void Initialize(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        public static async Task SetUserAsync(int userId, string userType)
        {
            if (_sessionStorage != null)
            {
                await _sessionStorage.SetAsync("UserId", userId.ToString());
                await _sessionStorage.SetAsync("UserType", userType);
            }
        }

        public static async Task<(int userId, string userType)> GetUserAsync()
        {
            if (_sessionStorage != null)
            {
                var idResult = await _sessionStorage.GetAsync<string>("UserId");
                var typeResult = await _sessionStorage.GetAsync<string>("UserType");

                int.TryParse(idResult.Value, out int userId);
                return (userId, typeResult.Value ?? string.Empty);
            }
            return (0, string.Empty);
        }
    }
}
