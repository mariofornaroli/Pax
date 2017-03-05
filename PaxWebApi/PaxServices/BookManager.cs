using Entities;
using PaxComputation;
using PaxDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaxServices
{
    public class BookManager : IBookManager
    {
        public IDataBaseManager myDatabaseManager { get; set; }

        public BookManager(IDataBaseManager dbManager)
        {
            myDatabaseManager = dbManager;
        }

        /// <summary>
        /// Get Heart books
        /// </summary>
        /// <returns>List of heart books</returns>
        public HeartBooksModel GetHeartBooks()
        {
            return myDatabaseManager.GetHeartBooks();
        }

        /// <summary>
        /// Get Details for heart books and details and store result to file
        /// </summary>
        /// <returns></returns>
        public DetailsBooksModel ComputeDetailsHeartBooks()
        {
            return BooksComputation.ComputeDetailsHeartBooks();
        }

        /// <summary>
        /// Get Heart books and details and store result to file
        /// </summary>
        /// <returns></returns>
        public BaseResultModel ComputeHeartBooksAndDetailsToFile()
        {
            return BooksComputation.ComputeHeartBooksToFile();
        }

        public BookDetailsItem GetDetailsBook(string completeHref)
        {
            return myDatabaseManager.GetDetailsBook(completeHref);
        }

        public EventsModel GetEvents()
        {
            return myDatabaseManager.GetEventss();
        }

        public BestSellersModel GetBestSellers()
        {
            return myDatabaseManager.GetBestSellers();
        }
    }
}
