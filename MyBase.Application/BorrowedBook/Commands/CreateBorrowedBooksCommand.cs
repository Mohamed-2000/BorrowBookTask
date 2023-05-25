using MyBase.Application.Books.Commands;
using MyBase.Application.Books.Models;
using MyBase.Application.Infrastructure;
using MyBase.Common;
using MyBase.Domain.Entities;

namespace MyBase.Application.BorrowedBook.Commands;
public class CreateBorrowedBooksCommand : IRequest<CommandResponse<string>>
{
    public string BookId { get; set; }
    public string AppUserId { get; set; }
    public DateTime BorrowDate { get; set; }
    public DateTime ReturnDate { get; set; }
    public decimal Amount { get; set; }
    public string? UserId { get; set; }
    public class handler : IRequestHandler<CreateBorrowedBooksCommand, CommandResponse<string>>
    {
        private readonly IMyBaseContext _Context;
        private readonly IMediator _mediator;
        private readonly IFileUploader _fileUploader;

        public handler(IMyBaseContext Context, IMediator mediator, IFileUploader fileUploader)
        {
            _Context = Context;
            _mediator = mediator;
            _fileUploader = fileUploader;
        }


        public async Task<CommandResponse<string>> Handle(CreateBorrowedBooksCommand request, CancellationToken cancellationToken)
        {
            try
            {

               

                var BorrowedBook = new MyBase.Domain.Entities.BorrowedBook
                {
                    BookId = request.BookId,
                    AppUserId = request.AppUserId,
                    Amount = request.Amount,
                    BorrowDate = request.BorrowDate,
                    ReturnDate = request.ReturnDate,
                    CreationDate = DateTime.Now,
                    CreatedById = request.UserId,
                    Deleted = false,
                };

                await _Context.BorrowedBooks.AddAsync(BorrowedBook);
                await _Context.SaveChangesAsync(cancellationToken);


                return new CommandResponse<string> { Data = null, Success = true, Message = "Borrowed A Book Successfully" };

            }
            catch (Exception ex)
            {
                return new CommandResponse<string> { Data = null, Success = false, Message = ex.Message };
            }
        }
    }
}
