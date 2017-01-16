using HtmlAgilityPack;
using System;
using System.Collections.Generic;
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
    }
}
