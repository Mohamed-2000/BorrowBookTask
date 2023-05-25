using MyBase.Application.Books.Models;
using MyBase.Application.BorrowedBook.Commands;
using MyBase.Domain.Entities;

namespace MyBase.Application.Fine.Commands;
public class CreateFineCommand : IRequest<CommandResponse<string>>
{
    public Guid AppUserId { get; set; }
    public string? UserId { get; set; }
    public int? BorrowedBookId { get; set; }
    public class handler : IRequestHandler<CreateFineCommand, CommandResponse<string>>
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


        public async Task<CommandResponse<string>> Handle(CreateFineCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var BorrowedBook = await _Context.BorrowedBooks.FindAsync(request.BorrowedBookId);

                var NumberOfLateDays =  (DateTime.Today - BorrowedBook.ReturnDate).Days;

                var FineAmount = NumberOfLateDays * 10;
                var Fines = new Fines
                {
                    AppUserId = request.AppUserId,
                    Amount = FineAmount,
                    CreationDate = DateTime.Now,
                    CreatedById = request.UserId,
                    Deleted = false,
                };

                await _Context.Fines.AddAsync(Fines);
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
