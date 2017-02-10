using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entities
{
    public interface IDataBaseManager
    {
        /// <summary>
        /// Get Heart books
        /// </summary>
        /// <returns>List of heart books</returns>
        List<BookItem> GetHeartBooks();

        /// <summary>
        /// Get Heart books
        /// </summary>
        /// <param name="booksToAdd"></param>
        /// <returns></returns>
        bool AddHeartBooks(List<BookItem> booksToAdd);

        /// <summary>
        /// Get book details
        /// </summary>
        /// <param name="completeHref"></param>
        /// <returns></returns>
        BookDetailsItem GetDetailsBook(string completeHref);

        /// <summary>
        /// begin Transaction
        /// </summary>
        void BeginDBTransaction();

        /// <summary>
        /// End Transaction
        /// </summary>
        void EndDBTransaction();

        /// <summary>
        /// Rollback Transaction
        /// </summary>
        void RollBackDBTransaction();
    }
}
