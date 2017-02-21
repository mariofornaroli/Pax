﻿using Entities;
using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PaxComputation
{
    public static class BooksComputation
    {
        private const string PAX_WEBSITE = "http://www.librairiepax.be/";
        private const string PAX_WEBSITE_BEST_SELLERS = "http://www.librairiepax.be/topfrance.php";
        private const string HTML_COERU_TD_CLASS = "CoeurCorpus";
        private const int MAX_COERU_BLOCS_NUM = 10;
        private const string HTML_COERU_TITLE_CLASS = "CoeurTitre";
        
        #region ComputeBookDetails Methods

        /// <summary>
        /// Get Heart book details
        /// </summary>
        /// <returns>Book details</returns>
        public static BookDetailsItem ComputeBookDetails(string completeHref)
        {
            return _ComputeBookDetails(completeHref);
        }

        private static BookDetailsItem _ComputeBookDetails(string completeHref)
        {
            HtmlDocument doc = new HtmlDocument();
            HttpDownloader downloader = new HttpDownloader(completeHref, null, null);
            doc.LoadHtml(downloader.GetPage());

            return GetBookDetailsObj(doc);
        }

        private static BookDetailsItem GetBookDetailsObj(HtmlDocument doc)
        {
            var bookDetails = new BookDetailsItem();

            /* Get coeurTdDoc object */
            var detailTableDoc = GetDetailTableObj(doc);

            /* Fill properties */
            FillTitle(detailTableDoc, bookDetails);
            FillImgSrc(detailTableDoc, bookDetails);
            FillAuthor(detailTableDoc, bookDetails);
            FillEditeur(detailTableDoc, bookDetails);
            FillDateParution(detailTableDoc, bookDetails);
            FillGenre(detailTableDoc, bookDetails);
            FillTraduction(detailTableDoc, bookDetails);

            /* mot_du_libraire */
            bookDetails.AdditionalDescriptionItems = new List<DescriptionItem>();
            FillAdditionalInfoSection(doc, bookDetails);

            /* global info */
            bookDetails.GlobalInfoDescriptionItems = new List<DescriptionItem>();
            FillEditorWord(doc, bookDetails);
            FillBiography(doc, bookDetails);

            return bookDetails;
        }

        private static void FillEditorWord(HtmlDocument doc, BookDetailsItem bookDetails)
        {
            var addItem = new DescriptionItem();
            HtmlNode globbalInfoNode = doc.DocumentNode
                .SelectSingleNode("//div[@class='bloc_presa']//p");
            if (globbalInfoNode != null)
            {
                addItem.Title = "Le mot de l'éditeur";
                addItem.Content = globbalInfoNode.InnerText;
            }
            bookDetails.GlobalInfoDescriptionItems.Add(addItem);
        }

        private static void FillBiography(HtmlDocument doc, BookDetailsItem bookDetails)
        {
            var addItem = new DescriptionItem();
            HtmlNode globbalInfoNode = doc.DocumentNode
                .SelectSingleNode("//div[@class='bloc_biographie']//p");
            if (globbalInfoNode != null)
            {
                addItem.Title = "Biographie";
                addItem.Content = globbalInfoNode.InnerText;
            }
            bookDetails.GlobalInfoDescriptionItems.Add(addItem);
        }

        private static void FillAdditionalInfoSection(HtmlDocument doc, BookDetailsItem bookDetails)
        {
            var addItem = new DescriptionItem();
            HtmlNode titleNode = doc.DocumentNode
                .SelectSingleNode("//div[@class='bloc_mot_du_libraire']//h2");
            if (titleNode != null)
            {
                addItem.Title = titleNode.InnerText;
            }
            HtmlNode contentNode = doc.DocumentNode
                .SelectSingleNode("//div[@class='bloc_mot_du_libraire']//div[@class='contenu_mdl']");
            if (contentNode != null)
            {
                addItem.Content = contentNode.InnerText;
            }
            bookDetails.AdditionalDescriptionItems.Add(addItem);
        }

        private static HtmlDocument GetDetailTableObj(HtmlDocument doc)
        {
            var className = "tab_detaillivre";
            var detailTable = doc.DocumentNode
                .Descendants("table")
                .Where(d =>
                d.Attributes.Contains("class")
                &&
                d.Attributes["class"].Value.Contains(className)
                ).Select(x => AgilityTool.LoadFromString(x.InnerHtml));
            return detailTable.FirstOrDefault();
        }

        /* Title */
        private static void FillTitle(HtmlDocument tableDoc, BookDetailsItem bookDetails)
        {
            HtmlNode titleNode = tableDoc.DocumentNode
                .SelectSingleNode("//td[@class='tab_detaillivre_metabook']//span[@class='titre']");
            if (titleNode != null) {
                bookDetails.Title = titleNode.InnerText;
            }
        }

        /* ImgSrc */
        private static void FillImgSrc(HtmlDocument tableDoc, BookDetailsItem bookDetails)
        {
            HtmlNode imgNode = tableDoc.DocumentNode
                .SelectSingleNode("//td[@class='visu']//img");
            if (imgNode != null)
            {
                bookDetails.ImgSrc = imgNode.HasAttributes ? imgNode.Attributes["src"].Value : string.Empty;
            }
        }

        /* auteur */
        private static void FillAuthor(HtmlDocument tableDoc, BookDetailsItem bookDetails)
        {
            HtmlNode autheurNode = tableDoc.DocumentNode.SelectSingleNode("//td[@class='tab_detaillivre_metabook']//div[@class='cont-metabook']//ul//li[@class='auteur']//a");
            if (autheurNode != null)
            {
                bookDetails.AuthorHref = autheurNode.HasAttributes ? autheurNode.Attributes["href"].Value : string.Empty;
                bookDetails.Author = autheurNode.InnerText;
            }
        }

        /* editeur_collection */
        private static void FillEditeur(HtmlDocument tableDoc, BookDetailsItem bookDetails)
        {
            HtmlNode editeurNode = tableDoc.DocumentNode.SelectSingleNode("//td[@class='tab_detaillivre_metabook']//div[@class='cont-metabook']//ul//li[@class='editeur_collection']//span[@class='editeur']");
            if (editeurNode != null)
            {
                bookDetails.Editor = editeurNode.InnerText;
            }
        }

        /* date_parution */
        private static void FillDateParution(HtmlDocument tableDoc, BookDetailsItem bookDetails)
        {
            HtmlNode dateNode = tableDoc.DocumentNode.SelectSingleNode("//td[@class='tab_detaillivre_metabook']//div[@class='cont-metabook']//ul//li[@class='date_parution']");
            if (dateNode != null)
            {
                bookDetails.PublishedDate = dateNode.InnerText;
            }
        }

        /* genre */
        private static void FillGenre(HtmlDocument tableDoc, BookDetailsItem bookDetails)
        {
            HtmlNode genreNode = tableDoc.DocumentNode.SelectSingleNode("//td[@class='tab_detaillivre_metabook']//div[@class='cont-metabook']//ul//li[@class='genre']");
            if (genreNode != null)
            {
                bookDetails.Type = genreNode.InnerText;
            }
        }

        /* traduction */
        private static void FillTraduction(HtmlDocument tableDoc, BookDetailsItem bookDetails)
        {
            HtmlNode traductionNode = tableDoc.DocumentNode.SelectSingleNode("//td[@class='tab_detaillivre_metabook']//div[@class='cont-metabook']//ul//li[@class='traduction']");
            if (traductionNode != null)
            {
                bookDetails.TraductionInfo = traductionNode.InnerText;
            }
        }



        #endregion


        #region ComputeHeartBooks Methods

        /// <summary>
        /// Get Heart books
        /// </summary>
        /// <returns>List of heart books</returns>
        public static HeartBooksModel ComputeHeartBooks()
        {
            return _ComputeHeartBooks();
        }

        private static HeartBooksModel _ComputeHeartBooks()
        {
            HtmlDocument doc = new HtmlDocument();
            HttpDownloader downloader = new HttpDownloader(PAX_WEBSITE, null, null);
            doc.LoadHtml(downloader.GetPage());

            return GetBookObjList(doc);
        }

        public static HeartBooksModel GetBookObjList(HtmlDocument doc)
        {
            var retData = new HeartBooksModel();

            /* Get coeurTdDoc object */
            var coeurTdDoc = GetCoeurTdObj(doc);

            /* Get coeurTdDoc Block list */
            var groupBlocksList = GetGroupBlocksList(coeurTdDoc);

            /* Get coeurTdDoc Img list and Description list */
            var bookList = new List<BookItem>();
            GetGeneralBookListItem(groupBlocksList, ref bookList);

            /* Compute here the book of the month */
            var monthBook = GetMonthBookTdObj(doc);

            /* Assign return variables */
            retData.HeartBooks = bookList;
            retData.MonthBook = monthBook;

            return retData;
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

        public static BookItem GetMonthBookTdObj(HtmlDocument doc)
        {
            var retMonthBook = new BookItem();
            HtmlNode monthBookNode = doc.DocumentNode
                .SelectSingleNode("//table[@class='blocLibre']//td[@class='LibreCorpus']");

            /* Fill title */
            HtmlNode titleNode = monthBookNode.SelectSingleNode("//span[@class='LibreTitre']");
            if (titleNode != null)
            {
                retMonthBook.Title = titleNode.InnerText;
            }
            /* Fill img */
            HtmlNode imgNode = monthBookNode.SelectSingleNode("//table[@class='blocLibre']//td[@class='LibreCorpus']//div//a//img");
            if (imgNode != null)
            {
                retMonthBook.ImgSrc = imgNode.HasAttributes ? imgNode.Attributes["src"].Value : string.Empty;
            }
            /* Fill href */
            HtmlNode hrefNode = monthBookNode.SelectSingleNode("//table[@class='blocLibre']//td[@class='LibreCorpus']//div//a");
            if (hrefNode != null)
            {
                retMonthBook.Href = hrefNode.HasAttributes ? hrefNode.Attributes["href"].Value : string.Empty;
            }
            /**/
            retMonthBook.CompleteHref = retMonthBook.Href;
            retMonthBook.DateComputation = DateTime.Now;

            /* Return computed book of the month */
            return retMonthBook;
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
               var test =  tr.Encoding;
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
                        bookList.Add(new BookItem { Href = href, CompleteHref = PAX_WEBSITE + href, ImgSrc = imgSrc, DateComputation = DateTime.Now });
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
                        description = descriptionItem.InnerText;

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




        #region ComputeBestSellers Methods

        private const string PAX_EVENTS_WEBSITE = "http://www.librairiepax.be/events.php?blid=5808";

        /// <summary>
        /// Get Heart books
        /// </summary>
        /// <returns>List of heart books</returns>
        public static BestSellersModel ComputeBestSellers()
        {
            return _ComputeBestSellers();
        }

        private static BestSellersModel _ComputeBestSellers()
        {
            HtmlDocument doc = new HtmlDocument();
            HttpDownloader downloader = new HttpDownloader(PAX_WEBSITE_BEST_SELLERS, null, null);
            doc.LoadHtml(downloader.GetPage());

            return GetBestSellersObjList(doc);
        }

        public static BestSellersModel GetBestSellersObjList(HtmlDocument doc)
        {
            var retData = new BestSellersModel() { BestSellers = new List<BookItem>() };

            /* Fill Best Sellers */
            FillBestSellers(doc, retData.BestSellers);            

            return retData;
        }

        private static void FillBestSellers(HtmlDocument doc, List<BookItem> bestSellers)
        {
            HtmlNodeCollection bsRows = doc.DocumentNode.SelectNodes("//table[@class='tab_listlivre']//tr");

            var countEvents = bsRows.Count;

            for (int i = 0; i < countEvents; i++)
            {
                var bookToAdd = fillBestSellerItem(bsRows[i]);

                
                /* Add event to list */
                bestSellers.Add(bookToAdd);
            }
        }

        private static BookItem fillBestSellerItem(HtmlNode bookNode)
        {
            var retBook = new BookItem();
            if (bookNode != null)
            {
                /* Fill title and href */
                var titleNode = bookNode.SelectSingleNode("//td[@class='metabook']//ul[@class='listeliv_metabook']//li[@class='titre_auteur']//span[@class='titre']//a");
                if (titleNode != null)
                {
                    retBook.Title = titleNode.InnerText;
                    retBook.Href = titleNode.HasAttributes ? titleNode.Attributes["href"].Value : string.Empty;
                    retBook.Href = PAX_WEBSITE + retBook.Href;
                    retBook.CompleteHref = retBook.Href;
                }
                /* Fill Autheur */
                var autheurNode = bookNode.SelectSingleNode("//td[@class='metabook']//ul[@class='listeliv_metabook']//li[@class='titre_auteur']//span[@class='auteur']//a");
                if (autheurNode != null)
                {
                    retBook.Author = autheurNode.InnerText;
                    retBook.AuthorHref = autheurNode.HasAttributes ? autheurNode.Attributes["href"].Value : string.Empty;
                    retBook.AuthorHref = PAX_WEBSITE + retBook.Href;
                }
                /* Fill img */
                var imgNode = bookNode.SelectSingleNode("//td[@class='visu']//img");
                if (imgNode != null)
                {
                    retBook.ImgSrc = imgNode.HasAttributes ? imgNode.Attributes["src"].Value : string.Empty;
                }
                /* Fill editor */
                var editorNode = bookNode.SelectSingleNode("//td[@class='metabook']//ul[@class='listeliv_metabook']//li[@class='editeur']");
                if (editorNode != null)
                {
                    retBook.Editor = editorNode.InnerText;
                }
                /* Fill published date */
                var pubDateNode = bookNode.SelectSingleNode("//td[@class='metabook']//ul[@class='listeliv_metabook']//li[@class='date_parution']");
                if (pubDateNode != null)
                {
                    retBook.PublishedDate = pubDateNode.InnerText;
                }
                /* Fill price */
                var priceNode = bookNode.SelectSingleNode("//td[@class='metabook']//ul[@class='listeliv_metabook']//li[@class='prix']//span[@class='prix_indicatif']");
                if (priceNode != null)
                {
                    retBook.Price = priceNode.InnerText;
                }
            }

            retBook.DateComputation = DateTime.Now;
            return retBook;
        }


        #endregion
    }
}
