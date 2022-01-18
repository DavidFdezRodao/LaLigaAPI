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
    public class JugadoresController : ApiController
    {
        LaLigaData laLigaDb = new LaLigaData();
        private static readonly ILog log = LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        // GET: Jugadores
        public IHttpActionResult GetAll()
        {
            log.Info("Comienzo /Jugadores (GET)");
            try
            {
                List<JUGADORES> jugadores = laLigaDb.GetAllPlayers();
                if (jugadores != null && jugadores.Count > 0)
                {
                    log.Info("Valores encontrados");
                    return Ok(jugadores);
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

            log.Info("Final /Jugadores (GET)");
        }

        // GET: Jugadores/Equipo
        [Route("api/Jugadores/Equipo/{equipo}")]
        public IHttpActionResult GetTeamPlayers(int equipo)
        {
            log.Info("Comienzo /Jugadores/Equipo/{equipo} (GET)");
            log.Info("Parametro equipo: " + equipo == null ? string.Empty : equipo.ToString());

            try
            {
                List<JUGADORES> jugadores = laLigaDb.GetTeamPlayers(equipo);
                if (jugadores != null && jugadores.Count > 0)
                {
                    log.Info("Valores encontrados");
                    return Ok(jugadores);
                }
                else
                {
                    log.Info("No hay valores");
                    return NotFound();
                }
            }
            catch(Exception ex)
            {
                log.Error(ex);
                return InternalServerError();
            }

            log.Info("Final /Jugadores/Equipo/{equipo} (GET)");
        }

        // GET: Jugadores/Nombre
        [Route("api/Jugadores/Nombre/{nombre}")]
        public IHttpActionResult GetPlayer(String nombre)
        {
            log.Info("Comienzo /Jugadores/Nombre/{nombre} (GET)");
            log.Info("Parametro nombre: " + nombre);
            try
            {
                JUGADORES jugador = laLigaDb.GetPlayer(nombre);
                if (jugador != null)
                {
                    log.Info("Jugador encontrado");
                    return Ok(jugador);
                }
                else
                {
                    log.Info("No hay jugadores con ese nombre");
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return InternalServerError();
            }

            log.Info("Final /Jugadores/Nombre/{nombre} (GET)");
        }

        // POST: Jugadores
        public IHttpActionResult AddPlayer(JUGADORES jugador)
        {
            log.Info("Comienzo /Jugadores (POST)");
            log.Info("Parametro jugador: " + JsonConvert.SerializeObject(jugador));
            try
            {
                CLUBES club = laLigaDb.GetClub(jugador.CLUB ?? default(int));
                POSICIONES posicion = laLigaDb.GetPosition(jugador.POSICION ?? default(int));
                int salarioTotalClub = laLigaDb.GetSalariosClub(jugador.CLUB ?? default(int));
                if(jugador.SALARIO == null)
                {
                    jugador.SALARIO = 0;
                }

                if (club == null && jugador.CLUB != null)
                {
                    log.Warn("CLUB no encontrado");
                    return BadRequest("El club no está en base de datos");
                } else if (club != null && club.PRESUPUESTO < salarioTotalClub + jugador.SALARIO)
                {
                    log.Warn("CLUB sin presupuesto suficiente");
                    return BadRequest("El club no tiene suficiente presupuesto");
                }
                else if(posicion == null && jugador.POSICION != null)
                {
                    log.Warn("POSICION no encontrada");
                    return BadRequest("La posición no está en base de datos");
                }
                else
                {
                    laLigaDb.AddPlayer(jugador);
                    log.Info("Jugador insertado");
                    return Ok();
                }           
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return InternalServerError();
            }

            log.Info("Final /Jugadores (POST)");
        }

        // PUT: Jugadores
        [Route("api/Jugadores/{jugador}/{equipo}")]
        [HttpPut]
        public IHttpActionResult EditPlayer(int jugador, int equipo)
        {
            log.Info("Comienzo /Jugadores/{jugador}/{equipo} (PUT)");
            log.Info("Parametro jugador: " + jugador == null ? string.Empty : jugador.ToString());
            log.Info("Parametro equipo: " + equipo == null ? string.Empty : equipo.ToString());
            try
            {
                JUGADORES player = laLigaDb.GetPlayer(jugador);
                CLUBES club = laLigaDb.GetClub(equipo);
                int salarioTotalClub = laLigaDb.GetSalariosClub(equipo);

                if (player == null)
                {
                    log.Warn("JUGADOR no encontrado");
                    return BadRequest("El jugador no está en base de datos");
                }
                else if (club == null)
                {
                    log.Warn("CLUB no encontrado");
                    return BadRequest("El club no está en base de datos");
                }
                else if (club.PRESUPUESTO < salarioTotalClub + player.SALARIO)
                {
                    log.Warn("CLUB sin presupuesto suficiente");
                    return BadRequest("El club no tiene suficiente presupuesto");
                }
                else
                {
                    laLigaDb.RegisterPlayer(jugador,equipo);
                    log.Info("Jugador inscrito");
                    return Ok();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return InternalServerError();
            }

            log.Info("Final /Jugadores/{jugador}/{equipo} (PUT)");
        }

        // DELETE: Jugadores
        [Route("api/Jugadores/{jugador}")]
        [HttpDelete]
        public IHttpActionResult DeletePlayer(int jugador)
        {
            log.Info("Comienzo /Jugadores/{jugador} (DELETE)");
            log.Info("Parametro jugador: " + jugador == null ? string.Empty : jugador.ToString());
            try
            {
                JUGADORES player = laLigaDb.GetPlayer(jugador);

                if (player == null)
                {
                    log.Warn("JUGADOR no encontrado");
                    return BadRequest("El jugador no está en base de datos");
                }
                else
                {
                    laLigaDb.DeletePlayer(jugador);

                    return Ok();
                }
            }
            catch (Exception ex)
            {
                log.Error(ex);
                return InternalServerError();
            }

            log.Info("Final /Jugadores/{jugador} (DELETE)");
        }

    }
}