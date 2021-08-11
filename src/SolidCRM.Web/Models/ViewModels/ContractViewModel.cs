using System;

namespace SolidCRM.Web
{
    public class ContractViewModel
    {
        public int Id { get; set; }

        public string Subject { get; set; }

        public Decimal ContractValue { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string Description { get; set; }

        public string CompanyClientId { get; set; }

        public string ContractTypeId { get; set; }

        public string ContractStatusId { get; set; }


    }
}

