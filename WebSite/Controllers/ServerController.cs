using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using WebSite.Models;

namespace WebSite.Controllers
{
    //Фрагмент коду з серверу, що отримує інформацію за бази даних
    public class ServerController : ApiController
    {
        Context db = new Context();

        public IHttpActionResult GetCars(string id)
        {
            return Json<Car>(db.Cars.SingleOrDefault(c => c.UserId == id));
        }


        public IHttpActionResult GetQueue(int id)
        {
            return Json<IEnumerable<Queue>>(db.Queues.Where(q => q.CarId == id));
        }
    }
}
