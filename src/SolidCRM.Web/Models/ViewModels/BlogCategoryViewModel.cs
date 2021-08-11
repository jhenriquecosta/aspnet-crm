using System;

namespace SolidCRM.Web
{
    public class BlogCategoryViewModel
    {
        public int Id { get; set; }

        public string BlogId { get; set; }

        public string CategoryId { get; set; }

        public int? AddedBy { get; set; }

        public DateTime? DateAdded { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? DateModied { get; set; }


    }
}

