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
    public class comentariosController : ApiController
    {
        private localartsEntities db = new localartsEntities();

        // GET: api/comentarios
        public IQueryable<comentario> Getcomentario(Guid guid)
        {
            return db.comentario.Where(u => u.guid == guid).AsQueryable();
        }

        // GET: api/comentarios/5
        [ResponseType(typeof(comentario))]
        public IHttpActionResult Getcomentario(int id_comentario)
        {
            comentario comentario = db.comentario.Find(id_comentario);
            if (comentario == null)
            {
                return NotFound();
            }

            return Ok(comentario);
        }

        // GET: api/comentarios
        public IQueryable<comentario> Getcomentariobypost(int id_post)
        {
            return db.comentario.Where(c => c.id_post == id_post);
        }

        // PUT: api/comentarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putcomentario(int id, comentario comentario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != comentario.id_comentario)
            {
                return BadRequest();
            }

            db.Entry(comentario).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!comentarioExists(id))
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

        // POST: api/comentarios
        [ResponseType(typeof(comentario))]
        public IHttpActionResult Postcomentario(comentario comentario)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.comentario.Add(comentario);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = comentario.id_comentario }, comentario);
        }

        // DELETE: api/comentarios/5
        [ResponseType(typeof(usuario))]
        public IHttpActionResult Deletecomentario(Guid guid)
        {
            usuario usuario = db.usuario.Where(u => u.guid == guid).FirstOrDefault();

            if (usuario == null)
            {
                return NotFound();
            }

            IQueryable<comentario> comentarios = db.comentario.Where(c => c.guid == guid);

            foreach (comentario c in comentarios)
            {
                db.comentario.Remove(c);
            }

            db.SaveChanges();

            return Ok(usuario);
        }

        // DELETE: api/comentarios/5
        [ResponseType(typeof(comentario))]
        public IHttpActionResult Deletecomentario(int id_comentario)
        {
            comentario comentario = db.comentario.Find(id_comentario);
            if (comentario == null)
            {
                return NotFound();
            }

            db.comentario.Remove(comentario);
            db.SaveChanges();

            return Ok(comentario);
        }

        // DELETE: api/comentarios/5
        [ResponseType(typeof(post))]
        public IHttpActionResult Deletecomentariobypost(int id_post)
        {
            post post = db.post.Find(id_post);
            ICollection<comentario> comentarios = post.comentario;

            if (comentarios == null || comentarios.Count() == 0)
            {
                return NotFound();
            }

            foreach (comentario c in comentarios)
            {
                db.comentario.Remove(c);
            }

            db.SaveChanges();

            return Ok(post);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool comentarioExists(int id)
        {
            return db.comentario.Count(e => e.id_comentario == id) > 0;
        }
    }
}