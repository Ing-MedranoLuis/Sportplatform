using Microsoft.AspNetCore.Mvc;
using Proyecto.data.IRepositories;
using Proyecto.models.Models;

namespace Proyecto.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SliderPromocion : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _host;
        public SliderPromocion(IUnitOfWork unitOfWork, IWebHostEnvironment host)
        {
            _unitOfWork = unitOfWork;
            _host = host;
        }

        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult upsert(int? id)
        {
            if (id == null)
            {
                //editar
                var slider = _unitOfWork.SliderPromocionesRepository.Get(u => u.Id == id);
                return View(slider);


            }
            else
            {
                //new Slider

                return View();
            }
        }
        [HttpPost]
        public IActionResult upsert(SliderPromociones promociones, IFormFile? files)
        {
            var oldImage = promociones;
            if (promociones.Id != 0)
            {
                oldImage = _unitOfWork.SliderPromocionesRepository.Get(u => u.Id == promociones.Id);
            }
            var rootPath = _host.WebRootPath;
            if (promociones.Name.Length < 3)
            {
                ModelState.AddModelError("nombre", "Debe ser mayor que tres caracteres");
            }
           if(promociones.Active)
            {


                if (files != null)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(files.FileName);
                    var equipoPath = Path.Combine(rootPath, @"imagenes\slider");


                    if (oldImage.Image != null)
                    {

                        var ruta = Path.Combine(rootPath, oldImage.Image.TrimStart('\\'));


                        if (System.IO.File.Exists(ruta))
                        {
                            System.IO.File.Delete(ruta);
                        }
                    }

                    using (var fileStream = new FileStream(Path.Combine(equipoPath, fileName), FileMode.Create))
                    {
                        files.CopyTo(fileStream);

                    }
                    promociones.Image = @"\imagenes\slider\" + fileName;
                    promociones.fecha = DateTime.Now;
                }
                else
                {
                    promociones.Image = oldImage.Image;
                    promociones.fecha = oldImage.fecha;
                }

                if (promociones.Id == 0)
                {

                    TempData["success"] = "Slider agregado correctamente";
                    _unitOfWork.SliderPromocionesRepository.Add(promociones);
                    _unitOfWork.save();

                }
                else
                {
                    TempData["success"] = "Slider actualizado correctamente";
                    _unitOfWork.SliderPromocionesRepository.Update(promociones);
                    _unitOfWork.save();

                }
                return RedirectToAction(nameof(Index));
            }
            return View(promociones);


        }

        #region

        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new { data = _unitOfWork.SliderPromocionesRepository.GetAll() });

        }
        #endregion
    }
}

