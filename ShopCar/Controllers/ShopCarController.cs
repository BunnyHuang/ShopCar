using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ShopCar.Controllers
{
    public class ShopCarController : Controller
    {
        // GET: ShopCar
        public ActionResult Index()
        {
            ShopCarViewModel m = new ShopCarViewModel();
            m.Production = getProductionList();
            m.Shipment = getShipment();
            return View(m);
        }

        [HttpPost]
        public ActionResult Info(List<Production> ProductionList, int ShipmentID)
        {
            PurchaseDetailsViewModel m = new PurchaseDetailsViewModel();
            m.Production = MarketingActivity(ProductionList);
            m.Fee = calculateFee(ProductionList, ShipmentID);
            m.totalAmt = ProductionList.Sum(x => x.DiscountPrice * x.Count) + m.Fee.Sum();
            return View(m);
        }

        private List<Production> getProductionList()
        {
            List<Production> m = new List<Production>();
            m.Add(new Production { ID = 1, Name = "二手蘋果手機", Price = 8700, Company = "XX商店" });
            m.Add(new Production { ID = 2, Name = "C# cookbook", Price = 568, Company = "XX商店" });
            m.Add(new Production { ID = 3, Name = "HP 筆電", Price = 16888, Company = "XX商店" });
            m.Add(new Production { ID = 4, Name = "哈利波特影集", Price = 2250, Company = "OO商店" });
            m.Add(new Production { ID = 5, Name = "無間道三部曲", Price = 1090, Company = "OO商店" });
            return m;
        }

        private List<Shipment> getShipment()
        {
            List<Shipment> m = new List<Shipment>();
            m.Add(new Shipment { ID = 1, Name = "宅急便", Fee = 60 });
            m.Add(new Shipment { ID = 2, Name = "郵局", Fee = 40 });
            m.Add(new Shipment { ID = 3, Name = "超商店到店", Fee = 50 });
            return m;
        }

        /// <summary>
        /// 計算運費
        /// </summary>
        /// <param name="ProductionList">購買清單</param>
        /// <param name="ShipmentID">物流ID</param>
        /// <returns></returns>
        private List<int> calculateFee(List<Production> ProductionList, int ShipmentID)
        {
            List<int> FeeList = new List<int>();

            Shipment s = getShipment().Where(x => x.ID == ShipmentID).FirstOrDefault();

            foreach (var item in ProductionList.Select(x => x.Company).Distinct())
            {
                int Fee = s.Fee;

                if (ProductionList.Where(x => x.Company == item).Sum(n => n.Count) >= 3)
                {
                    Fee = 0;
                }
                else if (item == "OO商店" && s.Name == "宅急便")
                {
                    Fee = Convert.ToInt16(Fee * 0.8);
                }

                FeeList.Add(Fee);
            }
            return FeeList;
        }

        /// <summary>
        /// 行銷活動
        /// </summary>
        /// <param name="ProductionList">購買清單</param>
        /// <returns></returns>
        public List<Production> MarketingActivity(List<Production> ProductionList)
        {
            foreach (var item in ProductionList.Select(x => x.Company).Distinct())
            {
                if(item == "OO商店")
                {
                    foreach (var p in ProductionList.Where(x => x.Company == item))
                    {
                        if (p.Count >= 3)
                            p.DiscountPrice = Convert.ToInt16(p.Price * 0.7);
                        else if (p.Count == 2)
                            p.DiscountPrice = Convert.ToInt16(p.Price * 0.9);
                        else
                            p.DiscountPrice = p.Price;
                    }
                }
                else
                {
                    foreach (var p in ProductionList.Where(x => x.Company == item))
                    {
                        p.DiscountPrice = p.Price;
                    }
                }
            }
            return ProductionList;
        }

        public class Production
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int Price { get; set; }
            public string Company { get; set; }
            public int Count { get; set; }
            public int DiscountPrice { get; set; }
        }

        public class Company
        {
            public int ID { get; set; }
            public string Name { get; set; }
        }

        public class Shipment
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int Fee { get; set; }
        }

        public class ShopCarViewModel
        {
            public List<Shipment> Shipment { get; set; }
            public List<Production> Production { get; set; }
        }

        public class PurchaseDetailsViewModel
        {
            public int totalAmt { get; set; }
            public List<Production> Production { get; set; }
            public List<int> Fee { get; set; }
        }

    }
}