using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace InventoryStockUI.Models
{
    public class InventoryItem
    {
        public string Description { get; set; }

        public DateTime AddedDate { get; set; }

        public DateTime? SoldDate { get; set; }

        public DateTime? RemovedDate { get; set; }

        public string RemovedReason { get; set; }

        public decimal Valued { get; set; }

        public decimal Purchased { get; set; }

        public decimal? SoldAt { get; set; }
    }
}
