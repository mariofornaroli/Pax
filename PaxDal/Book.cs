﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PaxDal
{
    public class Book
    {
        public string Id { get; set; }
        public string ImgSrc { get; set; }
        public string Href { get; set; }
        public string CompleteHref { get; set; }
        public string Description { get; set; }
        public string ShortDescription { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public DateTime? DateComputation { get; set; }
    }
}
