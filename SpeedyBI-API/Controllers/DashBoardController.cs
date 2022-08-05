using SpeedyBI_API.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace SpeedyBI_API.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    public class DashBoardController : ApiController
    {

        // GET: api/DashBoard

        private SpeedyBIEntities db = new SpeedyBIEntities();

        public IQueryable<DashBoards> GetAllDashBoards()
        {
            return db.DashBoards;
        }

        // GET: api/DashBoard/5
        [ResponseType(typeof(DashBoards))]
        public IHttpActionResult GetDashBoardId(int id)
        {
            DashBoards dash = db.DashBoards.Find(id);
            if (dash == null)
            {
                return NotFound();
            }
            return Ok(dash);
        }

        // POST: api/DashBoard
        [ResponseType(typeof(DashBoards))]
        public IHttpActionResult PostDashBoard(DashBoards dash)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else
            {
                db.DashBoards.Add(dash);
                db.SaveChanges();
            }
            return CreatedAtRoute("DefaultApi", new { id = dash.id }, dash);
        }

        // PUT: api/DashBoard/5
        [ResponseType(typeof(DashBoards))]
        public IHttpActionResult PutDashBoard(int id, DashBoards dashBoards)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }
            else if (id != dashBoards.id)
            {
                return BadRequest();
            }
            else
            {
                db.Entry(dashBoards).State = System.Data.Entity.EntityState.Modified;

                try
                {
                    db.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!DashBoardExists(id))
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
        }

        // DELETE: api/DashBoard/5
        [ResponseType(typeof(DashBoards))]
        public IHttpActionResult DeletedashBoard(int id)
        {
            DashBoards dashBoards = db.DashBoards.Find(id);
            if (dashBoards == null)
            {
                return NotFound();
            }
            else
            {
                db.DashBoards.Remove(dashBoards);
                db.SaveChanges();
            }

            return Ok(dashBoards);
        }

        private bool DashBoardExists(int id)
        {
            return db.loginUsers.Count(e => e.idUser == id) > 0;
        }
    }
}
