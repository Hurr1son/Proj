using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.Entity;
using Microsoft.AspNet.Identity;
using System.Text.RegularExpressions;
using WebSite.Models;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.Net.Mail;
using System.Text;

namespace WebSite.Controllers
{
    public class HomeController : Controller
    {
        private string UserId { get; set; }
        private Context db = new Context();

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult AddQueue()
        {
            return View();
        }

        public ActionResult Edit()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return HttpNotFound();
            }
            Queue queue = db.Queues.Find(id);
            if (queue != null)
            {
                return View(queue);
            }
            return HttpNotFound();
        }
        [HttpPost]
        public ActionResult Edit(Queue q, int id, int CarId, string CarStatus)
        {
            q.CarStatus = CarStatus;
            db.Entry(q).State = EntityState.Modified;
            db.SaveChanges();
            return RedirectToAction("AdminPanel");
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddQueue([Bind(Include = "Description, Email")] Queue Queue)
        {
            Queue.CarStatus = "В ожидании";
            Queue.CarId = GetCarId();
            using (Context db = new Context())
            {
                db.Queues.Add(Queue);
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }

        [Authorize(Roles = "Admin")]
        public ActionResult AdminPanel()
        {
            using (Context db = new Context())
            {
                var que = db.Queues.ToList();
                var car = db.Cars.ToList();
                return View(db.Queues.ToList());
            }

        }
        public async Task<ActionResult> SendMessage(Queue queue, string Email)
        {

            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(Email, "Ваше авто готово", "Здравствуйте \n " +
                " \n Ради сообщить вам, что ваш автомобиль готов и вы можете забрать его в удобное для вас время");
            return RedirectToAction("Index");
        }

        public async Task<ActionResult> SendMessage2(Queue queue, string Email, string Datetime)
        {

            EmailService emailService = new EmailService();
            await emailService.SendEmailAsync(Email, "Ваша очередь", "Здравствуйте. Ваша очередь назначена на " + Datetime + ". Ждем вас с нетерпением. С уважением австосервис Сar Maintenance");

            return RedirectToAction("Index");
        }

        public ActionResult Addrepair()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult AddRepair([Bind(Include = "RepairDesc, DateTime")] Repair repair, int id)
        {
            repair.CarId = id;
            using (Context db = new Context())
            {
                db.Repairs.Add(repair);
                db.SaveChanges();
            }
            return RedirectToAction("Index");

        }
        public ActionResult Service()
        {
            return View();
        }

        public ActionResult About()
        {
            return View();
        }
        public ActionResult Contact()
        {

            return View();
        }


        public ActionResult CarsView()
        {
            return View(db.Cars.ToList());
        }

        public ActionResult RepairView()
        {
            using (Context db = new Context())
            {

            }
            var Repair = db.Repairs.ToList();
            ViewData.Model = Repair;
            return View();
        }

 

        public ActionResult Delete(int id)
        {
            Car c = new Car { Id = id };
            db.Entry(c).State = EntityState.Deleted;
            db.SaveChanges();
            ViewBag.Message = "Успешно удалено";
            return RedirectToAction("AdminPanel");
        }

        public ActionResult Delete2(int id)
        {
            Queue q = new Queue { Id = id };
            db.Entry(q).State = EntityState.Deleted;
            db.SaveChanges();
            ViewBag.Message = "Успешно удалено";
            return RedirectToAction("AdminPanel");
        }

        public ActionResult UserPanel()
        {
            Car c;

            if (IsUserHasCar(out c))
            {
                ViewBag.IsUserHasCar = true;
                ViewBag.Car = c;
            }
            else
            {
                ViewBag.IsUserHasCar = false;
            }

            return View(db.Queues.ToList());
        }


        [HttpGet]
        public ActionResult AddCar2()
        {
            Car c;
            bool res = IsUserHasCar(out c);

            if (res)
            {
                ViewBag.Message = "AlreadyHasCar";
                ViewBag.IsUserHasCar = false;
                return View("UserPanel");
            }

            return View();
        }

        [HttpPost]
        public ActionResult AddCar2([Bind(Include = "Brand,Model,Year,TankAmount,IsCarHasDC")] Car Car, string EngineType)
        {
            Car.UserId = User.Identity.GetUserId();
            Car.EngineType = EngineType;

            using (Context db = new Context())
            {
                db.Cars.Add(Car);
                db.SaveChanges();
            }

            return RedirectToAction("UserPanel");
         
        }
  
        public bool IsUserHasCar(out Car Car)
        {
            this.UserId = User.Identity.GetUserId();

            var result = GetCarWebApi(UserId);

            if (result != null)
            {
                Car = result;
                return true;
            }
            else
            {
                Car = null;
                return false;
            }

        }

        #region WEBAPI
        public Car GetCarWebApi(string id)
        {
            HttpClient client = new HttpClient();
            string uri = "http://localhost:3250/api/Server/GetCars/" + User.Identity.GetUserId();
            string res = client.GetStringAsync(uri).Result;

            var car = JsonConvert.DeserializeObject(res, typeof(Car));

            return (Car)car;
        }

        public IEnumerable<Queue> GetQueueWebApi()
        {
            HttpClient client = new HttpClient();
            string uri = "http://localhost:3250/api/Server/GetQueue/" + GetCarId().ToString();
            string res = client.GetStringAsync(uri).Result;

            var queues = JsonConvert.DeserializeObject(res, typeof(IEnumerable<Queue>));

            return (IEnumerable<Queue>)queues;
        }

        #endregion

        public int GetCarId()
        {
            this.UserId = User.Identity.GetUserId();

            var result = GetCarWebApi(UserId);

            if (result != null)
            {
                return result.Id;
            }
            else
            {
                return -1;
            }
        }

    }
}