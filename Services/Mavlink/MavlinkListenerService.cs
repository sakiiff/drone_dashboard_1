using Asv.IO;
using Asv.Mavlink;
using Asv.Mavlink.Common;
using Asv.Mavlink.Minimal;

namespace drone_dashboard_1.Services.Mavlink;

public class MavlinkListenerService : BackgroundService
{
    private readonly ILogger<MavlinkListenerService> _logger;

    public MavlinkListenerService(
        ILogger<MavlinkListenerService> logger)
    {
        _logger = logger;
    }

    protected override async Task ExecuteAsync(
        CancellationToken stoppingToken)
    {
        var builder = new ProtocolBuilder();

        // Create MAVLink V2 message factory
        var factory = MavlinkV2Protocol.CreateMessageFactory(
            MavlinkV2Protocol.RegisterDefaultDialects);

        // Register MAVLink V2 parser
        MavlinkV2Protocol.RegisterMavlinkV2Protocol(
            builder,
            factory);

        var protocol = builder.Create();

        // Create MAVLink router
        await using var router =
            protocol.CreateRouter("mavlink");

        // Listen on UDP port 14551
        router.AddPort(
            new Uri("udp://127.0.0.1:14551"));

        router.OnRxMessage.Subscribe(message =>
        {
            switch (message)
            {
                case GlobalPositionIntPacket gps:
                    HandleGlobalPosition(gps);

                    break;

                case AttitudePacket attitude:
                    HandleAttitude(attitude);

                    break;

                case SysStatusPacket sys:
                    HandleSystemStatus(sys);

                    break;

                case HeartbeatPacket heartbeat:
                    HandleHeartbeat(heartbeat);

                    break;
            }
        });

        _logger.LogInformation(
            "MAVLink listener started on UDP 14551");

        await Task.Delay(
            Timeout.Infinite,
            stoppingToken);
    }

    private void HandleGlobalPosition(
        GlobalPositionIntPacket packet)
    {
        var p = packet.Payload;

        var latitude = p.Lat / 1e7;
        var longitude = p.Lon / 1e7;
        var altitude = p.Alt / 1000.0;

        _logger.LogInformation(
            "GPS Lat:{Lat} Lon:{Lon} Alt:{Alt}",
            latitude,
            longitude,
            altitude);
    }

    private void HandleAttitude(
        AttitudePacket packet)
    {
        var p = packet.Payload;

        _logger.LogInformation(
            "Roll:{Roll} Pitch:{Pitch} Yaw:{Yaw}",
            p.Roll,
            p.Pitch,
            p.Yaw);
    }

    private void HandleSystemStatus(
        SysStatusPacket packet)
    {
        var p = packet.Payload;

        var batteryPercent =
            p.BatteryRemaining;

        _logger.LogInformation(
            "Battery:{Battery}%",
            batteryPercent);
    }

    private void HandleHeartbeat(
        HeartbeatPacket packet)
    {
        var p = packet.Payload;

        _logger.LogInformation(
            "Heartbeat Type:{Type} Status:{Status}",
            p.Type,
            p.SystemStatus);
    }
}