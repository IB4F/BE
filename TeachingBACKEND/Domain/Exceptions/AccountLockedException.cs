namespace TeachingBACKEND.Domain.Exceptions;

public class AccountLockedException : Exception
{
    public DateTime LockedUntil { get; }

    public AccountLockedException(DateTime lockedUntil)
        : base("Llogaria u bllokua përkohësisht.")
    {
        LockedUntil = lockedUntil;
    }
}
