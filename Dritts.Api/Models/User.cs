using System.ComponentModel.DataAnnotations;

namespace Dritts.Api.Models
{
    public class User
    {
        public Guid Id { get; set; }

        [MaxLength(20)]
        public required string FirstName { get; set; }

        [MaxLength(20)]
        public required string MiddleName { get; set; }

        [MaxLength(20)]
        public required string LastName { get; set; }

        [MaxLength(10)]
        public required string PhoneNumber { get; set; }
        public UserAccount Account { get; set; } = default!;
    }
}
