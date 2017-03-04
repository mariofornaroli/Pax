using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Web.Http;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaxWebApi;
using PaxWebApi.Controllers;
using PaxComputation;
using Entities;

namespace PaxWebApi.Tests.Controllers
{
    [TestClass]
    public class BooksComputationTest
    {
        [TestMethod]
        public void ComputeHeartBooksOk()
        {
            // Act
            HeartBooksModel result = BooksComputation.ComputeHeartBooks();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.HeartBooks);
            Assert.AreEqual(12, result.HeartBooks.Count());
        }

        [TestMethod]
        public void ComputeHeartBooksToFileOk()
        {
            // Act
            HeartBooksModel result = BooksComputation.ComputeHeartBooksToFile();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.HeartBooks);
            Assert.AreEqual(12, result.HeartBooks.Count());
        }

        [TestMethod]
        public void ComputeBookDetailsOk()
        {
            string bookDetail = getFirstBookCompleteHref();
            // Act
            BookDetailsItem bookDetails = BooksComputation.ComputeBookDetails(bookDetail);

            // Assert
            Assert.IsNotNull(bookDetails);
        }

        private string getFirstBookCompleteHref()
        {
            var booksList = BooksComputation.ComputeHeartBooks();
            return booksList.HeartBooks.FirstOrDefault().CompleteHref;
        }

        [TestMethod]
        public void ComputeBestSellersOk()
        {
            // Act
            BestSellersModel result = BooksComputation.ComputeBestSellers();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.BestSellers);
        }

        [TestMethod]
        public void ComputeBestSellerDetailsOk()
        {
            string bookDetail = getFirstBestSellerHref();
            // Act
            BookDetailsItem bookDetails = BooksComputation.ComputeBookDetails(bookDetail);

            // Assert
            Assert.IsNotNull(bookDetails);
        }

        private string getFirstBestSellerHref()
        {
            var booksList = BooksComputation.ComputeBestSellers();
            return booksList.BestSellers.FirstOrDefault().CompleteHref;
        }

    }
}
