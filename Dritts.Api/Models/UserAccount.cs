namespace Dritts.Api.Models
{
    public class UserAccount
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public User User { get; set; } = default!;
    }
}
