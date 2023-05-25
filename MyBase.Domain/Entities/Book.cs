namespace MyBase.Domain.Entities;
public class Book : BaseEntity
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public string AuthorId { get; set; }
    public string PictureUrl { get; set; }

    public virtual Author Author { get; set; }
    public virtual Category Category { get; set; }
    public virtual ICollection<BorrowedBook> BorrowedBooks { get; set; }


}
