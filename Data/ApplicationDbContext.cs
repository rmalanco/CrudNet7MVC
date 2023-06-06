using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CrudNet7MVC.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudNet7MVC.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        // DbSet: Representa una colección de entidades en el contexto que se pueden que son los modelos
        public DbSet<Contacto> Contacto { get; set; }
    }
}