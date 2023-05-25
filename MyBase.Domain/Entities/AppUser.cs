using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace MyBase.Domain.Entities;


public class AppUser : IdentityUser<Guid>
{
  
    public Guid Id { get; set; }
    public string Name { get; set; }
    public string IdentityNo { get; set; }
    public new DateTime? LockoutEnd { get; set; }
    public string CreatedById { get; set; }
    public DateTime CreationDate { get; set; }
    public string ModifiedById { get; set; }
    public DateTime? ModificationDate { get; set; }
    public bool Deleted { get; set; }

    //public virtual Employee Employee { get; set; }

    //public virtual ICollection<AppUserNotificationToken> AppUserNotificationTokens { get; set; }
    //public virtual ICollection<AuditLog> AuditLogs { get; set; }
    public virtual ICollection<ApplicationRole> AspNetUserRoles { get; set; } = new List<ApplicationRole>();

    public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; }
    public virtual ICollection<Fines> Fines { get; set; }
}

