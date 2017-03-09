using Entities;
using HtmlAgilityPack;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace PaxComputation
{
    public static class BooksComputation
    {
        private const string PAX_WEBSITE = "http://www.librairiepax.be/";
        private const string PAX_WEBSITE_BEST_SELLERS = "http://www.librairiepax.be/topfrance.php";
        private const string PAX_WEBSITE_BOOKSELLER_WORDS = "http://www.librairiepax.be/selec-10827-mots-des-libraires/";
        private const string HTML_COERU_TD_CLASS = "CoeurCorpus";
        private const int MAX_COERU_BLOCS_NUM = 10;
        private const string HTML_COERU_TITLE_CLASS = "CoeurTitre";


        #region properties

        private static FileComputation fileComputation = new FileComputation();

        #endregion


        #region Get data (from file for example)

        /// <summary>
        /// Get Seller Words books
        /// </summary>
        /// <returns>List of Seller Words books</returns>
        public static BooksListModel GetSellerWords()
        {
            /* Get HeartBooks from file, deserialize JSON result string into model and return the model */
            var resultJsonStringFormat = fileComputation.getFile("", "", "sellerWordsBooks.txt");
            /* Test */
            BooksListModel resDeserialized = JsonConvert.DeserializeObject<BooksListModel>(resultJsonStringFormat);
            return resDeserialized;
        }

        /// <summary>
        /// Get Details Seller Words books from file, deserialize JSON result string into model and return the model
        /// </summary>
        /// <returns>List of heart books</returns>
        public static DetailsBooksModel GetSellerWordsBooksDetails()
        {
            /* Get HeartBooks from file, deserialize JSON result string into model and return the model */
            var resultJsonStringFormat = fileComputation.getFile("", "", "sellerWordsBooksDetails.txt");
            /* Test */
            DetailsBooksModel resDeserialized = JsonConvert.DeserializeObject<DetailsBooksModel>(resultJsonStringFormat);
            return resDeserialized;
        }

        /// <summary>
        /// Get Heart books from file, deserialize JSON result string into model and return the model
        /// </summary>
        /// <returns>List of heart books</returns>
        public static BooksListModel GetAdvicedBooks()
        {
            /* Get HeartBooks from file, deserialize JSON result string into model and return the model */
            var resultJsonStringFormat = fileComputation.getFile("", "", "advicedBooks.txt");
            /* Test */
            BooksListModel resDeserialized = JsonConvert.DeserializeObject<BooksListModel>(resultJsonStringFormat);
            return resDeserialized;
        }

        /// <summary>
        /// Get adviced books details from file, deserialize JSON result string into model and return the model
        /// </summary>
        /// <returns>List of adviced books details</returns>
        public static DetailsBooksModel GetAdvicedBooksDetails()
        {
            /* Get adviced books details from file, deserialize JSON result string into model and return the model */
            var resultJsonStringFormat = fileComputation.getFile("", "", "advicedBooksDetails.txt");
            /* Test */
            DetailsBooksModel resDeserialized = JsonConvert.DeserializeObject<DetailsBooksModel>(resultJsonStringFormat);
            return resDeserialized;
        }


        /// <summary>
        /// Get Best Sellers books
        /// </summary>
        /// <returns>List of Best Sellers books</returns>
        public static BooksListModel GetBestSellers()
        {
            //return _ComputeBestSellers();

            /* Get HeartBooks from file, deserialize JSON result string into model and return the model */
            var resultJsonStringFormat = fileComputation.getFile("", "", "bestSellerBooks.txt");
            /* Test */
            BooksListModel resDeserialized = JsonConvert.DeserializeObject<BooksListModel>(resultJsonStringFormat);
            return resDeserialized;
        }

        /// <summary>
        /// Get Best Sellers books details from file, deserialize JSON result string into model and return the model
        /// </summary>
        /// <returns>List of Best Sellers books details</returns>
        public static DetailsBooksModel GetBestSellersBooksDetails()
        {
            /* Get Best Sellers details from file, deserialize JSON result string into model and return the model */
            var resultJsonStringFormat = fileComputation.getFile("", "", "bestSellerBooksDetails.txt");
            /* Test */
            DetailsBooksModel resDeserialized = JsonConvert.DeserializeObject<DetailsBooksModel>(resultJsonStringFormat);
            return resDeserialized;
        }

        /// <summary>
        /// Get book details
        /// </summary>
        /// <returns>Book details</returns>
        public static BookDetailsItem GetBookDetails(string completeHref)
        {
            var retDetail = _ComputeBookDetails(completeHref);
            retDetail.CompleteHref = completeHref;
            return retDetail;
        }

        #endregion


        #region Compute, save to file and get from file

        /// <summary>
        /// Compute pax books and details and insert information into files
        /// </summary>
        /// <returns>List of Seller Words books</returns>
        public static BaseResultModel ComputePaxToFile()
        {
            var ret = new BaseResultModel();

            /* SELLER WORDS BOOKS and SELLER WORDS BOOKS NOTIFICATION */
            var sbRes = ComputeSellerBooksToFileAndNotification();

            /* ADVICED BOOKS */
            var adbRes = ComputeAdvicedBooksToFile();

            /* BEST SELLER BOOKS */
            var bsRes = ComputeBestSellerBooksToFile();

            /* RETURN */
            if (sbRes.OperationResult && sbRes.OperationResult)
            {
                ret.OperationResult = true;
            }
            else
            {
                ret.OperationResult = false;
            }
            return ret;
        }

        /// <summary>
        /// Compute seller books and details and insert information into files
        /// </summary>
        /// <returns>BaseResultModel with result information</returns>
        public static BaseResultModel ComputeSellerBooksToFileAndNotification()
        {
            var ret = new BaseResultModel();
            string resultJsonStringified;

            BooksListModel hn = ComputeSellerWordsBooksNotifications();

            /* Compute DETAILS FOR HEART BOOKS and save into "sellerWordsBooksDetails.txt" file */
            DetailsBooksModel detList = new DetailsBooksModel
            {
                DetailsBooks = new List<BookDetailsItem>(),
                BookType = BookTypeEnum.HEART_BOOK
            };
            var bookDetailsList = new List<BookDetailsItem>();
            foreach (var bookItem in hn.BooksList)
            {
                detList.DetailsBooks.Add(GetBookDetails(bookItem.CompleteHref));
            }
            resultJsonStringified = JsonConvert.SerializeObject(detList);
            fileComputation.writeFile("", "", "sellerWordsBooksDetails.txt", resultJsonStringified);

            ret.OperationResult = true;
            return ret;
        }

        /// <summary>
        /// Compute adviced books and details and insert information into files
        /// </summary>
        /// <returns>BaseResultModel with result information</returns>
        private static BaseResultModel ComputeAdvicedBooksToFile()
        {
            var ret = new BaseResultModel();
            string resultJsonStringified;

            /* Compute new books and save it to file */
            BooksListModel hn = _ComputeHeartBooks();
            resultJsonStringified = JsonConvert.SerializeObject(hn);
            fileComputation.writeFile("", "", "advicedBooks.txt", resultJsonStringified);

            /* Compute DETAILS FOR HEART BOOKS and save into "advicedBooks.txt" file */
            DetailsBooksModel detList = new DetailsBooksModel
            {
                DetailsBooks = new List<BookDetailsItem>(),
                BookType = BookTypeEnum.HEART_BOOK
            };
            var bookDetailsList = new List<BookDetailsItem>();
            foreach (var bookItem in hn.BooksList)
            {
                detList.DetailsBooks.Add(GetBookDetails(bookItem.CompleteHref));
            }
            resultJsonStringified = JsonConvert.SerializeObject(detList);
            fileComputation.writeFile("", "", "advicedBooksDetails.txt", resultJsonStringified);

            ret.OperationResult = true;
            return ret;
        }

        /// <summary>
        /// Compute best seller books and details and insert information into files
        /// </summary>
        /// <returns>BaseResultModel with result information</returns>
        private static BaseResultModel ComputeBestSellerBooksToFile()
        {
            var ret = new BaseResultModel();
            string resultJsonStringified;

            /* Compute new books and save it to file */
            BooksListModel hn = _ComputeBestSellers();
            resultJsonStringified = JsonConvert.SerializeObject(hn);
            fileComputation.writeFile("", "", "bestSellerBooks.txt", resultJsonStringified);


            /* Compute DETAILS FOR HEART BOOKS and save into "bestSellerBooks.txt" file */
            DetailsBooksModel detList = new DetailsBooksModel
            {
                DetailsBooks = new List<BookDetailsItem>(),
                BookType = BookTypeEnum.HEART_BOOK
            };
            var bookDetailsList = new List<BookDetailsItem>();
            foreach (var bookItem in hn.BooksList)
            {
                detList.DetailsBooks.Add(GetBookDetails(bookItem.CompleteHref));
            }
            resultJsonStringified = JsonConvert.SerializeObject(detList);
            fileComputation.writeFile("", "", "bestSellerBooksDetails.txt", resultJsonStringified);

            ret.OperationResult = true;
            return ret;
        }


        /// <summary>
        /// Get notifications if present, save information into files and notify users
        /// </summary>
        /// <returns>List of heart books</returns>
        public static BooksListModel ComputeSellerWordsBooksNotifications()
        {
            var ret = new BaseResultModel();
            string jsonStringified;
            BooksListModel newEntriesHeartBooks = new BooksListModel { BooksList = new List<BookItem>() };

            /* Compute new heart books */
            BooksListModel newSellerWordsBooks = _ComputeSellerWords();

            /* Compute old heart books */
            var resultJsonStringFormat = fileComputation.getFile("", "", "sellerWordsBooks.txt");
            BooksListModel oldHeartBooks = JsonConvert.DeserializeObject<BooksListModel>(resultJsonStringFormat);

            /* ------  seller words books NOTIFICATION ----- */
            if (newSellerWordsBooks != null && oldHeartBooks != null)
            {
                // First compute the new entroes, if any
                var newEntries = newSellerWordsBooks.BooksList.Where(newB => oldHeartBooks.BooksList.Count(oldB => oldB.CompleteHref == newB.CompleteHref) == 0).ToList();
                // Are there any notifications? 
                if (newEntries != null && newEntries.Count > 0)
                {
                    /* Save newentries into model */
                    newEntriesHeartBooks.BooksList = newEntries;

                    /* Execute device notifications */
                    executeHeartNotifications();

                    /* Then, modify the new read heart books adding the info isNewAdded = true/false 
                     * and save new version into file
                     */
                    foreach (var newItem in newEntries)
                    {
                        foreach (var book in newSellerWordsBooks.BooksList)
                        {
                            if (book.CompleteHref == newItem.CompleteHref)
                            {
                                book.IsNewAdded = true;
                            };
                        }
                    }

                    /* Then, save last notifications file ("lastNotifications.txt") */
                    jsonStringified = JsonConvert.SerializeObject(newEntriesHeartBooks);
                    fileComputation.writeFile("", "", "lastSellerWordsNotifications.txt", jsonStringified);

                    /* Save new seller words books */
                    /* Order new sellers to have the new added books on top */
                    newSellerWordsBooks.BooksList.OrderByDescending(b => b.IsNewAdded);
                    jsonStringified = JsonConvert.SerializeObject(newSellerWordsBooks);
                    fileComputation.writeFile("", "", "sellerWordsBooks.txt", jsonStringified);
                }
            }

            return newSellerWordsBooks;
        }

        #endregion


        #region Notifications

        private static void executeHeartNotifications(string msgBody = "")
        {
            if (string.IsNullOrEmpty(msgBody))
            {
                msgBody = "Des livres récents ont été conseillés";
            }

            NotifComputation notiffComputation = new NotifComputation(new HttpConfiguration());
            object defaultsNotif = new
            {
                body = msgBody,
                title = "Librairie Pax",
                icon = "fcm_push_icon",
                sound = "default",
                click_action = "FCM_PLUGIN_ACTIVITY"
            };

            var ret = notiffComputation.executeNotif(defaultsNotif);

        }

        #endregion


        #region ComputeHeartBooks Methods

        private static BooksListModel _ComputeHeartBooks()
        {
            HtmlDocument doc = new HtmlDocument();
            HttpDownloader downloader = new HttpDownloader(PAX_WEBSITE, null, null);
            doc.LoadHtml(downloader.GetPage());

            return GetBookObjList(doc);
        }

        public static BooksListModel GetBookObjList(HtmlDocument doc)
        {
            var retData = new BooksListModel();

            /* Get coeurTdDoc object */
            var coeurTdDoc = GetCoeurTdObj(doc);

            /* Get coeurTdDoc Block list */
            var groupBlocksList = GetGroupBlocksList(coeurTdDoc);

            /* Get coeurTdDoc Img list and Description list */
            var bookList = new List<BookItem>();
            GetGeneralBookListItem(groupBlocksList, ref bookList);

            /* Compute here the book of the month */
            var monthBook = GetMonthBookTdObj();

            /* Assign return variables */
            retData.BooksList = bookList;
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

        public static BookItem GetMonthBookTdObj()
        {
            HtmlDocument doc = new HtmlDocument();
            HttpDownloader downloader = new HttpDownloader(PAX_WEBSITE, null, null);
            doc.LoadHtml(downloader.GetPage());

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
                var test = tr.Encoding;
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


        #region ComputeBookDetails Methods

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
            if (titleNode != null)
            {
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


        #region ComputeBestSellers Methods

        private static BooksListModel _ComputeBestSellers()
        {
            HtmlDocument doc = new HtmlDocument();
            HttpDownloader downloader = new HttpDownloader(PAX_WEBSITE_BEST_SELLERS, null, null);
            doc.LoadHtml(downloader.GetPage());

            return GetBestSellersObjList(doc);
        }

        public static BooksListModel GetBestSellersObjList(HtmlDocument doc)
        {
            var retData = new BooksListModel() { BooksList = new List<BookItem>() };

            /* Fill Best Sellers */
            FillBestSellers(doc, retData.BooksList);

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
            var bookNodeDocument = AgilityTool.LoadFromString(bookNode.InnerHtml);

            var retBook = new BookItem();
            if (bookNodeDocument != null)
            {
                /* Fill title and href */
                var titleNode = bookNodeDocument.DocumentNode.SelectSingleNode("//td[@class='metabook']//ul[@class='listeliv_metabook']//li[@class='titre_auteur']//span[@class='titre']//a");
                if (titleNode != null)
                {
                    retBook.Title = titleNode.InnerText;
                    retBook.Href = titleNode.HasAttributes ? titleNode.Attributes["href"].Value : string.Empty;
                    retBook.Href = PAX_WEBSITE + retBook.Href;
                    retBook.CompleteHref = retBook.Href;
                }
                /* Fill Autheur */
                var autheurNode = bookNodeDocument.DocumentNode.SelectSingleNode("//td[@class='metabook']//ul[@class='listeliv_metabook']//li[@class='titre_auteur']//span[@class='auteur']//a");
                if (autheurNode != null)
                {
                    retBook.Author = autheurNode.InnerText;
                    retBook.AuthorHref = autheurNode.HasAttributes ? autheurNode.Attributes["href"].Value : string.Empty;
                    retBook.AuthorHref = PAX_WEBSITE + retBook.Href;
                }
                /* Fill img */
                var imgNode = bookNodeDocument.DocumentNode.SelectSingleNode("//td[@class='visu']//img");
                if (imgNode != null)
                {
                    retBook.ImgSrc = imgNode.HasAttributes ? imgNode.Attributes["src"].Value : string.Empty;
                }
                /* Fill editor */
                var editorNode = bookNodeDocument.DocumentNode.SelectSingleNode("//td[@class='metabook']//ul[@class='listeliv_metabook']//li[@class='editeur']");
                if (editorNode != null)
                {
                    retBook.Editor = editorNode.InnerText;
                }
                /* Fill published date */
                var pubDateNode = bookNodeDocument.DocumentNode.SelectSingleNode("//td[@class='metabook']//ul[@class='listeliv_metabook']//li[@class='date_parution']");
                if (pubDateNode != null)
                {
                    retBook.PublishedDate = pubDateNode.InnerText;
                }
                /* Fill price */
                var priceNode = bookNodeDocument.DocumentNode.SelectSingleNode("//td[@class='metabook']//ul[@class='listeliv_metabook']//li[@class='prix']//span[@class='prix_indicatif']");
                if (priceNode != null)
                {
                    retBook.Price = priceNode.InnerText;
                }
            }

            retBook.DateComputation = DateTime.Now;
            return retBook;
        }


        #endregion


        #region Mots des libraires Methods

        public static BooksListModel _ComputeSellerWords()
        {
            HtmlDocument doc = new HtmlDocument();
            HttpDownloader downloader = new HttpDownloader(PAX_WEBSITE_BOOKSELLER_WORDS, null, null);
            doc.LoadHtml(downloader.GetPage());

            return GetBookSellerWordsObjList(doc);
        }

        public static BooksListModel GetBookSellerWordsObjList(HtmlDocument doc)
        {
            var retData = new BooksListModel() { BooksList = new List<BookItem>() };

            /* Fill Book Seller Words */
            FillBookSellerWords(doc, retData.BooksList);

            /* Compute here the book of the month */
            var monthBook = GetMonthBookTdObj();

            /* Assign return variables */
            retData.MonthBook = monthBook;

            return retData;
        }

        private static void FillBookSellerWords(HtmlDocument doc, List<BookItem> bookSellerWords)
        {
            HtmlNodeCollection bsRows = doc.DocumentNode.SelectNodes("//table[@id='liste_livres']//tr");

            var countEvents = bsRows.Count;

            for (int i = 0; i < countEvents; i++)
            {
                var bookToAdd = fillBookSellerWordsItem(bsRows[i]);


                /* Add event to list */
                bookSellerWords.Add(bookToAdd);
            }
        }

        private static BookItem fillBookSellerWordsItem(HtmlNode bookNode)
        {
            var bookNodeDocument = AgilityTool.LoadFromString(bookNode.InnerHtml);

            var retBook = new BookItem();
            if (bookNodeDocument != null)
            {
                /* Fill title and href */
                var titleNode = bookNodeDocument.DocumentNode.SelectSingleNode("//td[@class='colonne_infos']//ul[@class='listeliv_metabook']//li[@class='titre_commentaire']//span[@class='titre']//a");
                if (titleNode != null)
                {
                    retBook.Title = titleNode.InnerText;
                    retBook.Href = titleNode.HasAttributes ? titleNode.Attributes["href"].Value : string.Empty;
                    retBook.Href = PAX_WEBSITE + retBook.Href;
                    retBook.CompleteHref = retBook.Href;
                }
                /* Short Description */
                var shortDesriptionNode = bookNodeDocument.DocumentNode.SelectSingleNode("//td[@class='colonne_infos']//ul[@class='listeliv_metabook']//li[@class='motdulibraire']");
                if (shortDesriptionNode != null)
                {
                    retBook.ShortDescription = shortDesriptionNode.InnerText;
                }
                /* Fill Autheur */
                var autheurNode = bookNodeDocument.DocumentNode.SelectSingleNode("//td[@class='colonne_infos']//ul[@class='listeliv_metabook']//li[@class='auteurs']//a");
                if (autheurNode != null)
                {
                    retBook.Author = autheurNode.InnerText;
                    retBook.AuthorHref = autheurNode.HasAttributes ? autheurNode.Attributes["href"].Value : string.Empty;
                    retBook.AuthorHref = PAX_WEBSITE + retBook.Href;
                }
                /* Fill img */
                var imgNode = bookNodeDocument.DocumentNode.SelectSingleNode("//td[@class='colonne_image']//img");
                if (imgNode != null)
                {
                    retBook.ImgSrc = imgNode.HasAttributes ? imgNode.Attributes["src"].Value : string.Empty;
                }
                /* Fill editor */
                var editorNode = bookNodeDocument.DocumentNode.SelectSingleNode("//td[@class='colonne_infos']//ul[@class='listeliv_metabook']//li[@class='editeur']");
                if (editorNode != null)
                {
                    retBook.Editor = editorNode.InnerText;
                }
                /* Fill published date */
                var pubDateNode = bookNodeDocument.DocumentNode.SelectSingleNode("//td[@class='colonne_infos']//ul[@class='listeliv_metabook']//li[@class='editeur']//span[@class='date_parution']");
                if (pubDateNode != null)
                {
                    retBook.PublishedDate = pubDateNode.InnerText;
                }
                /* Fill price */
                var priceNode = bookNodeDocument.DocumentNode.SelectSingleNode("//td[@class='colonne_infos']//ul[@class='listeliv_metabook']//li[@class='prix']//span[@class='prix_indicatif']");
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
