using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.models.Models
{
    public class Numero
    {
        [Key]
        public int Id { get; set; }
        public int H1 { get; set; }
        public int H2 { get; set; }
        public int H3 { get; set; }
        public int H4 { get; set; }
        public int KK { get; set; }
        public int BB { get; set; }
        public int TURNOS { get; set; }
        public int TURNOSOFICIALES { get; set; }
        public int OUT { get; set; }
        public double Avg { get; set; }

        public int IdJugador { get; set; }
      
        [ForeignKey("IdJugador")]
        public Jugador jugador { get; set; }

    }
}

