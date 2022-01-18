using LaLigaAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LaLigaAPI.Datos
{
    public class LaLigaData
    {
        #region Jugadores
        public List<JUGADORES> GetAllPlayers()
        {
            using (LaLigaEntities db = new LaLigaEntities())
            {
                var q =
                    from c in db.JUGADORES
                    select c;

                return (q.Count() != 0) ? q.AsEnumerable().ToList() : null;
            }
        }

        public List<JUGADORES> GetTeamPlayers(int equipo)
        {
            using (LaLigaEntities db = new LaLigaEntities())
            {
                var q =
                    from c in db.JUGADORES
                    where c.CLUB == equipo
                    select c;

                return (q.Count() != 0) ? q.AsEnumerable().ToList() : null;
            }
        }

        public JUGADORES GetPlayer(int idJugador)
        {
            using (LaLigaEntities db = new LaLigaEntities())
            {
                var q =
                    from c in db.JUGADORES
                    where c.ID_JUGADOR == idJugador
                    select c;

                return (q.Count() != 0) ? q.AsEnumerable().FirstOrDefault() : null;
            }
        }

        public JUGADORES GetPlayer(string nombreJugador)
        {
            using (LaLigaEntities db = new LaLigaEntities())
            {
                var q =
                    from c in db.JUGADORES
                    where c.NOMBRE == nombreJugador
                    select c;

                return (q.Count() != 0) ? q.AsEnumerable().FirstOrDefault() : null;
            }
        }

        public void DeletePlayer(int idJugador)
        {
            using (LaLigaEntities db = new LaLigaEntities())
            {
                var q =
                    from c in db.JUGADORES
                    where c.ID_JUGADOR == idJugador
                    select c;

                if (q.Count() != 0)
                {
                    db.JUGADORES.Remove(q.First());
                    db.SaveChanges();
                }
            }
        }


        public int GetSalariosClub(int equipo)
        {
            using (LaLigaEntities db = new LaLigaEntities())
            {
                var q =
                    from c in db.JUGADORES
                    where c.CLUB == equipo
                    select c;

                if (q.Count() != 0)
                {
                    int salarios = 0;
                    foreach(JUGADORES jugador in q)
                    {
                        salarios +=  (int)jugador.SALARIO;
                    }
                    return salarios;
                }
                else
                {
                    return 0;
                }
            }
        }

        public void AddPlayer(JUGADORES jugador)
        {
            using (LaLigaEntities db = new LaLigaEntities())
            {
                var newItem = new JUGADORES
                {
                    NOMBRE = jugador.NOMBRE,
                    F_NACIMIENTO = jugador.F_NACIMIENTO,
                    POSICION = jugador.POSICION,
                    SALARIO = jugador.SALARIO,
                    CLUB = jugador.CLUB
                };

                db.JUGADORES.Add(newItem);
                db.SaveChanges();
            }
        }
        
        public void RegisterPlayer(int jugador, int equipo)
        {
            using (LaLigaEntities db = new LaLigaEntities())
            {
                var q =
                    from c in db.JUGADORES
                    where c.ID_JUGADOR == jugador
                    select c;

                if (q.Count() != 0)
                {
                    q.First().CLUB = equipo;
                    db.SaveChanges();
                }
            }
        }
        #endregion

        #region Clubes

        public void AddClub(CLUBES club)
        {
            using (LaLigaEntities db = new LaLigaEntities())
            {
                var newItem = new CLUBES
                {
                    NOMBRE = club.NOMBRE,
                    PRESUPUESTO = club.PRESUPUESTO,
                    ESCUDO = club.ESCUDO
                };

                db.CLUBES.Add(newItem);
                db.SaveChanges();
            }
        }

        public List<CLUBES> GetAllClubes()
        {
            using (LaLigaEntities db = new LaLigaEntities())
            {
                var q =
                    from c in db.CLUBES
                    select c;

                return (q.Count() != 0) ? q.AsEnumerable().ToList() : null;
            }
        }

        public CLUBES GetClub(int equipo)
        {
            using (LaLigaEntities db = new LaLigaEntities())
            {
                var q =
                    from c in db.CLUBES
                    where c.ID_CLUB == equipo
                    select c;

                return (q.Count() != 0) ? q.AsEnumerable().First() : null;
            }
        }

        public CLUBES GetClub(string nombreEquipo)
        {
            using (LaLigaEntities db = new LaLigaEntities())
            {
                var q =
                    from c in db.CLUBES
                    where c.NOMBRE == nombreEquipo
                    select c;

                return (q.Count() != 0) ? q.AsEnumerable().First() : null;
            }
        }

        public void EditClub(int equipo, int presupuesto)
        {
            using (LaLigaEntities db = new LaLigaEntities())
            {
                var q =
                    from c in db.CLUBES
                    where c.ID_CLUB == equipo
                    select c;

                if (q.Count() != 0)
                {
                    q.First().PRESUPUESTO = presupuesto;
                    db.SaveChanges();
                }
            }
        }

        public void DeleteClub(int equipo)
        {
            using (LaLigaEntities db = new LaLigaEntities())
            {
                var q =
                    from c in db.CLUBES
                    where c.ID_CLUB == equipo
                    select c;

                if (q.Count() != 0)
                {
                    db.CLUBES.Remove(q.First());
                    db.SaveChanges();
                }
            }
        }
        #endregion

        #region Posiciones
        public POSICIONES GetPosition(int posicion)
        {
            using (LaLigaEntities db = new LaLigaEntities())
            {
                var q =
                    from c in db.POSICIONES
                    where c.ID_POSICION == posicion
                    select c;

                return (q.Count() != 0) ? q.AsEnumerable().First() : null;
            }
        }
        #endregion

    }
}