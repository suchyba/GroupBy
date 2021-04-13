using System;

namespace GroupBy.Data.Models
{
    public abstract class FinancialRecord
    {
        // klucz 
        //      książka do której należy wpis
        public int BookId { get; set; }
        public int BookOrderNumber { get; set; }
        //      id wpisu w książce
        public int Id { get; set; }
        // klucz
        public decimal TotalAmount { get; protected set; }
        public abstract void CalculateTotalAmount();
        public DateTime Date { get; set; }
        public string Description { get; set; }
        public string Document { get; set; }
        public virtual Project AssignedProject { get; set; }
    }
}