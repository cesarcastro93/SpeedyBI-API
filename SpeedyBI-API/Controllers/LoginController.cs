using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using SpeedyBI_API.Models;
using System.Web.Http.Cors;
using SpeedyBI_API.Tools;

namespace SpeedyBI_API.Controllers
{
    [EnableCors(origins: "http://localhost:3000", headers: "*", methods: "*")]
    [RoutePrefix("api/LoginAccess")]
    public class LoginController : ApiController
    {
        private SpeedyBIEntities db = new SpeedyBIEntities();

        [Route("Login")]
        [HttpPost]
        public IHttpActionResult PostAccessLogin(loginUsers loginUsers)
        {
            string ePasss = Encrypt.EncriptarPassword(loginUsers.Contraseña);
            var log = db.loginUsers.Where(x => x.NombreUser.Equals(loginUsers.NombreUser) && x.Contraseña.Equals(ePasss)).FirstOrDefault();

            if (log == null)
            {
                return Ok(new { status = 401, isSuccess = false, message = "Invalid User", });
            }
            else
            {
                return Ok(new { status = 200, isSuccess = true, message = "User Login successfully" });
                //return Ok(new { status = 200, isSuccess = true, message = "User Login successfully", UserDetails = log });
            }
           
        }
    }
}
