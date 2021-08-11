namespace Serendip.IK.Authorization
{
    public interface IAuthorizedModel
    {
        long Id { get; set; }

        AuthorizeLevel? AuthorizeLevel { get; set; }

        long OwnerId { get; set; }

        string GetModelName();
    }
}
