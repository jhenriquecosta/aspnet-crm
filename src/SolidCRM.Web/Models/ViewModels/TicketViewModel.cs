using System;

namespace SolidCRM.Web
{
    public class TicketViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string TicketDetail { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string CompanyClientId { get; set; }

        public string StatusId { get; set; }

        public string PriorityId { get; set; }

        public string CompanyId { get; set; }

        public int? AddedBy { get; set; }

        public DateTime? DateAdded { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? DateModied { get; set; }

        public bool? IsDelete { get; set; }


    }
}

