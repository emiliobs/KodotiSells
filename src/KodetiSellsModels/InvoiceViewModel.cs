using System;
using System.Collections.Generic;
using System.Text;

namespace KodetiSellsModels
{
    public class InvoiceViewModel : Invoice
    {
        public InvoiceViewModel()
        {
            InvoiceDetails = new List<InvoiceDetail>();
        }

        public string Error { get; set; }
    }
}
