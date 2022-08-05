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
using SpeedyBI_API.Models;
using System.Web.Http.Cors;
using SpeedyBI_API.Tools;

namespace SpeedyBI_API.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class loginUsersController : ApiController
    {

        private SpeedyBIEntities db = new SpeedyBIEntities();
      

        // GET: api/loginUsers
        public IQueryable<loginUsers> GetloginUsers()
        {
            return db.loginUsers;

        }

        // GET: api/loginUsers/5
        [ResponseType(typeof(loginUsers))]
        public IHttpActionResult GetloginUsers(int id)
        {
            loginUsers loginUsers = db.loginUsers.Find(id);
            if (loginUsers == null)
            {
                return NotFound();
            }

            return Ok(loginUsers);
        }

        // PUT: api/loginUsers/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutloginUsers(int id, loginUsers loginUsers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != loginUsers.idUser)
            {
                return BadRequest();
            }

            db.Entry(loginUsers).State = System.Data.Entity.EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!loginUsersExists(id))
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

        // POST: api/loginUsers
        [ResponseType(typeof(loginUsers))]
        public IHttpActionResult PostloginUsers(loginUsers loginUsers)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                
                string ePass = loginUsers.Contraseña;
                loginUsers.Contraseña = Encrypt.EncriptarPassword(ePass);

                db.loginUsers.Add( loginUsers); 
            }

            db.SaveChanges();


            return CreatedAtRoute("DefaultApi", new { id = loginUsers.idUser }, loginUsers);
        }

        // DELETE: api/loginUsers/5
        [ResponseType(typeof(loginUsers))]
        public IHttpActionResult DeleteloginUsers(int id)
        {
            loginUsers loginUsers = db.loginUsers.Find(id);
            if (loginUsers == null)
            {
                return NotFound();
            }

            db.loginUsers.Remove(loginUsers);
            db.SaveChanges();

            return Ok(loginUsers);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool loginUsersExists(int id)
        {
            return db.loginUsers.Count(e => e.idUser == id) > 0;
        }
    }
}