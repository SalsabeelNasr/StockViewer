using StockViewer.BL.Entities;
using StockViewer.DL;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace StockViewer.BL.DataAccessManagers
{
    public class StockManager
    {
        public List<StockData> GetStocks()
        {
            using (StockViewerEntities db = new StockViewerEntities())
            {
                var stocks = db.Stocks.Select(stock => new StockData
                {

                    Id = stock.Id,
                    Code = stock.Code,
                    Name = stock.Name,
                });
                return stocks.ToList();
            }
        }
        public List<StockData> GetStocks(string email)
        {
            using (StockViewerEntities db = new StockViewerEntities())
            {
                var stocks = from user in db.StockAspNetUsers
                             where user.AspNetUser.Email == email
                             select new StockData
                             {
                                 Id = user.Stock.Id,
                                 Code = user.Stock.Code,
                                 Name = user.Stock.Name,
                                 IsFollowed = true
                             };
                return stocks.ToList();
            }
        }
        public bool FollowStock(string email, int stockId)
        {
            using (StockViewerEntities db = new StockViewerEntities())
            {
                bool alreadyFollowed = db.StockAspNetUsers.Any(
                    ts => ts.Stock.Id == stockId && ts.AspNetUser.Email.ToLower() == email.ToLower());
                if (!alreadyFollowed)
                {
                    AspNetUser user = db.AspNetUsers.FirstOrDefault(t => t.Email.ToLower() == email.ToLower());
                    Stock stock = db.Stocks.FirstOrDefault(s => s.Id == stockId);
                    if (user != null && stock != null)
                    {
                        StockAspNetUser ts = new StockAspNetUser();
                        ts.AspNetUser = user;
                        ts.Stock = stock;
                        db.StockAspNetUsers.Add(ts);
                        db.SaveChanges();
                        return true;
                    }
                }
                return true;
            }
        }
        public bool UnfollowStock(string email, int stockId)
        {
            using (StockViewerEntities db = new StockViewerEntities())
            {
                    StockAspNetUser stock = (from ts in db.StockAspNetUsers
                                             where ts.AspNetUser.Email.ToLower() == email.ToLower() &&
                                                 ts.Stock.Id == stockId
                                             select ts).FirstOrDefault();
                    if (stock != null)
                    {
                        db.Entry(stock).State = EntityState.Deleted;
                        db.SaveChanges();
                    }
                return true;
            }
        }
    }
}
