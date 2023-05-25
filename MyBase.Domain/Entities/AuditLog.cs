namespace MyBase.Domain.Entities; 
public class AuditLog 
{
public int Id { get; set; }
public string TableName { get; set; }
public string Action  { get; set; }
public string Feature { get; set; }

public Guid AppUserId { get; set; }
public DateTime CreationDate { get; set; }
//public virtual AppUser AppUser { get; set; }
}