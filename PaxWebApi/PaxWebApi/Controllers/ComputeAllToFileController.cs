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
    public class ComputeAllToFileController : ApiController
    {
        private IBookManager bookManager = null;

        public ComputeAllToFileController(IBookManager _bookManager)
        {
            bookManager = _bookManager;
        }

        // Allow CORS for all origins. (Caution!)
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public BaseResultModel Get()
        {
            var resultModel = new BaseResultModel();

            /* Compute heart books and heart books details to file */
            resultModel = bookManager.ComputePaxToFile();

            /* Return data */
            resultModel.OperationResult = true;
            return resultModel;
        }
    }
}
