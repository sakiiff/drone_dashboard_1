using Microsoft.AspNetCore.SignalR;

namespace drone_dashboard_1.Hubs
{
    public class TelemetryHub : Hub
    {
        public async Task JoinTelemGroup(int flightId)
        {
            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                $"flight-{flightId}");
        }

        public async Task LeaveTelemGroup(int flightId)
        {
            await Groups.RemoveFromGroupAsync(
                Context.ConnectionId,
                $"flight-{flightId}");
        }

        public async Task JoinTelemByDroneGroup(int droneId)
        {
            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                $"drone-{droneId}");
        }

        public async Task LeaveTelemByDroneGroup(int droneId)
        {
            await Groups.RemoveFromGroupAsync(
                Context.ConnectionId,
                $"drone-{droneId}");
        }
    }
}
