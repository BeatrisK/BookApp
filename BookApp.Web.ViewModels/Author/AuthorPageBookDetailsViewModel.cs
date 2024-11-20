using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookApp.Web.ViewModels.Author
{
    public class AuthorPageBookDetailsViewModel
    {
        public string Title { get; set; } = null!;

        public string Genre { get; set; } = null!;

        public int Pages { get; set; }

        public string Publisher { get; set; } = null!;

        public string ImageUrl { get; set; } = null!;
    }
}
