using Entities;
using PaxServices;
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
        private IBookManager bookManager = null;

        public HeartBooksController(IBookManager _bookManager)
        {
            bookManager = _bookManager;
        }

        public ResultModel<List<BookItem>> Get()
        {
            var resultModel = new ResultModel<List<BookItem>>();
            resultModel.ResultData = bookManager.GetHeartBooks();

            /* Return data */
            resultModel.OperationResult = true;
            return resultModel;
        }
    }
}
