using System.Net;
using System.Text.Json.Serialization;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace TrackingApp.Core.Models
{
    public  class BaseResponseDto<T>
    {

        public BaseResponseDto()
        {
            Errors = new List<string>();
        }

        public bool IsSuccess =>  (Errors == null || Errors.Count == 0) ? true : false;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Message { get; set; }

        public  List<string>Errors { get; set; }

        [JsonIgnore(Condition =JsonIgnoreCondition.WhenWritingNull)]
        public T? Data { get; set; }

        [JsonIgnore]
        public HttpStatusCode StatusCode { get; set; }


        public static BaseResponseDto<T> Success(T? data, string message = "Process completed successfully.", HttpStatusCode statusCode = HttpStatusCode.Created)
        {
            return new BaseResponseDto<T>
            {
                Message = message,
                StatusCode= statusCode,
                Data=data
            };
        }

      

    }

    public class BaseResponseDto
    {

        [JsonIgnore]
        public bool IsSuccess => string.IsNullOrWhiteSpace(ErrorCode) && (Errors == null || Errors.Count == 0) ? true : false;

        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public string Message { get; set; }

        public string? ErrorCode { get; set; } = null;

        public List<string> Errors { get; set; }


        [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
        public HttpStatusCode StatusCode { get; set; }


        public static BaseResponseDto Success( string Message = "Process completed successfully.", HttpStatusCode StatusCode = HttpStatusCode.Created)
        {
            return new BaseResponseDto
            {
                Message = Message,
                StatusCode = StatusCode
            };
        }



    }
}
