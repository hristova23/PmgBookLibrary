using Microsoft.AspNetCore.Mvc;
using Ganss.Xss;

namespace Library.Core.Models.Book
{
    public class BookViewModel
    {
        public int Id { get; set; }

        public string Title { get; set; }

        public string Publisher { get; set; }

        public string Description { get; set; }
        public string SanitizedDescription => new HtmlSanitizer().Sanitize(this.Description);

        public string ImageUrl { get; set; }

        [BindProperty]
        public string PdfUrl { get; set; }

        public string Category { get; set; }
    }
}
