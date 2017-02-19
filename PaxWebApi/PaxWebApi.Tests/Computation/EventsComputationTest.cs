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
    public class EventsComputationTest
    {
        [TestMethod]
        public void EventsOk()
        {
            // Act
            EventsModel result = EventsComputation.ComputeEvents();

            // Assert
            Assert.IsNotNull(result);
            Assert.IsNotNull(result.Events);
        }
        
    }
}
