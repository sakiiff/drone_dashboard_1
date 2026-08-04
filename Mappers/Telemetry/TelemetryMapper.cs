using drone_dashboard_1.DTOs.Telemetry;

namespace drone_dashboard_1.Mappers.Telemetry
{
    public static class TelemetryMapper
    {
        public static Models.Telemetry ToEntity(this CreateTelemetryDTO dto)
        {
            return new Models.Telemetry
            {
                FlightId = dto.FlightId,
                Timestamp = dto.Timestamp,
                Longitude = dto.Longitude,
                Latitude = dto.Latitude,
                AltitudeMeters = dto.AltitudeMeters,
                SpeedMetersPerSecond = dto.SpeedMetersPerSecond,
                VerticalSpeedMetersPerSecond = dto.VerticalSpeedMetersPerSecond,
                BatteryPercentage = dto.BatteryPercentage,
                HeadingDegrees = dto.HeadingDegrees,
                PitchDegrees = dto.PitchDegrees,
                RollDegrees = dto.RollDegrees,
                YawDegrees = dto.YawDegrees,
                CurrentAmps = dto.CurrentAmps,
                BatteryVoltage = dto.BatteryVoltage,
                SatelliteCount = dto.SatelliteCount,
                GpsFixType = dto.GpsFixType,
                FlightMode = dto.FlightMode,
                IsArmed = dto.IsArmed,
            };
        }

        public static TelemetryResponseDTO ToResponseDTO(this Models.Telemetry telemetry, string? droneName)
        {
            return new TelemetryResponseDTO
            {
                Id = telemetry.Id,
                DroneName = droneName ?? "N/A",
                FlightId = telemetry.FlightId,
                Timestamp = telemetry.Timestamp,
                Longitude = telemetry.Longitude,
                Latitude = telemetry.Latitude,
                AltitudeMeters = telemetry.AltitudeMeters,
                SpeedMetersPerSecond = telemetry.SpeedMetersPerSecond,
                VerticalSpeedMetersPerSecond = telemetry.VerticalSpeedMetersPerSecond,
                BatteryPercentage = telemetry.BatteryPercentage,
                BatteryVoltage = telemetry.BatteryVoltage,
                CurrentAmps = telemetry.CurrentAmps,
                SatelliteCount = telemetry.SatelliteCount,
                GpsFixType = telemetry.GpsFixType,
                IsArmed = telemetry.IsArmed,
                FlightMode = telemetry.FlightMode
            };
            
        }
    }
}
