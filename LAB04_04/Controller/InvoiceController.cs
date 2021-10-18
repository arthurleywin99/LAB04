using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using LAB04_04.Model;

namespace LAB04_04.Controller
{
    public class InvoiceController
    {
        public static List<Invoice> GetInvoice()
        {
            using (var context = new DBContextContainer())
            {
                return context.Invoices.ToList();
            }
        }

        public static long GetTotalByID(string InvoiceNo)
        {
            using (var context = new DBContextContainer())
            {
                var temp = context.Orders.Where(p => p.InvoiceNo == InvoiceNo).ToList();
                long ans = 0;
                foreach (var item in temp)
                {
                    ans += long.Parse(item.Price.ToString());
                }
                return ans;
            }
        }
    }
}
