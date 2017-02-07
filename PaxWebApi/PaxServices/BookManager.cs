using Entities;
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
        public List<BookItem> GetHeartBooks()
        {
            return myDatabaseManager.GetHeartBooks();
        }
    }
}
