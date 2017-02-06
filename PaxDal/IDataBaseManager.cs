using PaxDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaxDal
{
    public interface IDataBaseManager
    {
        /// <summary>
        /// Get Heart books
        /// </summary>
        /// <returns>List of heart books</returns>
        List<Book> GetHeartBooks();

        /// <summary>
        /// Get Heart books
        /// </summary>
        /// <param name="booksToAdd"></param>
        /// <returns></returns>
        bool AddHeartBooks(List<Book> booksToAdd);

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
