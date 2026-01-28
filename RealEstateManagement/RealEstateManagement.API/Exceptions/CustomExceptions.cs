namespace RealEstateManagement.API.Exceptions;

public class NotFoundException : Exception
{
    public NotFoundException(string message) : base(message) { }
    
    public NotFoundException(string message, Exception innerException) 
        : base(message, innerException) { }
}

public class ValidationException : Exception
{
    public Dictionary<string, string[]> Errors { get; }

    public ValidationException(Dictionary<string, string[]> errors) 
        : base("Validation failed")
    {
        Errors = errors;
    }

    public ValidationException(string message, Dictionary<string, string[]> errors) 
        : base(message)
    {
        Errors = errors;
    }
}

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message) { }
    
    public BusinessException(string message, Exception innerException) 
        : base(message, innerException) { }
}

public class ConflictException : Exception
{
    public ConflictException(string message) : base(message) { }
    
    public ConflictException(string message, Exception innerException) 
        : base(message, innerException) { }
}
