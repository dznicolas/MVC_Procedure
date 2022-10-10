using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Projeto_MVC.Models
{
    public class Contexto : DbContext
    {
        public Contexto(DbContextOptions<Contexto> options) : base(options)
        {
            Database.EnsureCreated();
        }

        public DbSet<Cliente> Cliente { get; set; }

    }
}