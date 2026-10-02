using CargoFlow.BuildingBlocks.Domain;

namespace CargoFlow.Identity.Domain.Entities;

public sealed class RefreshToken : Entity<Guid>
{
    private RefreshToken()
    {
    }

    public string Token { get; private set; }

    public DateTime ExpiresAt { get; private set; }

    public bool Revoked { get; private set; }

    public RefreshToken(
    Guid id,
    string token,
    DateTime expiresAt)
    : base(id)
    {
        Token = token;
        ExpiresAt = expiresAt;
    }

    public void Revoke()
    {
        Revoked = true;
    }

    public bool IsExpired()
    => DateTime.UtcNow > ExpiresAt;
    public bool IsValid()
    {
        return !Revoked && ExpiresAt > DateTime.UtcNow;
    }
}