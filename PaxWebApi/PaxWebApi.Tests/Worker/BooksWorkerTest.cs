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
using PaxWorker;

namespace PaxWebApi.Tests.Worker
{
    [TestClass]
    public class BooksWorkerTest
    {
        [TestMethod]
        public void ComputeHeartBooksOk()
        {
            // Act
            var bWork = new BooksWorker();
            var res = bWork.HeartBooks();

            // Assert
            Assert.AreEqual(res, true);
        }
        
    }
}
