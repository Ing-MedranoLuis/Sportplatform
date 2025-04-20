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
    public class ApllicationUserRepository : Repository<ApplicationUser>,IApplicationRepository
    {
        private readonly ApplicationDbContext _db;
        public ApllicationUserRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }



        public IEnumerable<SelectListItem> listaUser()
        {
            var lista = _db.applicationsUser.Select(u => new SelectListItem()
            {
                Text = u.Nombre,
                Value = u.Id
            });
            return lista;
        }
    }
}
