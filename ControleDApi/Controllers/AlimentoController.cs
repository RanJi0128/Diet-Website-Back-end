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
using ControleDApi.DAL;
using ControleDApi.Models;
using System.Web.Http.Cors;
using System.Web.Script.Serialization;
using ControleDApi.App_Start;

namespace ControleDApi.Controllers
{

    //[EnableCors(origins: "*", headers: "*", methods: "*")]
    [RoutePrefix("api/Alimento")]
    public class AlimentoController : ApiController
    {
        private Context db = new Context();

        // GET: api/Alimento 18
        [HttpGet]
        [Authorize(Roles = "Medico")]
        [Route("GetAlimentos")]
        [Route("")]
        public IQueryable<Alimento> GetAlimentos()
        {


            //var caminho = "C:/Users/thiago.nsilva.ABNOTE/Desktop/alimentos.json";
            //string text = System.IO.File.ReadAllText(caminho);

    //        System.IO.StreamReader file =
    //new System.IO.StreamReader(caminho);

            var line = "";
            //try
            //{

            //    //while ((line = file.ReadLine()) != null)
            //    //{
            //    //    System.Console.WriteLine(line);
            //    //    // counter++;
            //    //}


            //    //JavaScriptSerializer jsonSerializer = new JavaScriptSerializer();
            //    //List<Alimento> alimentos = jsonSerializer.Deserialize<List<Alimento>>(text);


            //    //db.Alimento.AddRange(alimentos);

            //    //db.SaveChanges();

            //}
            //catch (Exception e)
            //{

            //    throw;
            //}
            var x = db.Alimento.ToList();
            return db.Alimento;
        }

        // GET: api/Alimento/5
        [ResponseType(typeof(Alimento))]
        [HttpGet]
        [Authorize(Roles = "Administrador,Medico,Paciente")]
        [Route("GetAlimento")]
        [Route("")]
        public IHttpActionResult GetAlimento(int id)
        {
            Alimento alimento = db.Alimento.Find(id);
            if (alimento == null)
            {
                return NotFound();
            }

            return Ok(alimento);
        }

        // PUT: api/Alimento/5
        [ResponseType(typeof(void))]
        [Authorize(Roles = "Administrador,Medico")]
        [Route("")]
        public IHttpActionResult PutAlimento(int id, Alimento alimento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != alimento.Id)
            {
                return BadRequest();
            }

            db.Entry(alimento).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlimentoExists(id))
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

        // POST: api/Alimento
        [ResponseType(typeof(Alimento))]
        [Authorize(Roles = "Administrador,Medico")]
        [Route("")]
        public IHttpActionResult PostAlimento(Alimento alimento)
        {
            try
            {
                db.Alimento.Add(alimento);
                db.SaveChanges();

                return Ok();
            }
            catch (Exception e)
            {
                return InternalServerError(e);
            }
        }

        // DELETE: api/Alimento/5
        [ResponseType(typeof(Alimento))]
        [Authorize(Roles = "Administrador")]
        [Route("")]
        public IHttpActionResult DeleteAlimento(int id)
        {
            Alimento alimento = db.Alimento.Find(id);
            if (alimento == null)
            {
                return NotFound();
            }

            db.Alimento.Remove(alimento);
            db.SaveChanges();

            return Ok(alimento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AlimentoExists(int id)
        {
            return db.Alimento.Count(e => e.Id == id) > 0;
        }
    }
}