namespace MyBase.Domain.Entities;
public class BorrowedBook : BaseEntity
{
    public int Id { get; set; }
    public string BookId { get; set; }
    public string AppUserId { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public decimal Amount { get; set; }

    public virtual AppUser AppUser { get; set; }
    public virtual Book Book { get; set; }

}
