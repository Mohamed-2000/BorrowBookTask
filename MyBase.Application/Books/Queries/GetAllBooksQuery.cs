using MyBase.Application.Books.Models;
using MyBase.Application.Interfaces;
using MyBase.Common;

namespace MyBase.Application.Books.Queries;
public class GetAllBooksQuery : IRequest<PagingDto<BookDto>>
{
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get { return 6; } }

    public string? Name { get; set; }
    public string? CategoryName { get; set; }
    public string? AuthorName { get; set; }


    public class Handler : IRequestHandler<GetAllBooksQuery, PagingDto<BookDto>>
    {
        private readonly IMyBaseContext _context;
        private readonly ILogger<Handler> _logger;
        private readonly IMediator _mediator;

        public Handler(IMyBaseContext context, ILogger<Handler> logger, IMediator mediator)
        {
            _context = context;
            _logger = logger;
            _mediator = mediator;
        }
        public async Task<PagingDto<BookDto>> Handle(GetAllBooksQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var Skip = request.CurrentPage;
                if (Skip > 0) { Skip = (Skip - 1) * request.PageSize; }
                int recordsTotal = 0;
                int PageCount = 0;
                int CurrentPage = request.CurrentPage == 0 ? 1 : request.CurrentPage;

                var BooksList = await _context.Books.Where(a => !a.Deleted
                && (string.IsNullOrEmpty(request.Name) || a.Name.ToUpper().Contains(request.Name.ToUpper()))
                && (string.IsNullOrEmpty(request.CategoryName) || a.Category.Name.ToUpper().Contains(request.CategoryName.ToUpper()))
                && (string.IsNullOrEmpty(request.AuthorName) || a.Author.Name.ToUpper().Contains(request.AuthorName.ToUpper())))
                    .Select(a =>
                new BookDto
                {
                    Id = a.Id,
                    Name = a.Name ?? "-",
                    CategoryName = a.Category.Name ?? "-",
                    AuthorName = a.Author.Name ?? "-",
                    PictureUrl = FileLocation.BooksImages + "/" + a.PictureUrl,

                }).Skip(Skip).Take(request.PageSize).ToListAsync();




                recordsTotal = await _context.Books.Where(a => !a.Deleted
                && (string.IsNullOrEmpty(request.Name) || a.Name.ToUpper().Contains(request.Name.ToUpper()))
                && (string.IsNullOrEmpty(request.CategoryName) || a.Category.Name.ToUpper().Contains(request.CategoryName.ToUpper()))
                && (string.IsNullOrEmpty(request.AuthorName) || a.Author.Name.ToUpper().Contains(request.AuthorName.ToUpper())))
                    .Select(a =>
                new BookDto
                {
                    Id = a.Id,
                    Name = a.Name ?? "-",
                    CategoryName = a.Category.Name ?? "-",
                    AuthorName = a.Author.Name ?? "-",

                }).CountAsync();

                PageCount = (recordsTotal / request.PageSize) + ((recordsTotal % request.PageSize) > 0 ? 1 : 0);


                return new PagingDto<BookDto>
                {
                    data = BooksList,
                    PagesCount = PageCount,
                    CurrentPage = CurrentPage,
                    recordsTotal = recordsTotal,
                };
            }
            catch (Exception)
            {

                return new PagingDto<BookDto>
                {
                    data = new List<BookDto>(),
                    PagesCount = 1,
                    CurrentPage = 1,
                    recordsTotal = 0,
                };
            }
            
        }
    }
}
