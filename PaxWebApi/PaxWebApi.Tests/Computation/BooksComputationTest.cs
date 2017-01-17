﻿using System;
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
            IEnumerable<BookItem> result = BooksComputation.ComputeHeartBooks();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(12, result.Count());
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
            return booksList.FirstOrDefault().CompleteHref;

        }
    }
}
