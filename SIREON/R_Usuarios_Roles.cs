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
    
    public partial class R_Usuarios_Roles
    {
        public int ID_RolUsuario { get; set; }
        public int ID_Usuario { get; set; }
        public int ID_Rol { get; set; }
    
        public virtual Role Role { get; set; }
        public virtual Usuario Usuario { get; set; }
    }
}
