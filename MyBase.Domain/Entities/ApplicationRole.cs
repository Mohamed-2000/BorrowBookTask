using Microsoft.AspNetCore.Identity;

namespace MyBase.Domain.Entities;
public class ApplicationRole : IdentityRole<Guid>
{
    public virtual ICollection<AppUser> ApplicationUsers { get; set; } = new List<AppUser>();

}