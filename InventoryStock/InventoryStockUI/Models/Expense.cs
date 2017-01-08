using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryStockUI.Models
{
    public class Expense
    {
        public DateTime ExpenseDate { get; set; }

        public string Description { get; set; }

        public decimal Cost { get; set; }

        public string ReceiptNumber { get; set; }
    }
}
