using HtmlAgilityPack;
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
            //HtmlDocument doc = new HtmlDocument();
            //doc.Load("http://www.librairiepax.be/");

            string Url = "http://www.librairiepax.be/";
            HtmlWeb web = new HtmlWeb();
            HtmlDocument doc = web.Load(Url);

            var findclasses = doc.DocumentNode
                .Descendants("tr")
                .Where(d =>
                d.Attributes.Contains("class")
                &&
                d.Attributes["class"].Value.Contains("blocCoupCoeurN1")
                );

            var test = findclasses.ToList();

            return new List<Books>();
        }
        

        #endregion
    }
}
