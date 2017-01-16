﻿using Entities;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaxComputation
{
    public static class BooksComputation
    {
        private const string PAX_WEBSITE = "http://www.librairiepax.be/";
        private const string HTML_COERU_TD_CLASS = "CoeurCorpus";
        private const int MAX_COERU_BLOCS_NUM = 10;
        private const string HTML_COERU_TITLE_CLASS = "CoeurTitre";

        /// <summary>
        /// Get Heart books
        /// </summary>
        /// <returns>List of heart books</returns>
        public static List<BookItem> ComputeHeartBooks()
        {
            return _ComputeHeartBooks();
        }

        #region Private Methods

        private static List<BookItem> _ComputeHeartBooks()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(PAX_WEBSITE);

            return GetBookObjList(doc);
        }

        public static List<BookItem> GetBookObjList(HtmlDocument doc)
        {
            /* Get coeurTdDoc object */
            var coeurTdDoc = GetCoeurTdObj(doc);

            /* Get coeurTdDoc Block list */
            var groupBlocksList = GetGroupBlocksList(coeurTdDoc);

            /* Get coeurTdDoc Img list and Description list */
            var bookList = new List<BookItem>();
            GetGeneralBookListItem(groupBlocksList, ref bookList);

            return bookList;
        }

        public static HtmlDocument GetCoeurTdObj(HtmlDocument doc)
        {
            var coeurTdObj = doc.DocumentNode
                .Descendants("td")
                .Where(d =>
                d.Attributes.Contains("class")
                &&
                d.Attributes["class"].Value.Contains(HTML_COERU_TD_CLASS)
                ).FirstOrDefault().InnerHtml;
            return AgilityTool.LoadFromString(coeurTdObj);
        }

        private static List<HtmlDocument> GetGroupBlocksList(HtmlDocument coeurTdDoc)
        {
            var blockList = new List<HtmlDocument>();
            string className = string.Empty;
            for (int i = 0; i < MAX_COERU_BLOCS_NUM; i++)
            {
                className = "blocCoupCoeurN" + i.ToString();
                var tmpBlocksList = coeurTdDoc.DocumentNode
                    .Descendants("tr")
                    .Where(d =>
                    d.Attributes.Contains("class")
                    &&
                    d.Attributes["class"].Value.Contains(className)
                    ).Select(x => AgilityTool.LoadFromString(x.InnerHtml));
                blockList.AddRange(tmpBlocksList.ToList());
            }

            return blockList;
        }


        private static void GetGeneralBookListItem(List<HtmlDocument> groupBlocksList, ref List<BookItem> bookList)
        {
            foreach (var tr in groupBlocksList)
            {
                var tdBlocks = tr.DocumentNode
                    .Descendants("td");
                foreach (var td in tdBlocks)
                {
                    FillImgOrDescriptionItem(td, ref bookList);
                }
            }
        }

        private static void FillImgOrDescriptionItem(HtmlNode td, ref List<BookItem> bookList)
        {
            var href = td
                    .Descendants("a")
                    .Where(d =>
                    d.Attributes.Contains("href")).Select(d => d.Attributes["href"].Value).FirstOrDefault();
            if (href == null || string.IsNullOrEmpty(href))
            {
                return;
            }

            var ImgNode = td
                    .Descendants("a").FirstOrDefault();
            if (ImgNode != null)
            {
                /* CHECK FOR IMAGE */
                var imgSrc = ImgNode
                    .Descendants("img")
                    .Where(d =>
                    d.Attributes.Contains("src")).Select(d => d.Attributes["src"].Value).FirstOrDefault();
                if (imgSrc != null && !string.IsNullOrEmpty(imgSrc))
                {
                    /* If image does not exist yet, then add it */
                    int indexImg = bookList.FindIndex(x => x.Href == href);
                    if (indexImg < 0)
                    {
                        bookList.Add(new BookItem { Href = href, CompleteHref = PAX_WEBSITE + href, ImgSrc = imgSrc });
                    }
                    else
                    {
                        bookList[indexImg].Href = href;
                        bookList[indexImg].CompleteHref = PAX_WEBSITE + href;
                        bookList[indexImg].ImgSrc = href;
                    }
                    return;
                }

                /* CHECK FOR TITLE AND DESCRIPTION */
                var title = string.Empty;
                var description = string.Empty;
                var descrItem = ImgNode
                    .FirstChild;
                if (descrItem != null &&
                    descrItem.Attributes.Contains("class") &&
                    descrItem.Attributes["class"].Value == "conseilib-ibc")
                {
                    /* Title */
                    var titleItem = descrItem
                    .Descendants("span")
                    .Where(d =>
                    d.Attributes.Contains("class") &&
                    d.Attributes["class"].Value == "CoeurTitre").FirstOrDefault();
                    if (titleItem != null)
                        title = titleItem.InnerHtml;
                    /* Description */
                    var descriptionItem = descrItem
                    .Descendants("p").FirstOrDefault();
                    if (descriptionItem != null)
                        description = descriptionItem.InnerHtml;

                    /* If description does not exist yet, then add it */
                    int indexDescr = bookList.FindIndex(x => x.Href == href);
                    if (indexDescr < 0)
                    {
                        bookList.Add(new BookItem { Title = title, ShortDescription = description });
                    }
                    else
                    {
                        bookList[indexDescr].Title = title;
                        bookList[indexDescr].ShortDescription = description;
                    }
                }
            }

        }

        #endregion
    }
}
