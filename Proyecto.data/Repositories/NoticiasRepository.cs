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
    public class NoticiasRepository:Repository<Noticias>,INoticiasRepository
    {
        private readonly ApplicationDbContext _db;
        public NoticiasRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

      

        public IEnumerable<SelectListItem> noticiaLista()
        {
            var lista = _db.noticias.Select(u => new SelectListItem()
            {
                Text = u.Titulo,
                Value = u.Id.ToString()
            });
            return lista;
        }

        public void UpdateNoticias(Noticias noticias)
        {
            var update = _db.noticias.FirstOrDefault(u => u.Id == noticias.Id);
            update.Titulo = noticias.Titulo;
            update.Descripcion = noticias.Descripcion;
            update.Fecha = DateTime.Now;
            update.Imagen = noticias.Imagen;
        }
    }
}
