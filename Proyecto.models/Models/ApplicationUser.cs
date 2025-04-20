using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.models.Models
{
    public class ApplicationUser:IdentityUser
    {
        public string Nombre { get; set; }
        public string ciudad { get; set; }
        public string Apellido { get; set; }
        public string Provincia { get; set; }
        public string municipio { get; set; }

        public string telefono { get; set; }
        [NotMapped]
       public string role { get; set; }
    }
}
