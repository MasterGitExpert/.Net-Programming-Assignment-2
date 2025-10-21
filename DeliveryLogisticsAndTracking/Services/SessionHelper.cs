using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Threading.Tasks;

namespace DeliveryLogisticsAndTracking.Services
{
    /*<summary>
    A static helper class to manage user session data using ProtectedSessionStorage.
    Stores and retrieves user ID and user type securely in the browser session.
    </summary>*/
    public static class SessionHelper
    {
        // Private reference to the ProtectedSessionStorage instance.
        private static ProtectedSessionStorage? _sessionStorage;

        // Initializes the helper with a ProtectedSessionStorage instance.
        // Must be called before using SetUserAsync or GetUserAsync.
        public static void Initialize(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        // Saves the user's ID and type to session storage.
        // Uses async storage APIs to securely persist data for the current browser session.
        public static async Task SetUserAsync(int userId, string userType)
        {
            if (_sessionStorage != null)
            {
                // Store user ID as string
                await _sessionStorage.SetAsync("UserId", userId.ToString());

                // Store user type
                await _sessionStorage.SetAsync("UserType", userType);
            }
        }

        // Retrieves the current user's ID and type from session storage.
        // Returns (0, "") if storage is uninitialized or values are missing.
        // <returns>A tuple containing userId and userType.</returns>
        public static async Task<(int userId, string userType)> GetUserAsync()
        {
            if (_sessionStorage != null)
            {
                // Get stored user ID and user type
                var idResult = await _sessionStorage.GetAsync<string>("UserId");
                var typeResult = await _sessionStorage.GetAsync<string>("UserType");

                // Try parsing the stored user ID to integer
                int.TryParse(idResult.Value, out int userId);

                // Return tuple: parsed ID and user type (or empty string if null)
                return (userId, typeResult.Value ?? string.Empty);
            }

            // Return default values if session storage is not initialized
            return (0, string.Empty);
        }
    }
}