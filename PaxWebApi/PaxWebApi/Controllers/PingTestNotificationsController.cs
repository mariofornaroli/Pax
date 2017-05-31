using Entities;
using PaxComputation;
using PaxServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PaxWebApi.Controllers
{
    public class PingTestNotificationsController : ApiController
    {
        private IBookManager bookManager = null;

        public PingTestNotificationsController(IBookManager _bookManager)
        {
            bookManager = _bookManager;
        }

        // Allow CORS for all origins. (Caution!)
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public BaseResultModel Get(string pswd)
        {
            var resultModel = new BaseResultModel();

            /* Execute test only for correct pswd */
            if (pswd == "334455")
            {
                object defaultData = new
                {
                    numberNewBooks = "1"
                };

                object defaultsNotif = new
                {
                    body = "Des livres récents ont été conseillés",
                    title = "Librairie Pax",
                    icon = "fcm_push_icon",
                    sound = "default",
                    //click_action = "FCM_PLUGIN_ACTIVITY",
                    color = "#154991"
                };
                string topics = "/topics/paxNewHeratBooks";
                NotifComputation notiffComputation = new NotifComputation(new HttpConfiguration());
                // Act
                var ret = notiffComputation.executeNotif(defaultsNotif, defaultData, topics);

            }
            /* Return data */
            resultModel.OperationResult = true;
            return resultModel;
        }
    }
}
