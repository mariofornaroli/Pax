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
        public string CompleteHref { get; set; }
        public string Title { get; set; }
        public string ImgSrc { get; set; }
        public string Author { get; set; }
        public string AuthorHref { get; set; }
        public string Type { get; set; }
        public string Editor { get; set; }
        public string Price { get; set; }
        public List<string> TypeExisting { get; set; }
        public List<DescriptionItem> AdditionalDescriptionItems { get; set; }
        public List<DescriptionItem> GlobalInfoDescriptionItems { get; set; }
        public List<DescriptionItem> OtherInfoTableItems { get; set; }        
        public string DescriptionTitle { get; set; }
        public string Description { get; set; }
        public string PublishedDate { get; set; }
        public string TraductionInfo { get; set; }
        public List<BookDetailsItem> LastBooksOfThisAuthor { get; set; }
        public string Collection { get; set; }
        public string MiseEnLigne { get; set; }
    }
}
