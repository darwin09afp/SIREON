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
    
    public partial class R_Salas_Usuarios
    {
        public int ID_RSalas { get; set; }
        public int ID_Sala { get; set; }
        public string IdAspNetUsers { get; set; }
    
        public virtual Sala Sala { get; set; }
        public virtual AspNetUser AspNetUser { get; set; }
    }
}
