using System;
namespace ExpenseTracker
{
    public enum Categories
    {
        Housing,
        Transportation,
        Food,
        Utilities,
        Insurance,
        Health,
        Savings,
        Recreation
    }

    public class Expense
    {

        public string Name { get; set; } 
        public int Amount { get; set; }
        public DateTime Date { get; set; }
        public string FileName { get; set; }
        //public Categories Category { get; set; }
    }
}

