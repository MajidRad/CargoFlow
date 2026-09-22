namespace CargoFlow.BuildingBlocks.Domain;

public abstract class ValueObject
{
    protected abstract IEnumerable<Object?> GetEqualityComponents();
    public override bool Equals(object? obj)
    {
        if(obj is not ValueObject other) return false;  
        return GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
    }
    public override int GetHashCode() {
        return GetEqualityComponents().Aggregate(0, (hash, component) => HashCode.Combine(hash, component));
    }
}
public interface IValueObject { }