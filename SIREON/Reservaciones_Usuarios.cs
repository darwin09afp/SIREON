//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace SIREON
{
    using System;
    using System.Collections.Generic;
    
    public partial class Reservaciones_Usuarios
    {
        public int Id { get; set; }
        public string IdAspNetUser { get; set; }
        public int IdReservacion { get; set; }
        public string NombreInvitado { get; set; }
        public string CedulaInvitado { get; set; }
        public string Estatus { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        public virtual Reservacione Reservacione { get; set; }
        public virtual Reservacione Reservacione1 { get; set; }
    }
}
