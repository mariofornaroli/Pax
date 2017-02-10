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
    public class BookDetailsController : ApiController
    {
        private IBookManager bookManager = null;

        public BookDetailsController(IBookManager _bookManager)
        {
            bookManager = _bookManager;
        }

        public ResultModel<BookDetailsItem> Post([FromBody]string completeHref)
        {
            var resultModel = new ResultModel<BookDetailsItem>();
            resultModel.ResultData = bookManager.GetDetailsBook(completeHref);

            /* Return data */
            resultModel.OperationResult = true;
            return resultModel;
        }
    }
}
