namespace RoadMap.Domain.Exceptions;

public abstract class AppException : Exception
{
    public int StatusCode { get; }

    protected AppException(string message, int statusCode)
        : base(message)
    {
        StatusCode = statusCode;
    }
}

public class DomainException : Exception
{
    public DomainException(string message) : base(message) { }
}

public class EmailAlreadyExistsException : DomainException
{
    public EmailAlreadyExistsException() : base("Email already exists") { }
}

public class InvalidCredentialsException : DomainException
{
    public InvalidCredentialsException() : base("Invalid username or password") { }
}

public class NotFoundException : AppException
{
    public NotFoundException(string entity, object key)
        : base($"{entity} with id '{key}' was not found", 404) { }
}

public class BadRequestException : AppException
{
    public BadRequestException(string message)
        : base(message, 400) { }
}

public class ConflictException : AppException
{
    public ConflictException(string message)
        : base(message, 409) { }
}

public class BusinessException : AppException
{
    public BusinessException(string message)
        : base(message, 400) { }
}