using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.models.VM
{
    public class JugadoresVM
    {
        public Jugador jugador { get; set; }
        public IEnumerable<SelectListItem>  equipoLista { get; set; }

       
    }
}
