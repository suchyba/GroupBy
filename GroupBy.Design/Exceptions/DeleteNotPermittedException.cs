namespace GroupBy.Design.Exceptions
{
    public class DeleteNotPermittedException : ApplicationException
    {
        public DeleteNotPermittedException(string name) : base($"Cannot delete {name}, because it's used by other object.")
        {

        }
    }
}
