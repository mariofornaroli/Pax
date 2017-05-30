using Entities;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PaxComputation
{
    public static class EventsComputation
    {
        //private const string PAX_EVENTS_WEBSITE = "http://www.librairiepax.be/events.php?blid=5808";
        private const string PAX_WEBSITE_NO_SLASH = "http://www.librairiepax.be";
        private const string PAX_EVENTS_WEBSITE = "https://www.librairiepax.be/agenda.php";
        #region ComputeHeartBooks Methods

        /// <summary>
        /// Get Heart books
        /// </summary>
        /// <returns>List of heart books</returns>
        public static EventsModel ComputeEvents()
        {
            return _ComputeEvents();
        }

        private static EventsModel _ComputeEvents()
        {
            HtmlDocument doc = new HtmlDocument();
            HttpDownloader downloader = new HttpDownloader(PAX_EVENTS_WEBSITE, null, null);
            doc.LoadHtml(downloader.GetPage());

            return GetEventsObjList(doc);
        }

        public static EventsModel GetEventsObjList(HtmlDocument doc)
        {
            var retData = new EventsModel() { Events = new List<EventItem>() };

            /* Fill events title */
            FillEventsShortData(doc, retData.Events);

            /* Fill events Image */
            FillEventsImage(doc, retData.Events);

            /* Fill events Description */
            FillEventsDescription(doc, retData.Events);

            ///* Get coeurTdDoc object */
            //var coeurTdDoc = GetCoeurTdObj(doc);

            ///* Get coeurTdDoc Block list */
            //var groupBlocksList = GetGroupBlocksList(coeurTdDoc);

            ///* Get coeurTdDoc Img list and Description list */
            //var bookList = new List<BookItem>();
            //GetGeneralBookListItem(groupBlocksList, ref bookList);

            ///* Compute here the book of the month */
            //var monthBook = GetMonthBookTdObj(doc);

            ///* Assign return variables */
            //retData.HeartBooks = bookList;
            //retData.MonthBook = monthBook;

            return retData;
        }
        
        private static void FillEventsShortData(HtmlDocument doc, List<EventItem> events)
        {
            //desc_agenda
            HtmlNodeCollection eventsTitles = doc.DocumentNode
                //.SelectNodes("//div[@id='wrap_central']//div[contains(@class, 'infos_agenda')]//div[contains(@class, 'desc_agenda')]");
                .SelectNodes("//div[@id='wrap_central']//article");

            var countEvents = eventsTitles.Count;

            for (int i = 0; i < countEvents; i++)
            {
                var eventToAdd = new EventItem();

                /* Fill title */
                eventToAdd.Title = AgilityTool
                 .GetInnerText(eventsTitles[i], "div[contains(@class, 'infos_agenda')]/div[contains(@class, 'desc_agenda')]/h2/a");

                /* Fill title href */
                eventToAdd.CompleteHref = AgilityTool
                 .GetAttributeValue(eventsTitles[i], "href", "/h2/a");
                eventToAdd.CompleteHref = PAX_WEBSITE_NO_SLASH + eventToAdd.CompleteHref;

                /* Fill title */
                //HtmlNode titleNode = eventsTitles[i];
                //if (titleNode != null)
                //{
                //    eventToAdd.Title = titleNode.InnerText;
                //}
                ///* Add event to list */
                //events.Add(eventToAdd);
            }
        }

        private static void FillEventsImage(HtmlDocument doc, List<EventItem> events)
        {
            HtmlNodeCollection eventsImages = doc.DocumentNode.SelectNodes("//td[@class='EventsCorpus2']//div[@class='events_image']//img");

            var countEvents = eventsImages.Count;

            for (int i = 0; i < countEvents; i++)
            {
                /* Fill image */
                HtmlNode imgNode = eventsImages[i];
                if (imgNode != null)
                {
                    if (events.Count > i) {
                        events[i].ImgSrc = imgNode.HasAttributes ? imgNode.Attributes["src"].Value : string.Empty;
                        events[i].ImgSrc = "http://www.librairiepax.be" + events[i].ImgSrc;
                    }
                }
            }
        }

        private static void FillEventsDescription(HtmlDocument doc, List<EventItem> events)
        {
            HtmlNodeCollection eventsDescr = doc.DocumentNode.SelectNodes("//td[@class='EventsCorpus2']//div[@class='event_item']//p");

            var countEvents = eventsDescr.Count;

            for (int i = 0; i < countEvents; i++)
            {
                /* Fill image */
                HtmlNode descrNode = eventsDescr[i];
                if (descrNode != null)
                {
                    if (events.Count > i)
                    {
                        events[i].Description = descrNode.InnerText;
                        events[i].Description = events[i].Description.Replace("\r\n", "<br>");
                        events[i].ShortDescription = events[i].Description.Replace("\r\n", " ").Substring(0, 100);
                        events[i].ShortDescription += " [...]";
                    }
                }
            }
        }

        #endregion

    }
}
