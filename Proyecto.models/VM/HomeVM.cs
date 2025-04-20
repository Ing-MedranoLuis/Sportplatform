using Proyecto.models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.models.VM
{
    public class HomeVM
    {
        public IEnumerable<Jugador> Jugador { get; set; }
        public IEnumerable< Numero> Numero { get; set; }
            public IEnumerable<Noticias> noticias { get; set; }
        public IEnumerable<SliderPromociones> sliderPromociones { get; set; }
    }
}
