using System;

namespace SolidCRM.Web
{
    public class TransactionViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public DateTime DateAdded { get; set; }

        public int AddedBy { get; set; }

        public string CompanyId { get; set; }

        public string DebitLedgerAccountId { get; set; }

        public Decimal DebitAmount { get; set; }

        public string CreditLedgerAccountId { get; set; }

        public Decimal CreditAmount { get; set; }

        public DateTime TransactionDate { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? DateModied { get; set; }

        public string Attachment { get; set; }


    }
}

