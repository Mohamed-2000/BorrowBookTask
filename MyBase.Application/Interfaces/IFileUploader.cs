using Microsoft.VisualBasic.FileIO;
using MyBase.Application.Infrastructure;
using MyBase.Domain.Enums;

namespace MyBase.Application.Interfaces;
public interface IFileUploader
{
    Task<FileUploaderResult> Save(IFormFile file, IContentTypeValidator contentTypeValidator, FileType type = FileType.Image);
    FileUploaderResult Delete(string wwwroot, string FolderName, string FileName);
}