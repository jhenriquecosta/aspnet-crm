using System;

namespace SolidCRM.Web
{
    public class InvoiceItemViewModel
    {
        public int Id { get; set; }

        public string InvoiceId { get; set; }

        public string Description { get; set; }

        public string Title { get; set; }

        public Decimal Quantity { get; set; }

        public Decimal UnitPrice { get; set; }

        public string QuantityUnitId { get; set; }

        public Decimal Total { get; set; }

        public Decimal? Tax { get; set; }

        public Decimal? Discount { get; set; }

        public Decimal? Adjustment { get; set; }


    }
}

