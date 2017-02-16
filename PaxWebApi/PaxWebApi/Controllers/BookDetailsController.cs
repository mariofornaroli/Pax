using Entities;
using PaxServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace PaxWebApi.Controllers
{
    public class BookDetailsController : ApiController
    {
        private IBookManager bookManager = null;

        public BookDetailsController(IBookManager _bookManager)
        {
            bookManager = _bookManager;
        }

        // Allow CORS for all origins. (Caution!)
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        //public ResultModel<BookDetailsItem> Post([FromBody]string completeHref)
        public ResultModel<BookDetailsItem> Post(BookItem bookItem)
        {
            var resultModel = new ResultModel<BookDetailsItem>();
            resultModel.ResultData = bookManager.GetDetailsBook(bookItem.CompleteHref);

            /* Return data */
            resultModel.OperationResult = true;
            return resultModel;
        }
    }
}
