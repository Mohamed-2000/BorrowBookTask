namespace MyBase.Application.Exceptions
{
    public class DuplicatedIndexException : Exception
    {
        public DuplicatedIndexException(string action, string name, object index, string message= "Index Is already exists")
            : base($"{action} entity \"{name}\" ({index}) failed. {message}")
        {

        }
    }
}
