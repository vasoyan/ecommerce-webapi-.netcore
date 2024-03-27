namespace ECommerce.Application.Models.Helpers
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? ErrorMessage { get; set; }
        public int StatusCode { get; set; }

        public ApiResponse(T data, int statusCode = 200)
        {
            Success = true;
            Data = data;
            StatusCode = statusCode;
        }

        public ApiResponse(string errorMessage, int statusCode = 200)
        {
            Success = false;
            ErrorMessage = errorMessage;
            StatusCode = statusCode;
        }

        public ApiResponse(Exception exception, int statusCode = 200)
        {
            Success = false;
            ErrorMessage = exception.Message;
            StatusCode = statusCode;
        }
    }

}
