using EncoreSample.Controllers;
using Microsoft.AspNetCore.Identity;

namespace EncoreSample.Models
{
    public class User : IdentityUser
    {
        public string? firstName { get; set; }
        public string? lastName { get; set; }
        public string FullName { get { return firstName + " " + lastName; } }

        public virtual List<Audio>? Audios { get; set; }
        public virtual List<Comment>? Comments { get; set; }
        public virtual List<Vote>? Votes { get; set; }
    }
}
