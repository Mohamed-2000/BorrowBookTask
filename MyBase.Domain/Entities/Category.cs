namespace MyBase.Domain.Entities;
public class Category : BaseEntity
{
    public int Id { get; set; }
    public string Name { get; set; }

    public virtual ICollection<Book> Books { get; set; }

}
