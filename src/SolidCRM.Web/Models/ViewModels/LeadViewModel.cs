using System;

namespace SolidCRM.Web
{
    public class LeadViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Position { get; set; }

        public string Email { get; set; }

        public string Website { get; set; }

        public string Phone { get; set; }

        public string Company { get; set; }

        public string Description { get; set; }

        public string LeadStatusId { get; set; }

        public string SourceId { get; set; }

        public string AssignedToUserId { get; set; }

        public DateTime OnDated { get; set; }

        public string Address { get; set; }

        public string CountryId { get; set; }


    }
}

