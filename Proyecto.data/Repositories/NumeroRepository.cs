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
    public class NumeroRepository : Repository<Numero>,INumeroRepository
    {
        private readonly ApplicationDbContext _db;
        public NumeroRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public IEnumerable<Numero> getByname(string parameter)
        {
            return _db.numeros.Where(u=>u.jugador.Nombre.Contains(parameter)).ToList();
        }

        public void Update(Numero numero)
        {
            throw new NotImplementedException();
        }
    }
}
