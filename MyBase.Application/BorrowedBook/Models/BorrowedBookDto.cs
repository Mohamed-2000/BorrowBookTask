namespace MyBase.Application.BorrowedBook.Models;
public class BorrowedBookDto
{
    public int Id { get; set; }
    public string BookId { get; set; }
    public string AppUserId { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public decimal Amount { get; set; }
}
