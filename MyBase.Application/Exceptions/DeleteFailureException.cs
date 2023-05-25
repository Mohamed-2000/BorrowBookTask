namespace MyBase.Application.Exceptions
{
    public class DeleteFailureException : Exception
    {
        public DeleteFailureException(string message)
            : base($"{message}")
        {
        }
    }
}
