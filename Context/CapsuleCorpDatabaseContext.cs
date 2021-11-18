using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CapsuleCorp.Models;

namespace CapsuleCorp.Context
{
    public class CapsuleCorpDatabaseContext : DbContext
    {
        public CapsuleCorpDatabaseContext(DbContextOptions<CapsuleCorpDatabaseContext> options) : base(options)
        {
        }
        public DbSet<Administrador> Administradores { get; set; }
        public DbSet<Paciente> Pacientes { get; set; }
        public DbSet<Turno> Turnos { get; set; }
    }
}