using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public class DetailsBooksModel
    {
        public List<BookDetailsItem> DetailsBooks { get; set; }
        public BookTypeEnum BookType { get; set; }
    }
}
