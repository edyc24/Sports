using Microsoft.AspNetCore.Identity;

namespace Sports.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string? FirstName { get; set; }
        public string? LastName { get; set; }
        public DateTime DateOfBirth { get; set; }
        public ICollection<Discussion> Discussions { get; set; }
        public ICollection<Comment> Comments { get; set; }
    }
}
