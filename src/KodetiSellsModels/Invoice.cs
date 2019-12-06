using System;
using System.Collections.Generic;
using System.Text;

namespace KodetiSellsModels
{
    public class Invoice
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public decimal Iva { get; set; }
        public decimal SubTotal { get; set; }
        public decimal Total { get; set; }

        //relation
        public Client Client { get; set; }
    }
}
