using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.models.Models
{
    public class Equipo
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        public int Orden { get; set; }
        public int Ganados { get; set; }
        public int Perdidos { get; set; }
        public double Promedio { get; set; }

        public string Imagen { get; set; }
    }
}
