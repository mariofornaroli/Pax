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
        List<Books> GetHeartBooks();

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
