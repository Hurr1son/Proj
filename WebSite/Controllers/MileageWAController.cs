using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebSite.Models;

namespace WebSite.Controllers
{
    public class MileageWAController : ApiController
    {
        private Context db = new Context();

        // GET api/Server
        public IQueryable<Mileage> GetMileages()
        {
            return db.Mileages;
        }

        // GET api/Server/5
        [ResponseType(typeof(Mileage))]
        public IHttpActionResult GetMileage(int id)
        {
            Mileage mileage = db.Mileages.Find(id);
            if (mileage == null)
            {
                return NotFound();
            }

            return Ok(mileage);
        }

        // PUT api/Server/5
        public IHttpActionResult PutMileage(int id, Mileage mileage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != mileage.Id)
            {
                return BadRequest();
            }

            db.Entry(mileage).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!MileageExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST api/Server
        [ResponseType(typeof(Mileage))]
        public IHttpActionResult PostMileage(Mileage mileage)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Mileages.Add(mileage);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = mileage.Id }, mileage);
        }

        // DELETE api/Server/5
        [ResponseType(typeof(Mileage))]
        public IHttpActionResult DeleteMileage(int id)
        {
            Mileage mileage = db.Mileages.Find(id);
            if (mileage == null)
            {
                return NotFound();
            }

            db.Mileages.Remove(mileage);
            db.SaveChanges();

            return Ok(mileage);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool MileageExists(int id)
        {
            return db.Mileages.Count(e => e.Id == id) > 0;
        }
    }
}