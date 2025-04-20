using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.models.Models
{
    public class SliderPromociones
    {
        [Key]
        public int Id { get; set; }
        [Required(ErrorMessage ="El nombre es necesario")]
        public string Name { get; set; }
        public bool Active { get; set; }
        public DateTime fecha { get; set; }
      
        public string Image { get; set; }
    }
}
