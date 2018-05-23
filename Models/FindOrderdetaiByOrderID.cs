using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSchool.MvcSolution.OnlineStore.Models
{
    public class FindOrderdetaiByOrderID
    {
        public int OrderID { get; set; }
        public DateTime OrderDate { get; set; }
        public string ProductName { get; set; }
        public string Size { get; set; }
        public string Color { get; set; }
        public int Quantity { get; set; }
        public string Status { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
