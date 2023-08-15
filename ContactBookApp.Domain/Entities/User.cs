using Microsoft.AspNetCore.Identity;

namespace ContactBookApp.Domain.Entities
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string? ImageURL { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime ModifiedAt { get; set; } = DateTime.UtcNow;
        //Navigational Property
        public ICollection<Contact> Contacts { get; set; }
    }
}
