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

        /// <summary>
        /// Get Heart book details
        /// </summary>
        /// <returns>Book details</returns>
        public static BookDetailsItem ComputeBookDetails(string completeHref)
        {
            return _ComputeBookDetails(completeHref);
        }


        #region Private ComputeBookDetails Methods

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




        #region Private ComputeHeartBooks Methods

        private static List<BookItem> _ComputeHeartBooks()
        {
            HtmlDocument doc = new HtmlDocument();
            HttpDownloader downloader = new HttpDownloader(PAX_WEBSITE, null, null);
            doc.LoadHtml(downloader.GetPage());

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
                
    }
}
