namespace GroupBy.Design.UnitOfWork
{
    public interface IUnitOfWorkLocator<UnitOfWork> where UnitOfWork : class
    {
        public Stack<UnitOfWork> UnitOfWorks { get; }
    }
}
