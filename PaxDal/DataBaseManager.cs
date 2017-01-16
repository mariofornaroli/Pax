using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data;
using System;

namespace PaxDal
{
    public class DataBaseManager : IDataBaseManager
    {
        PAXEntities entities;
        protected DbContextTransaction transaction;

        public DataBaseManager()
        {
            entities = new PAXEntities();
        }
        #region transaction
        public void BeginDBTransaction()
        {
            transaction = entities.Database.BeginTransaction();
        }
        public void EndDBTransaction()
        {
            transaction.Commit();
        }
        public void RollBackDBTransaction()
        {
            transaction.Rollback();
        }

        #endregion

        #region books

        /// <summary>
        /// Get Heart books
        /// </summary>
        /// <returns>List of heart books</returns>
        public List<Books> GetHeartBooks()
        {
            return entities.Books.ToList();
        }

        #endregion
    }
}
