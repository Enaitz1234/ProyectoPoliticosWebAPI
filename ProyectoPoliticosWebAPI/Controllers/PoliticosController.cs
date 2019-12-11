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
using ProyectoPoliticosWebAPI.Models;
using System.Web.Http.Cors;

namespace ProyectoPoliticosWebAPI.Controllers
{
    public class PoliticosController : ApiController
    {
        private PoliticosModel db = new PoliticosModel();

        // GET: api/Politicos
        [EnableCors(origins: "*", headers:"*", methods:"*")]
        public IQueryable<Politicos> GetPoliticos()
        {
            return db.Politicos;
        }

        // GET: api/Politicos/5
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [ResponseType(typeof(Politicos))]
        public IHttpActionResult GetPoliticos(byte id)
        {
            Politicos politicos = db.Politicos.Find(id);
            if (politicos == null)
            {
                return NotFound();
            }

            return Ok(politicos);
        }

        // PUT: api/Politicos/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutPoliticos(byte id, Politicos politicos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != politicos.ID)
            {
                return BadRequest();
            }

            db.Entry(politicos).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!PoliticosExists(id))
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

        // POST: api/Politicos
        [ResponseType(typeof(Politicos))]
        public IHttpActionResult PostPoliticos(Politicos politicos)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Politicos.Add(politicos);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (PoliticosExists(politicos.ID))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = politicos.ID }, politicos);
        }

        // DELETE: api/Politicos/5
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        [ResponseType(typeof(Politicos))]
        public IHttpActionResult DeletePoliticos(byte id)
        {
            Politicos politicos = db.Politicos.Find(id);
            if (politicos == null)
            {
                return NotFound();
            }

            db.Politicos.Remove(politicos);
            db.SaveChanges();

            return Ok(politicos);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool PoliticosExists(byte id)
        {
            return db.Politicos.Count(e => e.ID == id) > 0;
        }
    }
}