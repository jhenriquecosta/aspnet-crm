using System;

namespace SolidCRM.Web
{
    public class NotesViewModel
    {
        public int Id { get; set; }

        public string Note { get; set; }

        public string AddedByUserId { get; set; }

        public DateTime? DateAdded { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? DateModied { get; set; }


    }
}

