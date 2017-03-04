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
    public class FileComputationTest
    {
        [TestMethod]
        public void HeartBooksGetFileContentOk()
        {
            FileComputation fc = new FileComputation();

            var ret = fc.getFile();

            Console.Write(ret);
        }

        [TestMethod]
        public void HeartBooksDetailsGetFileContentOk()
        {
            FileComputation fc = new FileComputation();

            var ret = fc.getFile(string.Empty, string.Empty, "heartBooksDetails.txt");

            Console.Write(ret);
        }

    }
}
