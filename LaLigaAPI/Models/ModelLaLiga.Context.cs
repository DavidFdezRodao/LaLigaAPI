//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace LaLigaAPI.Models
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class LaLigaEntities : DbContext
    {
        public LaLigaEntities()
            : base("name=LaLigaEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<CLUBES> CLUBES { get; set; }
        public virtual DbSet<ENTRENADORES> ENTRENADORES { get; set; }
        public virtual DbSet<POSICIONES> POSICIONES { get; set; }
        public virtual DbSet<JUGADORES> JUGADORES { get; set; }
    }
}
