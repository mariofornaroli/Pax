using System.Collections.Generic;
using System.Linq;
using System.Data.Entity;
using System.Data;
using System;
using PaxDal;
using Entities;
using PaxDal.Tools;

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
        public List<BookItem> GetHeartBooks()
        {
            throw new NotImplementedException();
            //var dbHeartBooks = dbContext.Books.ToList();
            //return dbHeartBooks.Select(x => Tools.Mapper.DalToServiceBookMapper(x)).ToList();
        }

        
        public bool AddHeartBooks(List<BookItem> booksToAdd)
        {
            throw new NotImplementedException();

            //// add books to db
            //var dbBooks = booksToAdd.Select(x => Mapper.ServiceToDalBookMapper(x)).ToList();

            ////create DBContext object
            //using (var dbContext = new PAXEntities())
            //{
            //    dbBooks.ForEach( x => dbContext.Books.Add(x));                
            //    // call SaveChanges method to save student into database
            //    dbContext.SaveChanges();
            //}

            return true;
        }

        public BookDetailsItem GetDetailsBook(string completeHref)
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
