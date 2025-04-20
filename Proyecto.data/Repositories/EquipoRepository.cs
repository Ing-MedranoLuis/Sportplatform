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
    public class EquipoRepository:Repository<Equipo>,IEquipoRepository
    {
        private readonly ApplicationDbContext _db;
        public EquipoRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> equipoLista()
        {
            var lista = _db.equipos.Select(u => new SelectListItem()
            {
                Text = u.Nombre,
                Value = u.Id.ToString()
            });
            return lista;
        }

        public void Update(Equipo equipo)
        {
            var equi = _db.equipos.FirstOrDefault(u => u.Id == equipo.Id);
            equi.Nombre = equipo.Nombre;
            equi.Orden = equipo.Orden;
            equi.Imagen=equipo.Imagen;
            _db.SaveChanges();
        }
    }
}
