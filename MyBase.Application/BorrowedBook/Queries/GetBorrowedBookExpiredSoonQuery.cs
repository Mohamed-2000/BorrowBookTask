using MyBase.Application.Books.Models;
using MyBase.Application.Books.Queries;
using MyBase.Application.BorrowedBook.Models;
using MyBase.Common;

namespace MyBase.Application.BorrowedBook.Queries;
public class GetBorrowedBookExpiredSoonQuery : IRequest<PagingDto<BorrowedBookDto>>
{
    public int CurrentPage { get; set; } = 1;
    public int PageSize { get { return 6; } }

    public string? Name { get; set; }
    public string? CategoryName { get; set; }
    public string? AuthorName { get; set; }


    public class Handler : IRequestHandler<GetBorrowedBookExpiredSoonQuery, PagingDto<BorrowedBookDto>>
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
        public async Task<PagingDto<BorrowedBookDto>> Handle(GetBorrowedBookExpiredSoonQuery request, CancellationToken cancellationToken)
        {

            try
            {
                var Skip = request.CurrentPage;
                if (Skip > 0) { Skip = (Skip - 1) * request.PageSize; }
                int recordsTotal = 0;
                int PageCount = 0;
                int CurrentPage = request.CurrentPage == 0 ? 1 : request.CurrentPage;

                var BorrowedBooks = await _context.BorrowedBooks.Where(a => !a.Deleted
                && (string.IsNullOrEmpty(request.Name) || a.Book.Name.ToUpper().Contains(request.Name.ToUpper()))
                && (((DateTime.Today - a.ReturnDate).Days) <= 2))
                    .Select(a =>
                new BorrowedBookDto
                {
                    Id = a.Id,
                    AppUserId = a.AppUserId,
                    CusomerName = a.AppUser.Name ?? "-",
                    Amount = a.Amount,
                    ReturnDate = a.ReturnDate,
                    ReturnDateStr = a.ReturnDate.ToString("yyyy-MM-dd"),

                }).Skip(Skip).Take(request.PageSize).ToListAsync();

                recordsTotal = await _context.BorrowedBooks.Where(a => !a.Deleted
                && (string.IsNullOrEmpty(request.Name) || a.Book.Name.ToUpper().Contains(request.Name.ToUpper()))
                && (((DateTime.Today - a.ReturnDate).Days) <= 2))
                    .Select(a =>
                new BorrowedBookDto
                {
                    Id = a.Id,
                    AppUserId = a.AppUserId,
                    CusomerName = a.AppUser.Name ?? "-",
                    Amount = a.Amount,
                    ReturnDate = a.ReturnDate,
                    ReturnDateStr = a.ReturnDate.ToString("yyyy-MM-dd"),

                }).CountAsync();

                PageCount = (recordsTotal / request.PageSize) + ((recordsTotal % request.PageSize) > 0 ? 1 : 0);


                return new PagingDto<BorrowedBookDto>
                {
                    data = BorrowedBooks,
                    PagesCount = PageCount,
                    CurrentPage = CurrentPage,
                    recordsTotal = recordsTotal,
                };
            }
            catch (Exception)
            {

                return new PagingDto<BorrowedBookDto>
                {
                    data = new List<BorrowedBookDto>(),
                    PagesCount = 1,
                    CurrentPage = 1,
                    recordsTotal = 0,
                };
            }

        }
    }
}
