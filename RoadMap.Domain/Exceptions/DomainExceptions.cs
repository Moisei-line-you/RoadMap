namespace RoadMap.Application.Exceptions;

public class DomainExceptions : Exception
{
    public DomainExceptions(string message) : base(message) { }
}

public class EmailAlreadyExistsException : DomainExceptions
{
    public EmailAlreadyExistsException() : base("Email already exists") { }
}

public class InvalidCredentialsException : DomainExceptions
{
    public InvalidCredentialsException() : base("Invalid username or password") { }
}