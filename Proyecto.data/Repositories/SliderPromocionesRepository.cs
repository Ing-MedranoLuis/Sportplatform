using Microsoft.AspNetCore.Mvc.Rendering;
using Proyecto.data.IRepositories;
using Proyecto.Data;
using Proyecto.models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto.data.Repositories
{
    public class SliderPromocionesRepository:Repository<SliderPromociones>,ISliderPromocionesRepository
    {
        private readonly ApplicationDbContext _db;
        public SliderPromocionesRepository(ApplicationDbContext db):base(db)
        {
            _db = db;
        }

        public IEnumerable<SelectListItem> sliderLista()
        {
            var lista = _db.sliderPromociones.Select(u => new SelectListItem()
            {
                Text = u.Name,
                Value = u.Id.ToString()
            });
            return lista;
        }

        public void Update(SliderPromociones slider)
        {
            var slide = _db.sliderPromociones.FirstOrDefault(u => u.Id == slider.Id);
            slide.Name = slider.Name;
            slide.Image = slider.Image;
            slide.fecha = slider.fecha;
            slide.Active = slider.Active;

            _db.SaveChanges();
        }
    }
}
