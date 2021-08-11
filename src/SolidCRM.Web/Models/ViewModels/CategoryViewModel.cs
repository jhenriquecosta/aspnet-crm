using System;

namespace SolidCRM.Web
{
    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Details { get; set; }

        public string CategoryImage { get; set; }

        public string ParentId { get; set; }

        public bool IsActive { get; set; }


    }
}

