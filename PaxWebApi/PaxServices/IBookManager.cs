using Entities;
using PaxDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaxServices
{
    public interface IBookManager
    {
        List<BookItem> GetHeartBooks();

        BookDetailsItem GetDetailsBook(string completeHref);
    }
}
