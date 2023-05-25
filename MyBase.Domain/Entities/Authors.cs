namespace MyBase.Domain.Entities;
public class Author : BaseEntity
{
    public string Id { get; set; }
    public string Name { get; set; }
    public virtual ICollection<Book> Books { get; set; }

}
