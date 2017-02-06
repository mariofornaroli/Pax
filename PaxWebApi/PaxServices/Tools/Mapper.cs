using Entities;
using PaxDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaxServices.Tools
{
    public static class Mapper
    {
        #region Book

        public static BookItem DalToServiceBookMapper(Book book)
        {
            return new BookItem
            {
                Id = book.Id.ToString(),
                Title = book.Title,
                ShortDescription = book.Description,
                Description = book.Description,
                Author = book.Author,
                Href = book.Href,
                CompleteHref = book.CompleteHref,
                ImgSrc = book.ImgSrc,
                DateComputation = book.DateComputation
            };
        }

        public static Book ServiceToDalBookMapper(BookItem book)
        {
            return new Book
            {
                Id = book.Id != null ? Int32.Parse(book.Id) : -1,
                Title = book.Title,
                Description = book.Description,
                Author = book.Author,
                Href = book.Href,
                CompleteHref = book.CompleteHref,
                ImgSrc = book.ImgSrc,
                DateComputation = book.DateComputation
            };
        }

        #endregion


    }
}
