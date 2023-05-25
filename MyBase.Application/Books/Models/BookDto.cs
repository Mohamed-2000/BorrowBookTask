namespace MyBase.Application.Books.Models;
public class BookDto
{
    public string Id { get; set; }
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public string AuthorId { get; set; }
    public string PictureUrl { get; set; }

    public string CategoryName { get; set; }
    public string AuthorName { get; set; }

}
