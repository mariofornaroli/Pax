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
    public class BestSellersController : ApiController
    {
        private IBookManager bookManager = null;

        public BestSellersController(IBookManager _bookManager)
        {
            bookManager = _bookManager;
        }

        // Allow CORS for all origins. (Caution!)
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public ResultModel<BooksListModel> Get()
        {
            var resultModel = new ResultModel<BooksListModel>();
            resultModel.ResultData = bookManager.GetBestSellers();

            /* Return data */
            resultModel.OperationResult = true;
            return resultModel;
        }
    }
}
