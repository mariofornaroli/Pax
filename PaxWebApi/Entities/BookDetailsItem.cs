using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BookDetailsItem
    {
        public string Href { get; set; }
        public string Title { get; set; }
        public string ImgSrc { get; set; }
        public string Author { get; set; }
        public string AuthorHref { get; set; }
        public string Type { get; set; }
        public string Editor { get; set; }
        public string Price { get; set; }
        public List<string> TypeExisting { get; set; }
        public Dictionary<string, string> AdditionalDescriptionItems { get; set; }
        public string DescrptionTitle { get; set; }
        public string Descrption { get; set; }
        public string PublishedDate { get; set; }
        public string TraductionInfo { get; set; }
        public List<BookDetailsItem> LastBooksOfThisAuthor { get; set; }
    }
}
