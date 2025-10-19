using System;
using System.Threading.Tasks;

namespace DeliveryLogisticsAndTracking.Services
{
    public class SessionStateService
    {
        public string? UserType { get; private set; }
        public int? UserId { get; private set; }

        public event Func<Task>? OnChange;

        public async Task SetUserAsync(int userId, string userType)
        {
            UserId = userId;
            UserType = userType;
            if (OnChange != null)
                await OnChange.Invoke();
        }

        public async Task ClearUserAsync()
        {
            UserId = null;
            UserType = null;
            if (OnChange != null)
                await OnChange.Invoke();
        }
    }
}
