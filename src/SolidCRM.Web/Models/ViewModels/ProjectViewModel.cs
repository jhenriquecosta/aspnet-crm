using System;

namespace SolidCRM.Web
{
    public class ProjectViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int AddedBy { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public int CompanyOfficeId { get; set; }

        public string Status { get; set; }

        public string CompanyClientId { get; set; }

        public string Details { get; set; }

        public DateTime? TargetDate { get; set; }

        public DateTime? DateAdded { get; set; }

        public DateTime? DateModified { get; set; }

        public int? ModifiedBy { get; set; }

        public string StatusId { get; set; }

        public string PriorityId { get; set; }

        public string Tags { get; set; }

        public Decimal? Amount { get; set; }


    }
}

