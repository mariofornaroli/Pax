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
        public List<BookItem> GetHeartBooks()
        {
            return BooksComputation.ComputeHeartBooks();
        }

        /// <summary>
        /// Get Heart book details
        /// </summary>
        /// <returns>Book details</returns>
        public BookDetailsItem GetBookDetails(string completeHref)
        {
            return BooksComputation.ComputeBookDetails(completeHref);
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
