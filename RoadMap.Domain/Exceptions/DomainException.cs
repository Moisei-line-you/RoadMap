namespace RoadMap.Domain.Exceptions;

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

public class NotFoundException : Exception
{
    public NotFoundException(string entity, object key)
        : base($"{entity} with id '{key}' was not found") { }
}

public class BadRequestException : Exception
{
    public BadRequestException() 
        : base("Bad request") { }
    
    public BadRequestException(string message) 
        : base(message) { }

    public BadRequestException(string message, Exception innerException) 
        : base(message, innerException) { }
}

public class ConflictException : Exception
{
    public ConflictException() 
        : base("Conflict occurred") { }

    public ConflictException(string message) 
        : base(message) { }

    public ConflictException(string message, Exception innerException) 
        : base(message, innerException) { }
}

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message) { }
}