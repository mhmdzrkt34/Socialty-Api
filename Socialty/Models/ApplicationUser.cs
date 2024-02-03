using Microsoft.AspNetCore.Identity;

namespace Socialty.Models
{
    public class ApplicationUser:IdentityUser
    {

        public String? Profile_Url { get; set; }

        public virtual ICollection<Post>? Posts { get; set; }



    }
}
