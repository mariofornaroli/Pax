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
            object defaultsNotif = new
            {
                body = "Des livres récents ont été conseillés",
                title = "Librairie Pax",
                icon = "fcm_push_icon",
                sound = "default",
                click_action = "FCM_PLUGIN_ACTIVITY",
                color = "#B71C1C"
            };
            string topics = "/topics/testPaxNewHeratBooks";
            NotifComputation notiffComputation = new NotifComputation(new HttpConfiguration());
            // Act
            var ret = notiffComputation.executeNotif(defaultsNotif, null, topics);            
        }
        
    }
}
