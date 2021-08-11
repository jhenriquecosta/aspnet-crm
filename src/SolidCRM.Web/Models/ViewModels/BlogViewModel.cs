using System;

namespace SolidCRM.Web
{
    public class BlogViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        public int? AddedBy { get; set; }

        public DateTime DateAdded { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? DateModied { get; set; }

        public bool IsPublished { get; set; }

        public int? ViewCount { get; set; }


    }
}

