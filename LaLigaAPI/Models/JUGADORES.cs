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
    using System.Collections.Generic;
    
    public partial class JUGADORES
    {
        public int ID_JUGADOR { get; set; }
        public string NOMBRE { get; set; }
        public System.DateTime F_NACIMIENTO { get; set; }
        public Nullable<int> POSICION { get; set; }
        public Nullable<int> SALARIO { get; set; }
        public Nullable<int> CLUB { get; set; }
        public Nullable<bool> CANTERANO { get; set; }
    }
}