﻿using System;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Script.Serialization;

namespace PaxComputation
{
    public class NotifComputation
    {
        private string FCM_SEND_API = "https://fcm.googleapis.com/fcm/send";
        //private string SERVER_KEY = "AAAA4db8KbY:APA91bF17AT3HwYU_DCL6Nw93usEPbOiOwdz3bYuQbkEiWeskfn_iNuIvI4YrMiI9vQBkAD0xPuYF5WVo2N-t1tgbyc62u0Lm4eurL9fBBe0bsRhTAZ38b1s_hKaE_ypKof7BJFEB2I0";
        //private string SENDER_ID = "969974491574";
        private string SERVER_KEY = "AAAAaa8gkUU:APA91bEMVNitP2fTr6djxOp6nPv_IVJP-bGg0zMYpYd74WXeXSyhi56G2iltW2cQgArqGYRHH5fpoEIKhowAnUPEGSy1HRPOIIhXf1UbTrelRXl8uN1djxEoA-a0NTd196SOl07FXdR0";
        private string SENDER_ID = "453909713221";
        private string TOPICS = "/topics/paxNewHeratBooks";

        private HttpConfiguration config;

        #region default variables

        private object defaultsNotif = new
        {
            body = "Des livres récents ont été conseillés",
            title = "Librairie Pax",
            icon = "fcm_push_icon",
            sound = "default",
            //click_action = "FCM_PLUGIN_ACTIVITY",
            color = "#154991"
        };

        private object defaultData = new
        {
            numberNewBooks = "1",
            //param2 = "value2"
        };

        #endregion

        public NotifComputation(HttpConfiguration _config)
        {
            config = _config;
        }


        #region NotifComputation Methods

        public async Task executeNotif(object notifContent, object dataToSend = null, string topics = "")
        {
            try
            {
                await PushNotification(notifContent, dataToSend, topics);
            }
            catch (System.Exception ex)
            {
                var errMsg = ex.Message;
            }
        }

        private async Task PushNotification(object notifContent, object dataToSend = null, string topics = "")
        {
            try
            {
                WebRequest tRequest = WebRequest.Create(FCM_SEND_API);
                tRequest.Method = "post";
                tRequest.ContentType = "application/json";

                /* Check data */
                if (notifContent == null)
                {
                    notifContent = defaultsNotif;
                }
                if (dataToSend == null) {
                    dataToSend = defaultData;
                }
                if (string.IsNullOrEmpty(topics))
                {
                    topics = TOPICS;
                }

                var data = new
                {
                    //to = token or topics here...,
                    to = topics,
                    notification = notifContent,
                    data = dataToSend,
                    priority = "high"
                };

                var serializer = new JavaScriptSerializer();

                var json = serializer.Serialize(data);

                Byte[] byteArray = Encoding.UTF8.GetBytes(json);

                tRequest.Headers.Add(string.Format("Authorization: key={0}", SERVER_KEY));

                tRequest.Headers.Add(string.Format("Sender: id={0}", SENDER_ID));

                tRequest.ContentLength = byteArray.Length;


                using (Stream dataStream = tRequest.GetRequestStream())
                {

                    dataStream.Write(byteArray, 0, byteArray.Length);


                    using (WebResponse tResponse = tRequest.GetResponse())
                    {

                        using (Stream dataStreamResponse = tResponse.GetResponseStream())
                        {

                            using (StreamReader tReader = new StreamReader(dataStreamResponse))
                            {

                                String sResponseFromServer = tReader.ReadToEnd();

                                string str = sResponseFromServer;

                            }
                        }
                    }
                }
            }

            catch (Exception ex)
            {

                string str = ex.Message;

            }

        }

        #endregion

    }
}



/*
    //POST: https://fcm.googleapis.com/fcm/send
    //HEADER: Content-Type: application/json
    //HEADER: Authorization: key=AIzaSy*******************

    {
      "notification":{
        "title":"Notification title",
        "body":"Notification body",
        "sound":"default",
        "click_action":"FCM_PLUGIN_ACTIVITY",
        "icon":"fcm_push_icon"
      },
      "data":{
        "param1":"value1",
        "param2":"value2"
      },
        "to":"/topics/topicExample",
        "priority":"high",
        "restricted_package_name":""
    }

    //sound: optional field if you want sound with the notification
    //click_action: must be present with the specified value for Android
    //icon: white icon resource name for Android >5.0
    //data: put any "param":"value" and retreive them in the JavaScript notification callback
    //to: device token or /topic/topicExample
    //priority: must be set to "high" for delivering notifications on closed iOS apps
    //restricted_package_name: optional field if you want to send only to a restricted app package (i.e: com.myapp.test)  
 */
