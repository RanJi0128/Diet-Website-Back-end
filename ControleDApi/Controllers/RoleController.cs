using ControleDApi.DAL;
using ControleDApi.Models.Auth;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace ControleDApi.Controllers
{
    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Role")]
    public class RoleController : ApiController
    {

        private Context db = new Context();

        // GET: api/Alimento 18
        [HttpGet]
        [Route("GetRoles")]
        [Route("")]
        public List<CustomRole> GetRoles()
        {

            var roles = db.Roles.Where(x => !x.Name.ToUpper().Contains("ADMINISTRADOR")).ToList();
            return roles;
        }

    }
}