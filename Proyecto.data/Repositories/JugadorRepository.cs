using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.data.IRepositories;
using Proyecto.Data;
using Proyecto.models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.data.Repositories
{
    public class JugadorRepository : Repository<Jugador>,IJugadorRepository
    {
        private readonly ApplicationDbContext _db;
        public JugadorRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> jugadorLista()
        {
            
                var lista = _db.jugadores.Select(u => new SelectListItem()
                {
                    Text = u.Nombre,
                    Value = u.Id.ToString()
                });
                return lista;
            
        }

        public void Update(Jugador equipo)
        {
            var jug = _db.jugadores.FirstOrDefault(u => u.Id == equipo.Id);
            jug.Nombre= equipo.Nombre;
            jug.equipo = equipo.equipo;
            jug.Cedula = equipo.Cedula;
            jug.FechaNacimiento=equipo.FechaNacimiento;
            jug.Poscion = equipo.Poscion;
            jug.Imagen = equipo.Imagen;
            jug.IdEquipo = equipo.IdEquipo;

            _db.SaveChanges();

        }
    }
}
