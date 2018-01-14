using StockViewer.BL.DataAccessManagers;
using StockViewer.DL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace StockViewer.Api.Controllers
{

    public class StockController : ApiController
    {
        private StockManager stockManager;
        public StockController()
        {
            stockManager = new StockManager();
        }
        [HttpGet]
        public HttpResponseMessage Get()
        {
            List<BL.Entities.StockData> stocks = stockManager.GetStocks();
            return Request.CreateResponse(HttpStatusCode.OK, stocks);
        }
        [HttpGet]
        public HttpResponseMessage Get(string email)
        {
            var stocks = stockManager.GetStocks(email);
            return Request.CreateResponse(HttpStatusCode.OK, stocks);
        }

        [HttpPost]
        public HttpResponseMessage Follow(string email,int stockId )
        {
            var stocks = stockManager.FollowStock(email,stockId);
            return Request.CreateResponse(HttpStatusCode.OK, stocks);
        }
        [HttpPut]
        public HttpResponseMessage Unfollow(string email, int stockId, int x)
        {
            var stocks = stockManager.UnfollowStock(email, stockId);
            return Request.CreateResponse(HttpStatusCode.OK, stocks);
        }
    }
}
