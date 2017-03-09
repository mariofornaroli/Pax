using Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaxComputation
{
    public class BooksComputationManager : IDataBaseManager
    {
        /// <summary>
        /// Get Heart books
        /// </summary>
        /// <returns>List of heart books</returns>
        public BooksListModel GetSellerWords()
        {
            return BooksComputation.GetSellerWords();
        }
        
        public BookDetailsItem GetDetailsBook(string completeHref)
        {
            return BooksComputation.GetBookDetails(completeHref);
        }

        public EventsModel GetEventss()
        {
            return EventsComputation.ComputeEvents();
        }

        public BooksListModel GetBestSellers()
        {
            return BooksComputation.GetBestSellers();
        }

        public bool AddHeartBooks(List<BookItem> booksToAdd)
        {
            throw new NotImplementedException();
        }

        public void BeginDBTransaction()
        {
            throw new NotImplementedException();
        }

        public void EndDBTransaction()
        {
            throw new NotImplementedException();
        }
                
        public void RollBackDBTransaction()
        {
            throw new NotImplementedException();
        }
    }
}
