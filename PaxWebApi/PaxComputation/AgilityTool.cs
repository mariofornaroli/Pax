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
    }
}
