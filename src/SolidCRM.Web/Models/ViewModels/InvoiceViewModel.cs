using System;

namespace SolidCRM.Web
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }

        public DateTime BillDate { get; set; }

        public DateTime? DueDate { get; set; }

        public string PaymentModeId { get; set; }

        public string To { get; set; }

        public string OtherInvoiceCode { get; set; }

        public string Address { get; set; }

        public int CreatedBy { get; set; }

        public string CompanyId { get; set; }

        public string CountryId { get; set; }

        public string ZipCode { get; set; }

        public string Email { get; set; }

        public string Mobile { get; set; }

        public string ClientAddressId { get; set; }


    }
}

