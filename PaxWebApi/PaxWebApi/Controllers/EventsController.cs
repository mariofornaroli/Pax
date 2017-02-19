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
    public class EventsController : ApiController
    {
        private IBookManager bookManager = null;

        public EventsController(IBookManager _bookManager)
        {
            bookManager = _bookManager;
        }

        // Allow CORS for all origins. (Caution!)
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public ResultModel<EventsModel> Get()
        {
            var resultModel = new ResultModel<EventsModel>();
            resultModel.ResultData = bookManager.GetEvents();

            /* Return data */
            resultModel.OperationResult = true;
            return resultModel;
        }
    }
}
