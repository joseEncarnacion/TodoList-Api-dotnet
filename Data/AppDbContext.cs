using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using todo.api.Models;

namespace todo.api.Data
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Tarea> Tareas { get; set; }
        public DbSet<Estado> Estados { get; set; }
        public DbSet<Prioridad> Prioridades { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Tarea>()
            .HasOne(x => x.Usuario)
            .WithMany(u => u.Tareas)
            .HasForeignKey(t => t.UsuarioID);

            modelBuilder.Entity<Tarea>()
            .HasOne(t => t.Estado)
            .WithMany(d => d.Tareas)
            .HasForeignKey(t => t.EstadoID);

             modelBuilder.Entity<Tarea>()
            .HasOne(t => t.Prioridad)
            .WithMany(p => p.Tareas) 
            .HasForeignKey(t => t.PrioridadID);

        }

        
    }
}