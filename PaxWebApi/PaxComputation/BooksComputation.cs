using HtmlAgilityPack;
using PaxComputation.Models;
using PaxDal;
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

        /// <summary>
        /// Get Heart books
        /// </summary>
        /// <returns>List of heart books</returns>
        public static List<Books> ComputeHeartBooks()
        {
            return _ComputeHeartBooks();
        }

        #region Private Methods

        private static List<Books> _ComputeHeartBooks()
        {
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(PAX_WEBSITE);

            return GetBookObjList(doc);
        }

        public static List<Books> GetBookObjList(HtmlDocument doc)
        {
            /* Get coeurTdDoc object */
            var coeurTdDoc = GetCoeurTdObj(doc);

            /* Get coeurTdDoc Block list */
            var groupBlocksList = GetGroupBlocksList(coeurTdDoc);

            /* Get coeurTdDoc Img list and Description list */
            var imgList = new List<ImgItem>();
            var descrList = new List<DescrItem>();
            GetGeneralBookListItem(groupBlocksList, ref imgList, ref descrList);

            return new List<Books>();
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


        private static void GetGeneralBookListItem(List<HtmlDocument> groupBlocksList, ref List<ImgItem> imgList, ref List<DescrItem> descrList)
        {
            foreach (var block in groupBlocksList)
            {

            }
        }

        #endregion
    }
}
