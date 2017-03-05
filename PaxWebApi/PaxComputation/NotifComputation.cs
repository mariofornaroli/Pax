using System;
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
        private string SERVER_KEY = "AAAA4db8KbY:APA91bF17AT3HwYU_DCL6Nw93usEPbOiOwdz3bYuQbkEiWeskfn_iNuIvI4YrMiI9vQBkAD0xPuYF5WVo2N-t1tgbyc62u0Lm4eurL9fBBe0bsRhTAZ38b1s_hKaE_ypKof7BJFEB2I0";
        private string SENDER_ID = "969974491574";
        private string TOPICS = "/topics/paxNewHeratBooks";

        private HttpConfiguration config;

        #region default variables

        private object defaultsNotif = new
        {
            body = "Defauls body msg 2",
            title = "Default title msg 2",
            icon = "myicon"
        };

        private object defaultData = new
        {
            param1 = "value1",
            param2 = "value2"
        };

        #endregion

        public NotifComputation(HttpConfiguration _config)
        {
            config = _config;
        }


        #region NotifComputation Methods

        public async Task executeNotif(object notifContent)
        {
            try
            {
                await PushNotification(notifContent);
            }
            catch (System.Exception ex)
            {
                var errMsg = ex.Message;
            }
        }

        private async Task PushNotification(object notifContent)
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

                var data = new
                {
                    //to = token or topics here...,
                    to = TOPICS,
                    notification = notifContent,
                    data = defaultData
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
