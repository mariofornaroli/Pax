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
        BooksListModel GetSellerWords();

        DetailsBooksModel ComputeDetailsHeartBooks();

        BaseResultModel ComputePaxToFile();

        BookDetailsItem GetDetailsBook(string completeHref);

        EventsModel GetEvents();

        BooksListModel GetBestSellers();
        
    }
}
