using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.models.Models
{
    public class Noticias
    {
        [Key]
        public int Id { get; set; }
        [DataType(DataType.ImageUrl)]
        public string Imagen { get; set; }
       
        public string Titulo { get; set; }
        public string Descripcion { get; set; }
        public bool video { get; set; }
        public DateTime Fecha { get; set; }
    }
}
