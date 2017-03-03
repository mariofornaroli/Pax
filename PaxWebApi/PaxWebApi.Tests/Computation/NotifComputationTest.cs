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
    public class NotifComputationTest
    {
        [TestMethod]
        public void NotifOk()
        {
            NotifComputation notiffComputation = new NotifComputation(new HttpConfiguration());
            // Act
            var ret = notiffComputation.executeNotif(null);            
        }
        
    }
}
