using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BookItem
    {
        public string Id { get; set; }
        public string ImgSrc { get; set; }
        public string Href { get; set; }
        public string CompleteHref { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string AuthorHref { get; set; }
        public string Editor { get; set; }
        public string Price { get; set; }
        public string PublishedDate { get; set; }
        public DateTime? DateComputation { get; set; }
        public bool IsNewAdded { get; set; }
    }
}
