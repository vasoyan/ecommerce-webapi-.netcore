using System.Net;

namespace ECommerce.Application.Models.Helpers
{
    public class ApiResponse<T>
    {
        public bool Success { get; set; }
        public T? Data { get; set; }
        public string? Message { get; set; }
        public int StatusCode { get; set; }

        public ApiResponse(T data, int statusCode = 200)
        {
            Success = true;
            Data = data;
            StatusCode = statusCode;
        }

        public ApiResponse(string message, int statusCode = 200)
        {
            Success = statusCode == (int)HttpStatusCode.OK;
            Message = message;
            StatusCode = statusCode;
        }

        public ApiResponse(Exception? exception, int statusCode = 200)
        {
            Success = false;
            Message = exception?.Message;
            StatusCode = statusCode;
        }
    }

}
