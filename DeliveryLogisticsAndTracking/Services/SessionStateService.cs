using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;

namespace DeliveryLogisticsAndTracking.Services
{
    /*<summary>
    Service to manage the current user's session state in a Blazor Server app.
    Wraps ProtectedSessionStorage to persist user info across the browser session
    and provides an event to notify UI components of changes.
    </summary>*/
    public class SessionStateService
    {
        private readonly ProtectedSessionStorage _sessionStorage;

        // Event triggered whenever the session state changes.
        // Components can subscribe to update UI automatically.
        public event Func<Task>? OnChange;

        // The currently logged-in user's ID, or null if not logged in.
        public int? UserId { get; private set; }

        // The type/role of the current user (e.g., Admin, Driver), or null if not logged in.
        public string? UserType { get; private set; }

        // The name of the current user, or null if not logged in.
        public string? UserName { get; private set; }

        // Constructor injects the ProtectedSessionStorage instance for storing user session data.
        public SessionStateService(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }

        // Sets the current user's session state and persists it to browser session storage.
        // Triggers OnChange event to notify subscribers.
        public async Task SetUserAsync(int userId, string userType, string? userName = null)
        {
            UserId = userId;
            UserType = userType;
            UserName = userName;

            // Store values in session storage
            await _sessionStorage.SetAsync("UserId", userId);
            await _sessionStorage.SetAsync("UserType", userType);
            await _sessionStorage.SetAsync("UserName", userName);

            // Notify subscribers that session state has changed
            if (OnChange != null) await OnChange.Invoke();
        }

        // Loads the current user's session state from browser session storage.
        // Updates properties and triggers OnChange event for UI updates.
        public async Task LoadUserAsync()
        {
            var userIdResult = await _sessionStorage.GetAsync<int>("UserId");
            var userTypeResult = await _sessionStorage.GetAsync<string>("UserType");
            var userNameResult = await _sessionStorage.GetAsync<string>("UserName");

            if (userIdResult.Success)
            {
                UserId = userIdResult.Value;
                UserType = userTypeResult.Success ? userTypeResult.Value : null;
                UserName = userNameResult.Success ? userNameResult.Value : null;
            }

            // Notify subscribers of session state change
            if (OnChange != null) await OnChange.Invoke();
        }

        // Clears the current user's session state both in memory and in browser session storage.
        // Triggers OnChange event to update UI accordingly.
        public async Task ClearUserAsync()
        {
            UserId = null;
            UserType = null;
            UserName = null;

            // Remove stored values from session storage
            await _sessionStorage.DeleteAsync("UserId");
            await _sessionStorage.DeleteAsync("UserType");
            await _sessionStorage.DeleteAsync("UserName");

            // Notify subscribers that session state has been cleared
            if (OnChange != null) await OnChange.Invoke();
        }
    }
}
