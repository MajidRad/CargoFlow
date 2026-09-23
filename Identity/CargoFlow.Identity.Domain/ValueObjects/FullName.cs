using CargoFlow.BuildingBlocks.Domain;
using CargoFlow.Identity.Domain.Exceptions;

namespace CargoFlow.Identity.Domain.ValueObjects;

public record FullName(string FirstName,string LastName) : IValueObject
{

    public static FullName Create(string first,string last)
    {
        if (string.IsNullOrWhiteSpace(first) || string.IsNullOrWhiteSpace(last))
            throw new IdentityDomainException("Invalid full name");
        return new FullName(first,last); 
    }
    public override string ToString()=>$"{FirstName} {LastName}";


}
