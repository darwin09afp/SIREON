﻿//------------------------------------------------------------------------------
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
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    
    public partial class UniversidadEntities : DbContext
    {
        public UniversidadEntities()
            : base("name=UniversidadEntities")
        {
        }
    
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            throw new UnintentionalCodeFirstException();
        }
    
        public virtual DbSet<Cargo> Cargos { get; set; }
        public virtual DbSet<Carrera> Carreras { get; set; }
        public virtual DbSet<Directore> Directores { get; set; }
        public virtual DbSet<Empleado> Empleados { get; set; }
        public virtual DbSet<Entidad> Entidads { get; set; }
        public virtual DbSet<Escuela> Escuelas { get; set; }
        public virtual DbSet<Estudiante> Estudiantes { get; set; }
        public virtual DbSet<Facultade> Facultades { get; set; }
        public virtual DbSet<Login> Logins { get; set; }
        public virtual DbSet<Profesore> Profesores { get; set; }
        public virtual DbSet<sysdiagram> sysdiagrams { get; set; }
    }
}
