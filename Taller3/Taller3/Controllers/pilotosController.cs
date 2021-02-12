using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Taller3.Models;

namespace Taller3.Controllers
{
    public class pilotosController : ApiController
    {
        Taller3Entities1 dbContext = new Taller3Entities1();
        // GET api/pilotos
        public IQueryable<pilotos> Get()
        {
            //pilotos lista= pilotos.
            
            var lista = dbContext.pilotos;
           

            return lista;
        }

        // GET api/pilotos/5
        [ResponseType(typeof(pilotos))]
        public IHttpActionResult Get(int id)
        {   
            pilotos buscado= dbContext.pilotos.Find(id);
            if (buscado == null)
            {
                return NotFound();
            }

            return Ok(buscado);
        }

        // POST: api/pilotos
        [ResponseType(typeof(pilotos))]
        public IHttpActionResult Postpilotos(pilotos piloto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            dbContext.pilotos.Add(piloto);

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateException)
            {
                pilotos buscado = dbContext.pilotos.Find(piloto.codigo);
                if (buscado == null)
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = piloto.codigo }, piloto);
        }

        // PUT: api/pilotos/5
        [ResponseType(typeof(void))]
        [HttpPut]
        public IHttpActionResult Putpiloto(int id, pilotos piloto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != piloto.codigo)
            {
                return BadRequest();
            }

            dbContext.Entry(piloto).State = EntityState.Modified;

            try
            {
                dbContext.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                pilotos buscado = dbContext.pilotos.Find(id);
                if (buscado==null)
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

        // DELETE: api/pilotos/5
        [ResponseType(typeof(pilotos))]
        public IHttpActionResult Deletepiloto(int id)
        {
            pilotos piloto = dbContext.pilotos.Find(id);
            if (piloto == null)
            {
                return NotFound();
            }

            dbContext.pilotos.Remove(piloto);
            dbContext.SaveChanges();

            return Ok(piloto);
        }



    }
}
