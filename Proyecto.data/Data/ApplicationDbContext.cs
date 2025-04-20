using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Proyecto.models.Models;

namespace Proyecto.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Equipo> equipos { get; set; }
        public DbSet<Jugador> jugadores { get; set; }
        public DbSet<Numero> numeros { get; set; }
        public DbSet<Noticias> noticias { get; set; }
        public DbSet<SliderPromociones> sliderPromociones { get; set; }
        public DbSet<ApplicationUser> applicationsUser { get; set; }
    }
}