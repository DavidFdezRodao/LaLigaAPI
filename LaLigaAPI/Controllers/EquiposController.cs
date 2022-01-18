using LaLigaAPI.Datos;
using LaLigaAPI.Models;
using log4net;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;

namespace LaLigaAPI.Controllers
{
    public class EquiposController : ApiController
    {
        LaLigaData laLigaDb = new LaLigaData();
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: Jugadores
        public IHttpActionResult GetAll()
        {
            log.Info("Comienzo /Equipos (GET)");
            try
            {
                List<CLUBES> equipos = laLigaDb.GetAllClubes();
                if (equipos != null && equipos.Count > 0)
                {
                    log.Info("Valores encontrados");
                    return Ok(equipos);
                }
                else
                {
                    log.Info("No hay valores");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return InternalServerError();
            }

            log.Info("Final /Equipos (GET)");
        }

        // GET: Equipos/Nombre
        [Route("api/Equipos/{nombre}")]
        public IHttpActionResult GetTeam(String nombre)
        {
            log.Info("Comienzo /Equipos/{nombre} (GET)");
            log.Info("Parametro nombre: " + nombre);
            try
            {
                CLUBES club = laLigaDb.GetClub(nombre);
                if (club != null)
                {
                    log.Info("Club encontrado");
                    return Ok(club);
                }
                else
                {
                    log.Info("No hay clubes con ese nombre");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return InternalServerError();
            }

            log.Info("Final /Equipos/{nombre} (GET)");
        }

        // POST: Equipos
        public IHttpActionResult AddClub(CLUBES club)
        {
            log.Info("Comienzo /Equipos (POST)");
            log.Info("Parametro club: " + JsonConvert.SerializeObject(club));
            try
            {                
                    laLigaDb.AddClub(club);
                    log.Info("Club insertado");
                    return Ok();
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return InternalServerError();
            }

            log.Info("Final /Equipos (POST)");
        }

        // PUT: Equipos
        [Route("api/Equipos/{equipo}/{presupuesto}")]
        [HttpPut]
        public IHttpActionResult EditClub(int equipo, int presupuesto)
        {
            log.Info("Comienzo /Equipos/{equipo}/{presupuesto} (PUT)");
            log.Info("Parametro equipo: " + equipo == null ? string.Empty : equipo.ToString());
            log.Info("Parametro presupuesto: " + presupuesto == null ? string.Empty : presupuesto.ToString());
            try
            {
                CLUBES club = laLigaDb.GetClub(equipo);

                if (club == null)
                {
                    log.Warn("CLUB no encontrado");
                    return BadRequest("El club no está en base de datos");
                }
                else
                {
                    laLigaDb.EditClub(equipo,presupuesto);
                    log.Info("Presupuesto cambiado");
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return InternalServerError();
            }

            log.Info("Final /Equipos/{equipo}/{presupuesto} (PUT)");
        }

        // DELETE: Equipos
        [Route("api/Equipos/{equipo}")]
        [HttpDelete]
        public IHttpActionResult DeleteClub(int equipo)
        {
            log.Info("Comienzo /Equipos/{equipo} (DELETE)");
            log.Info("Parametro equipo: " + equipo == null ? string.Empty : equipo.ToString());
            try
            {
                CLUBES club = laLigaDb.GetClub(equipo);

                if (club == null)
                {
                    log.Warn("CLUB no encontrado");
                    return BadRequest("El club no está en base de datos");
                }
                else
                {
                    laLigaDb.DeleteClub(equipo);

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return InternalServerError();
            }

            log.Info("Final /Equipos/{equipo} (DELETE)");
        }

    }    
}