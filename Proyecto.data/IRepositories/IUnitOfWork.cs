using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.data.IRepositories
{
    public interface IUnitOfWork
    {IEquipoRepository EquipoRepository { get; }
        IJugadorRepository JugadorRepository { get; }   
        INumeroRepository NumeroRepository { get; } 
        INoticiasRepository NoticiasRepository { get; }
        IApplicationRepository ApplicationRepository { get; }
        ISliderPromocionesRepository SliderPromocionesRepository { get; }
        void save();
    }
}
