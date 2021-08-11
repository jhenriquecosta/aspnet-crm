using System;

namespace SolidCRM.Web
{
    public class ProjectTaskViewModel
    {
        public int Id { get; set; }

        public string ProjectId { get; set; }

        public string TaskNames { get; set; }

        public string Description { get; set; }

        public int UserId { get; set; }

        public DateTime? StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        public DateTime? DateAdded { get; set; }

        public DateTime? DateModified { get; set; }

        public int? AddedBy { get; set; }

        public int? ModifiedBy { get; set; }

        public string StatusId { get; set; }

        public string PriorityId { get; set; }

        public string ParentId { get; set; }

        public string ProjectMileStoneId { get; set; }


    }
}

