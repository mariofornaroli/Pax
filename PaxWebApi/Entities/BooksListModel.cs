using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class BooksListModel
    {
        public List<BookItem> BooksList { get; set; }
        public BookItem MonthBook { get; set; }
        public BookTypeEnum BookTypeEnum { get; set; }

        /* Constructors */
        public BooksListModel() { }
        public BooksListModel(BookTypeEnum bookTypeEnum) {
            BookTypeEnum = bookTypeEnum;
        }

        /* Following fields are used only for retro compatibility and will be removed */
        //public List<BookItem> HeartBooks { get { return BooksList; } }
        //public List<BookItem> BestSellers { get { return BooksList;} }
    }
}
