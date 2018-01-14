using System;

namespace StockViewer.BL.Entities
{
    public class StockData
    {
        private static Random rnd = new Random(DateTime.Now.Second);
        public int Id { get; set; }

        public string Name { get; set; }

        public string Code { get; set; }

        private double price;

        public double Price
        {
            get
            {
                return rnd.Next(999) + Math.Round(rnd.NextDouble(), 3);
            }
            set
            {
                price = value;
            }
        }

        public bool IsFollowed { get; set; }
    }
}
