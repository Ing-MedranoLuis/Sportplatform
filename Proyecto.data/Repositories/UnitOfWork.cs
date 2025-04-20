using Proyecto.data.IRepositories;
using Proyecto.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.data.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
       private readonly ApplicationDbContext _context;
        public IEquipoRepository EquipoRepository { get; set; }

        public IJugadorRepository JugadorRepository { get; set; }

        public INumeroRepository NumeroRepository { get; set; }

        public INoticiasRepository NoticiasRepository { get; set; }

        public IApplicationRepository ApplicationRepository { get; set; }

        public ISliderPromocionesRepository SliderPromocionesRepository { get; set; }


        public UnitOfWork(ApplicationDbContext context)
        {
            _context= context;
            EquipoRepository = new EquipoRepository(_context);
            JugadorRepository=new JugadorRepository(_context);
            NumeroRepository=new NumeroRepository(_context);
            NoticiasRepository=new NoticiasRepository(_context);    
            ApplicationRepository=new ApllicationUserRepository(_context);
            SliderPromocionesRepository=new SliderPromocionesRepository(_context);
            
        }

  

        public void save()
        {
           _context.SaveChanges();
        }
    }
}
