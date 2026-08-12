using Microsoft.AspNetCore.SignalR;

namespace drone_dashboard_1.Hubs
{
    public class FlightHub : Hub
    {
        public async Task JoinFlightGroup(int flightId)
        {
            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                $"flight-{flightId}");
        }

        public async Task LeaveFlightGroup(int flightId)
        {
            await Groups.RemoveFromGroupAsync(
                Context.ConnectionId,
                $"flight-{flightId}");
        }

        public async Task JoinDroneGroup(int droneId)
        {
            await Groups.AddToGroupAsync(
                Context.ConnectionId,
                $"drone-{droneId}");
        }

        public async Task LeaveDroneGroup(int droneId)
        {
            await Groups.RemoveFromGroupAsync(
                Context.ConnectionId,
                $"drone-{droneId}");
        }
    }
}
