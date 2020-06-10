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
using LocalartsAPI.Models;

namespace LocalartsAPI.Controllers
{
    [Authorize]
    public class tipo_postController : ApiController
    {
        private readonly localartsEntities db = new localartsEntities();

        // GET: api/tipo_post
        public IQueryable<tipo_post> Gettipo_post()
        {
            return db.tipo_post;
        }

        // GET: api/tipo_post/5
        [ResponseType(typeof(tipo_post))]
        public IHttpActionResult Gettipo_post(int id)
        {
            tipo_post tipo_post = db.tipo_post.Find(id);
            if (tipo_post == null)
            {
                return NotFound();
            }

            return Ok(tipo_post);
        }

        // PUT: api/tipo_post/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttipo_post(int id, tipo_post tipo_post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipo_post.id_tipo_post)
            {
                return BadRequest();
            }

            db.Entry(tipo_post).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tipo_postExists(id))
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

        // POST: api/tipo_post
        [ResponseType(typeof(tipo_post))]
        public IHttpActionResult Posttipo_post(tipo_post tipo_post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tipo_post.Add(tipo_post);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tipo_post.id_tipo_post }, tipo_post);
        }

        // DELETE: api/tipo_post/5
        [ResponseType(typeof(tipo_post))]
        public IHttpActionResult Deletetipo_post(int id)
        {
            tipo_post tipo_post = db.tipo_post.Find(id);
            if (tipo_post == null)
            {
                return NotFound();
            }

            db.tipo_post.Remove(tipo_post);
            db.SaveChanges();

            return Ok(tipo_post);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tipo_postExists(int id)
        {
            return db.tipo_post.Count(e => e.id_tipo_post == id) > 0;
        }
    }
}