using Microsoft.AspNetCore.SignalR;


namespace drone_dashboard_1.Hubs
{
    public class DroneHub : Hub
    {
        public async Task JoinDroneGroup(int droneId)
        {
            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                $"flight-{droneId}");
        }

        public async Task LeaveDroneGroup(int droneId)
        {
            await Groups.RemoveFromGroupAsync(
                Context.ConnectionId,
                $"flight-{droneId}");
        }
    }
}
