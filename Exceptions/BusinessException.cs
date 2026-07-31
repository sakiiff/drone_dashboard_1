namespace drone_dashboard_1.Exceptions
{
    public class BusinessException : Exception
    {
        public string ErrorCode { get; }
        public int StatusCode { get; }

        public BusinessException(
            string errorCode,
            string message,
            int statusCode)
            : base(message)
        {
            ErrorCode = errorCode;
            StatusCode = statusCode;
        }
    }
}
