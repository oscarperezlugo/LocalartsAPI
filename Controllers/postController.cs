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
    public class postController : ApiController
    {
        private readonly localartsEntities db = new localartsEntities();

        // GET: api/posts
        public IQueryable<post> Getpost(Guid guid)
        {
            return db.post.Where(u => u.guid == guid).AsQueryable();
        }

        // GET: api/posts/5
        [ResponseType(typeof(post))]
        public IHttpActionResult Getpost(int id_post)
        {
            post post = db.post.Find(id_post);
            if (post == null)
            {
                return NotFound();
            }

            return Ok(post);
        }


        // PUT: api/posts/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Putpost(int id_post, post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id_post != post.id_post)
            {
                return BadRequest();
            }

            db.Entry(post).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!postExists(id_post))
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

        // POST: api/posts
        [ResponseType(typeof(post))]
        public IHttpActionResult Postpost(post post)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.post.Add(post);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = post.id_post }, post);
        }

        // DELETE: api/posts/5
        [ResponseType(typeof(post))]
        public IHttpActionResult Deletepost(int id_post)
        {
            post post = db.post.Find(id_post);
            if (post == null)
            {
                return NotFound();
            }

            db.post.Remove(post);
            db.SaveChanges();

            return Ok(post);
        }

        // DELETE: api/posts/5
        [ResponseType(typeof(post))]
        public IHttpActionResult Deletepost(Guid guid)
        {
            post post = db.post.Where(u => u.guid == guid).FirstOrDefault<post>();

            //post post = db.post.Find(id);
            if (post == null)
            {
                return NotFound();
            }

            db.post.Remove(post);
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

        private bool postExists(int id)
        {
            return db.post.Count(e => e.id_post == id) > 0;
        }
    }
}