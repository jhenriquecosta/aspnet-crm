using System;

namespace SolidCRM.Web
{
    public class NewsMediaViewModel
    {
        public int Id { get; set; }

        public string BlogId { get; set; }

        public string MediaFile { get; set; }

        public int? DisplaySortOrder { get; set; }

        public int? AddedBy { get; set; }

        public DateTime? DateAdded { get; set; }

        public int? ModifiedBy { get; set; }

        public DateTime? DateModied { get; set; }


    }
}

