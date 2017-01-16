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
using PaxDal;

namespace PaxWebApi.Tests.Controllers
{
    [TestClass]
    public class BooksComputationTest
    {
        [TestMethod]
        public void ComputeHeartBooksOk()
        {
            // Act
            IEnumerable<Books> result = BooksComputation.ComputeHeartBooks();

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(2, result.Count());
            Assert.AreEqual("value1", result.ElementAt(0));
            Assert.AreEqual("value2", result.ElementAt(1));
        }        
    }
}
