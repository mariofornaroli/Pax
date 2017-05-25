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
        #region properties and constants

        private const int EXPECTED_SELLER_WORDS_BOOKS = 20;
        private const int EXPECTED_ADVICED_BOOKS = 12;
        private const int EXPECTED_BEST_SELLERS_BOOKS = 50;

        #endregion

        #region All PAX

        [TestMethod]
        public void ComputePaxToFileOK()
        {
            bool forceRefreshAllData = true;

            // Act
            BaseResultModel result = BooksComputation.ComputePaxToFile(forceRefreshAllData);

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void SimpleComputeSellerBooksOK()
        {
            bool forceRefreshAllData = true;

            // Act
            var sellerBooks = BooksComputation._ComputeSellerWords();

            // Assert
            Assert.IsNotNull(sellerBooks);
        }


        [TestMethod]
        public void GetAllBooksDetailsOk()
        {
            // Act
            DetailsBooksModel bookDetails = BooksComputation.GetAllBooksDetails();

            // Assert
            Assert.IsNotNull(bookDetails);
            Assert.IsNotNull(bookDetails.DetailsBooks);
            Assert.AreEqual(84, bookDetails.DetailsBooks.Count());
        }
        
        #endregion

        #region Get Seller Words Books and Details

        [TestMethod]
        public void _ComputeSellerWordsOK()
        {
            // Act
            BooksListModel result = BooksComputation._ComputeSellerWords();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.BooksList);
            Assert.AreEqual(EXPECTED_SELLER_WORDS_BOOKS, result.BooksList.Count());
            Assert.IsNotNull(result.MonthBook);
        }


        [TestMethod]
        public void GetSellerWordsOK()
        {
            // Act
            BooksListModel result = BooksComputation.GetSellerWords();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.BooksList);
            Assert.AreEqual(EXPECTED_SELLER_WORDS_BOOKS, result.BooksList.Count());
            Assert.IsNotNull(result.MonthBook);
        }

        [TestMethod]
        public void ComputeSellerBooksToFileAndNotification()
        {
            bool notificationsOccurred = false;
            object defaultsNotif = new
            {
                body = "Des livres récents ont été conseillés",
                title = "Librairie Pax",
                icon = "fcm_push_icon",
                sound = "default",
                click_action = "FCM_PLUGIN_ACTIVITY",
                color = "#154991"
            };
            string topics = "/topics/testPaxNewHeratBooks";
            // Act
            BaseResultModel result = BooksComputation.ComputeSellerBooksToFileAndNotification(ref notificationsOccurred, defaultsNotif, null, topics);

            Assert.IsNotNull(result);
        }

        //[TestMethod]
        //public void GetSellerWordsDetailsOK()
        //{
        //    // Act
        //    DetailsBooksModel bookDetails = BooksComputation.GetSellerWordsBooksDetails();

        //    // Assert
        //    Assert.IsNotNull(bookDetails);
        //    Assert.IsNotNull(bookDetails.DetailsBooks);
        //    Assert.AreEqual(20, bookDetails.DetailsBooks.Count());
        //}

        #endregion


        #region Get Adviced Books and Details

        [TestMethod]
        public void GetAdvicedBooksOk()
        {
            // Act
            BooksListModel result = BooksComputation.GetAdvicedBooks();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.BooksList);
            Assert.AreEqual(EXPECTED_ADVICED_BOOKS, result.BooksList.Count());
        }

        //[TestMethod]
        //public void GetAdvicedBooksDetailsOk()
        //{
        //    // Act
        //    DetailsBooksModel bookDetails = BooksComputation.GetAdvicedBooksDetails();

        //    // Assert
        //    Assert.IsNotNull(bookDetails);
        //    Assert.IsNotNull(bookDetails.DetailsBooks);
        //    Assert.AreEqual(EXPECTED_ADVICED_BOOKS, bookDetails.DetailsBooks.Count());
        //}

        private string getFirstBookCompleteHref()
        {
            //var booksList = BooksComputation.GetAdvicedBooks();
            var sellerBooks = BooksComputation._ComputeSellerWords();
            return sellerBooks.BooksList.FirstOrDefault().CompleteHref;
        }

        #endregion


        #region Get Best Sellers Books and Details

        [TestMethod]
        public void GetBestSellersOk()
        {
            // Act
            BooksListModel result = BooksComputation.GetBestSellers();

            Assert.IsNotNull(result);
            Assert.IsNotNull(result.BooksList);
            Assert.AreEqual(EXPECTED_BEST_SELLERS_BOOKS, result.BooksList.Count());
        }

        //[TestMethod]
        //public void GetBestSellersBooksDetailsOk()
        //{
        //    string bookDetail = getFirstBestSellerHref();
        //    // Act
        //    DetailsBooksModel bookDetails = BooksComputation.GetBestSellersBooksDetails();

        //    // Assert
        //    Assert.IsNotNull(bookDetails);
        //    Assert.IsNotNull(bookDetails.DetailsBooks);
        //    Assert.AreEqual(EXPECTED_BEST_SELLERS_BOOKS, bookDetails.DetailsBooks.Count());
        //}

        private string getFirstBestSellerHref()
        {
            var booksList = BooksComputation.GetBestSellers();
            return booksList.BooksList.FirstOrDefault().CompleteHref;
        }

        #endregion

        #region other

        [TestMethod]
        public void DetailsBooksForHeartBooksOk()
        {
            // Act
            DetailsBooksModel result = BooksComputation.GetAllBooksDetails();

            // Assert
            Assert.IsNotNull(result);
        }

        [TestMethod]
        public void ComputeBookDetailsOk()
        {
            //string bookDetail = getFirstBookCompleteHref();
            string bookDetail = "http://www.librairiepax.be//livre/9782081413146-quand-sort-la-recluse-fred-vargas/";
            // Act
            BookDetailsItem bookDetails = BooksComputation.GetBookDetails(bookDetail);

            // Assert
            Assert.IsNotNull(bookDetails);
        }

        #endregion

    }
}
