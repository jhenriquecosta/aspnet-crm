using System;

namespace SolidCRM.Web
{
    public class TicketTaskViewModel
    {
        public int Id { get; set; }

        public string TicketId { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public string TaskDetail { get; set; }

        public string StatusId { get; set; }

        public string PriorityId { get; set; }

        public int? AddedBy { get; set; }

        public DateTime? DateAdded { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? DateModied { get; set; }

        public bool? IsDelete { get; set; }

        public Decimal? TakeTimeInHour { get; set; }


    }
}

