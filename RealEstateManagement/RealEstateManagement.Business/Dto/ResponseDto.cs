using System.Text.Json.Serialization;

namespace RealEstateManagement.Business.Dto;

public class ResponseDto<T>
{
    public T? Data { get; set; }
    public string? Message { get; set; }
    public string? Error { get; set; }
    public bool IsSucceed { get; set; }
    public string? TraceId { get; set; }
    public Dictionary<string, string[]>? ValidationErrors { get; set; }

    [JsonIgnore]
    public int StatusCode { get; set; }

    public static ResponseDto<T> Success(T? data, string? message = "Operation completed successfully", int statusCode = 200)
    {
        return new ResponseDto<T>
        {
            Data = data,
            Message = message,
            StatusCode = statusCode,
            IsSucceed = true
        };
    }

    public static ResponseDto<T> Success(int statusCode = 200, string? message = "Operation completed successfully")
    {
        return new ResponseDto<T>
        {
            Data = default,
            Message = message,
            StatusCode = statusCode,
            IsSucceed = true
        };
    }

    public static ResponseDto<T> Fail(string error, int statusCode = 400, string? message = null)
    {
        return new ResponseDto<T>
        {
            Error = error,
            Message = message ?? error,
            StatusCode = statusCode,
            IsSucceed = false
        };
    }

    public static ResponseDto<T> FailWithValidation(Dictionary<string, string[]> validationErrors, int statusCode = 400)
    {
        return new ResponseDto<T>
        {
            Message = "Validation failed",
            ValidationErrors = validationErrors,
            StatusCode = statusCode,
            IsSucceed = false
        };
    }
}
