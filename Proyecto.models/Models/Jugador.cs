using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.models.Models
{
    public class Jugador
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage = "El nombre es requerido")]
        public string Nombre { get; set; }
        [Required(ErrorMessage = "Posicion es requerido")]
        public int Poscion { get; set; }
        public int IdEquipo { get; set; }
        [ForeignKey("IdEquipo")]
        public Equipo equipo { get; set; }
      
        public string Cedula { get; set; }
        public string Imagen { get; set; }
        public DateTime FechaNacimiento { get; set; }
    }
}
