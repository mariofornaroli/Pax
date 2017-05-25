using HtmlAgilityPack;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaxComputation
{
    public static class AgilityTool
    {
        public static HtmlDocument LoadFromString(string innerHtml)
        {
            HtmlDocument htmlDocument = new HtmlDocument();
            htmlDocument.LoadHtml(innerHtml);
            return htmlDocument;
        }

        /// <summary>
        /// generate a stream from a string
        /// </summary>
        /// <param name="s">input string</param>
        /// <returns>Stream</returns>
        public static Stream GenerateStreamFromString(string s)
        {
            MemoryStream stream = new MemoryStream();
            StreamWriter writer = new StreamWriter(stream);
            writer.Write(s);
            writer.Flush();
            stream.Position = 0;
            return stream;
        }

        #region get inner text

        public static string GetInnerText(HtmlDocument node, string xPath)
        {
            return GetInnerText(node.DocumentNode, xPath);
        }

        public static string GetOptionalInnerText(HtmlDocument node, string xPath)
        {
            try
            {
                return GetInnerText(node.DocumentNode, xPath);
            }
            catch { return string.Empty; }
        }

        public static string GetInnerText(HtmlNode node, string xPath)
        {
            string retValue = string.Empty;
            HtmlNode selectedNode = node.SelectSingleNode(xPath);
            if (selectedNode != null)
            {
                retValue = selectedNode.InnerText;
            }

            return retValue;
        }

        #endregion

        #region get attribute

        public static string GetAttributeValue(HtmlDocument node, string attr, string xPath)
        {
            return GetAttributeValue(node.DocumentNode, attr, xPath);
        }

        public static string GetAttributeValue(HtmlNode node, string attr, string xPath)
        {
            string retValue = string.Empty;
            HtmlNode selectedNode = node.SelectSingleNode(xPath);
            if (selectedNode != null)
            {
                retValue = selectedNode.HasAttributes ? selectedNode.Attributes[attr].Value : string.Empty;
            }

            return retValue;
        }

        #endregion
    }
}
