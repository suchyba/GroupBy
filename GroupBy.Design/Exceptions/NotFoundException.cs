namespace GroupBy.Design.Exceptions
{
    public class NotFoundException : ApplicationException
    {
        public object Key { get; set; }
        public NotFoundException(string name, object key) : base($"{name} ({key}) is not found")
        {
            Key = key;
        }
    }
}
