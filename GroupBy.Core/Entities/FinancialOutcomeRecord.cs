using System.Linq;

namespace GroupBy.Domain.Entities
{
    /// <summary>
    /// The financial record for outcome (for example invoice)
    /// </summary>
    public class FinancialOutcomeRecord : FinancialRecord
    {
        /// <summary>
        /// Override of abstract function (sums all sources) <seealso cref="FinancialRecord.GetTotal"/>
        /// </summary>
        public override decimal GetTotal()
        {
            return Values.Where(c => !c.Category.Income).Sum(v => v.Value);
        }
    }
}
