namespace RealEstateManagement.Business.Dto;

public class ErrorResponseDto
{
    public bool IsSucceed { get; set; }
    public string Message { get; set; } = "";
    public string? Details { get; set; }
    public Dictionary<string, string[]>? ValidationErrors { get; set; }
    public string? TraceId { get; set; }
}
