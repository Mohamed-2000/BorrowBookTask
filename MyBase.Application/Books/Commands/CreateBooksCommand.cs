using MyBase.Application.Books.Models;
using MyBase.Application.Infrastructure;
using MyBase.Common;
using MyBase.Domain.Entities;

namespace MyBase.Application.Books.Commands;
public class CreateBooksCommand : IRequest<CommandResponse<string>>
{
    public string Name { get; set; }
    public int CategoryId { get; set; }
    public string AuthorId { get; set; }
    public string PictureUrl { get; set; }
    public IFormFile? Image { get; set; }
    public string? WebRootPath { get; set; }
    public string? UserId { get; set; }
    public class handler : IRequestHandler<CreateBooksCommand, CommandResponse<string>>
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


        public async Task<CommandResponse<string>> Handle(CreateBooksCommand request, CancellationToken cancellationToken)
        {
            try
            {

                FileUploaderResult UploadedPicture = null;
                if (request.Image != null)
                {
                    UploadedPicture = await _fileUploader.Save(request.Image, new ImageContentTypeValidator(request.WebRootPath, FileLocation.BooksImages));
                }

                if (!UploadedPicture.Status)
                {
                    return new CommandResponse<string> { Data = null, Success = false, Message = UploadedPicture.Error };
                }


                var Book = new Book
                {
                    Name = request.Name,
                    CategoryId = request.CategoryId,
                    AuthorId = request.AuthorId,
                    PictureUrl = UploadedPicture.FileName,
                    CreationDate = DateTime.Now,
                    CreatedById = request.UserId,
                    Deleted = false,
                };

                await _Context.Books.AddAsync(Book);
                await _Context.SaveChangesAsync(cancellationToken);


                return new CommandResponse<string> { Data = null, Success = true, Message = "Book Created Successfully" };

            }
            catch (Exception ex)
            {
                return new CommandResponse<string> { Data = null, Success = false, Message = ex.Message };
            }
        }
    }
}
