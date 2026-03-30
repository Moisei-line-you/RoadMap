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

public class BusinessException : Exception
{
    public BusinessException(string message) : base(message) { }
}