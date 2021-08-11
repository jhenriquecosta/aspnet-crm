using System;

namespace SolidCRM.Web
{
    public class TodoViewModel
    {
        public int Id { get; set; }

        public string Description { get; set; }

        public string UserId { get; set; }

        public DateTime? DateAdded { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? DateModied { get; set; }


    }
}

