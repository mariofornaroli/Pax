using Entities;
using PaxComputation;
using PaxDal;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaxWorker
{
    public class BooksWorker
    {
        private IDataBaseManager dataBaseManager = new BooksComputationManager();

        //public BooksWorker(IDataBaseManager _dataBaseManager)
        //{
        //    dataBaseManager = _dataBaseManager;
        //}

        public bool HeartBooks()
        {
            // Compute books to add
            HeartBooksModel resultCompute = BooksComputation.ComputeHeartBooks();

            // add books to db
            return dataBaseManager.AddHeartBooks(resultCompute.HeartBooks.ToList());

            //// add books to db
            //var booksToAdd = resultCompute.Select(x => Mapper.ServiceToDalBookMapper(x)).ToList();
            //return dataBaseManager.AddHeartBooks(booksToAdd);
        }
    }
}
