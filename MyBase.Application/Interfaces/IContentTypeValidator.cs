namespace MyBase.Application.Interfaces;
public interface IContentTypeValidator
{
    string Path { get; }
    bool IsValidContentType(string ContentType);
}
