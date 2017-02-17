using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class HeartBooksModel
    {
        public List<BookItem> HeartBooks { get; set; }
        public BookItem MonthBook { get; set; }
    }
}
