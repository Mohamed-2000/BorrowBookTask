namespace MyBase.Application.Books.Models;
public class PagingDto<T>
{
    public int? PagesCount { get; set; }
    public int? CurrentPage { get; set; }
    public int? recordsTotal { get; set; }
    public int? SelectTopVal { get; set; } = 0;
    public List<T> data { get; set; }
}
