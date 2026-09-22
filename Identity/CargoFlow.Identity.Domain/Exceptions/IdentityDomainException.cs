namespace CargoFlow.Identity.Domain.Exceptions;

public sealed class IdentityDomainException : Exception
{
    public IdentityDomainException(string message)
        :base(message)
    {
    }
}