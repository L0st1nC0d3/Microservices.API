namespace Business.Contracts
{
    public interface ICreatedUserEvent
    {
        int UserId { get; set; }
        string UserName { get; set; }
    }
}
