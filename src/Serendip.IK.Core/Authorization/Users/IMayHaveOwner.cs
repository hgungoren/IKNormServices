namespace Serendip.IK.Authorization.Users
{
    public interface IMayHaveOwner
    {
        long? OwnerId { get; set; }
        long? OwnerGroupId { get; set; }
    }
}
