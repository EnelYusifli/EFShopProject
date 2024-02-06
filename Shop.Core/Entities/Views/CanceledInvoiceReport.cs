using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shop.Core.Entities.Views;

public class CanceledInvoiceReport
{
    public decimal TotalPrice { get; set; }
    public DateTime CreatedDate { get; set; }
}
