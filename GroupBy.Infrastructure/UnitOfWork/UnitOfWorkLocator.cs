using GroupBy.Design.UnitOfWork;
using System.Collections.Generic;

namespace GroupBy.Data.UnitOfWork
{
    public class UnitOfWorkLocator : IUnitOfWorkLocator<UnitOfWork>
    {
        private Stack<UnitOfWork> unitOfWorks = new Stack<UnitOfWork>();
        public Stack<UnitOfWork> UnitOfWorks
        {
            get
            {
                if (unitOfWorks == null)
                {
                    unitOfWorks = new Stack<UnitOfWork>();
                }
                return unitOfWorks;
            }
        }
    }
}
