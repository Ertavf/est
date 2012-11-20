using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using MvcApplication2.Models;

namespace MvcApplication2.Controllers.API
{
    public class SanayiController : ApiController
    {
        private MvcApplication2Context db = new MvcApplication2Context();

        // GET api/Sanayi
        public IEnumerable<Sanayi> GetSanayis()
        {
            return db.Sanayis.AsEnumerable();
        }

        // GET api/Sanayi/5
        public Sanayi GetSanayi(int id)
        {
            Sanayi sanayi = db.Sanayis.Find(id);
            if (sanayi == null)
            {
                throw new HttpResponseException(Request.CreateResponse(HttpStatusCode.NotFound));
            }

            return sanayi;
        }

        // PUT api/Sanayi/5
        public HttpResponseMessage PutSanayi(int id, Sanayi sanayi)
        {
            if (ModelState.IsValid && id == sanayi.SanayiId)
            {
                db.Entry(sanayi).State = EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    return Request.CreateResponse(HttpStatusCode.NotFound);
                }

                return Request.CreateResponse(HttpStatusCode.OK);
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // POST api/Sanayi
        public HttpResponseMessage PostSanayi(Sanayi sanayi)
        {
            if (ModelState.IsValid)
            {
                db.Sanayis.Add(sanayi);
                db.SaveChanges();

                HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created, sanayi);
                response.Headers.Location = new Uri(Url.Link("DefaultApi", new { id = sanayi.SanayiId }));
                return response;
            }
            else
            {
                return Request.CreateResponse(HttpStatusCode.BadRequest);
            }
        }

        // DELETE api/Sanayi/5
        public HttpResponseMessage DeleteSanayi(int id)
        {
            Sanayi sanayi = db.Sanayis.Find(id);
            if (sanayi == null)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            db.Sanayis.Remove(sanayi);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                return Request.CreateResponse(HttpStatusCode.NotFound);
            }

            return Request.CreateResponse(HttpStatusCode.OK, sanayi);
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}