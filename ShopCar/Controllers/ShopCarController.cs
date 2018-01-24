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
            List<Production> m = getProductionList();
            return View(m);
        }

        [HttpPost]
        public ActionResult Info(List<Production> m)
        {
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

        public class Production
        {
            public int ID { get; set; }
            public string Name { get; set; }
            public int Price { get; set; }
            public string Company { get; set; }
            public int Count { get; set; }
        }
        
    }
}