using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PaxWebApi.Controllers
{
    public class HeartBooksController : ApiController
    {
        public string Get()
        {
            return "Heart Books";
        }
    }
}
