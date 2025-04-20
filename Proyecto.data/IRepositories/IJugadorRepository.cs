using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Proyecto.data.IRepositories
{
    public interface IJugadorRepository : IRepository<Jugador>
    {
        IEnumerable<SelectListItem> jugadorLista();
        void Update(Jugador equipo);
    }
}
