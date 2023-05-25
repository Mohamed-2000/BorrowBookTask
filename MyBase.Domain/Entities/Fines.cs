namespace MyBase.Domain.Entities;
public class Fines : BaseEntity
{
    public int Id { get; set; }
    public Guid AppUserId { get; set; }
    public decimal Amount { get; set; }
    public virtual AppUser AppUser { get; set; }

}
