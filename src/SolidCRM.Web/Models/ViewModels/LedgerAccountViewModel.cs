using System;

namespace SolidCRM.Web
{
    public class LedgerAccountViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public int CurrencyId { get; set; }

        public string AccountCode { get; set; }

        public string AccountColor { get; set; }

        public string ParentId { get; set; }

        public string CompanyId { get; set; }

        public DateTime? DateAdded { get; set; }

        public int? AddedBy { get; set; }


    }
}

