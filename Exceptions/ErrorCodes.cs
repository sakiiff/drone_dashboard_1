namespace drone_dashboard_1.Exceptions
{
    public class ErrorCodes
    {
        // Drone
        public const string DroneNotFound = "DRONE_001";
        public const string DroneInactive = "DRONE_002";
        public const string DuplicateSerialNumber = "DRONE_003";

        // Flight
        public const string FlightNotFound = "FLIGHT_404";
        public const string FlightAlreadyInProgress = "FLIGHT_002";
        public const string FlightAlreadyCompleted = "FLIGHT_003";

        // Telemetry
        public const string TelemetryFlightCompleted = "TELEM_001";
        public const string TelemetryInvalidGps = "TELEM_002";
        public const string TelemetryTimestampError = "TELEM_003";
        public const string TelemetryDroneReferencedNotFound = "TELEM_004";
        public const string TelemetryFlightNotFound = "TELEM_005";
    }
}
