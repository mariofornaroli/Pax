using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data;
using System;
using PaxDal;
using Entities;

namespace PaxDal
{
    public class DataBaseManager : IDataBaseManager
    {
        PAXEntities dbContext;
        protected DbContextTransaction transaction;

        public DataBaseManager()
        {
            dbContext = new PAXEntities();
        }
        #region transaction
        public void BeginDBTransaction()
        {
            transaction = dbContext.Database.BeginTransaction();
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
        public List<Book> GetHeartBooks()
        {
            return dbContext.Books.ToList();
        }

        
        public bool AddHeartBooks(List<Book> booksToAdd)
        {
            //create DBContext object
            using (var dbContext = new PAXEntities())
            {
                booksToAdd.ForEach( x => dbContext.Books.Add(x));                
                // call SaveChanges method to save student into database
                dbContext.SaveChanges();
            }

            return true;
        }

        #endregion
    }
}
