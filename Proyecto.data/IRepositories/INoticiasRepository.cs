using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Proyecto.data.IRepositories
{
    public interface INoticiasRepository:IRepository<Noticias>
    {
        IEnumerable<SelectListItem> noticiaLista();

        void UpdateNoticias(Noticias noticias);
    }
}
