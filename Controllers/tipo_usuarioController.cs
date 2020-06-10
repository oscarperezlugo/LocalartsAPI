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
    public class tipo_usuarioController : ApiController
    {
        private readonly localartsEntities db = new localartsEntities();

        // GET: api/tipo_usuario
        public IQueryable<tipo_usuario> Gettipo_usuario()
        {
            return db.tipo_usuario;
        }

        // GET: api/tipo_usuario/5
        [ResponseType(typeof(tipo_usuario))]
        public IHttpActionResult Gettipo_usuario(int id)
        {
            tipo_usuario tipo_usuario = db.tipo_usuario.Find(id);
            if (tipo_usuario == null)
            {
                return NotFound();
            }

            return Ok(tipo_usuario);
        }

        // PUT: api/tipo_usuario/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttipo_usuario(int id, tipo_usuario tipo_usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tipo_usuario.id_tipo_usuario)
            {
                return BadRequest();
            }

            db.Entry(tipo_usuario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tipo_usuarioExists(id))
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

        // POST: api/tipo_usuario
        [ResponseType(typeof(tipo_usuario))]
        public IHttpActionResult Posttipo_usuario(tipo_usuario tipo_usuario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tipo_usuario.Add(tipo_usuario);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tipo_usuario.id_tipo_usuario }, tipo_usuario);
        }

        // DELETE: api/tipo_usuario/5
        [ResponseType(typeof(tipo_usuario))]
        public IHttpActionResult Deletetipo_usuario(int id)
        {
            tipo_usuario tipo_usuario = db.tipo_usuario.Find(id);
            if (tipo_usuario == null)
            {
                return NotFound();
            }

            db.tipo_usuario.Remove(tipo_usuario);
            db.SaveChanges();

            return Ok(tipo_usuario);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tipo_usuarioExists(int id)
        {
            return db.tipo_usuario.Count(e => e.id_tipo_usuario == id) > 0;
        }
    }
}