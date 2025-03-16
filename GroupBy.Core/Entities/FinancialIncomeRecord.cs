using System.Linq;

namespace GroupBy.Domain.Entities
{
    /// <summary>
    /// Financial record for income (for example membership fee) <seealso cref="FinancialRecord"/>
    /// </summary>
    public class FinancialIncomeRecord : FinancialRecord
    {
        /// <summary>
        /// Sum of all variables
        /// </summary>
        /// <returns></returns>
        public override decimal GetTotal()
        {
            return Values.Where(c => c.Category.Income).Sum(v => v.Value);
        }
    }
}
