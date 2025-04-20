using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto.data.IRepositories;
using Proyecto.models.Models;
using Proyecto.roles;

namespace Proyecto.Areas.Admin.Controllers
{
    [Area("Admin")/*, Authorize(Roles = Roles.Admin)*/]
    public class NoticiaController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _host;
        public NoticiaController(IUnitOfWork unitOfWork, IWebHostEnvironment host)
        {
            _unitOfWork = unitOfWork;
            _host = host;
        }
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult upsert(int? Id)
        {
            if (Id != null && Id != 0)
            {
                var noticias = _unitOfWork.NoticiasRepository.Get(u => u.Id == Id);
                return View(noticias);
            }
            Noticias data = new Noticias();


            return View(data);
        }

        [HttpPost]
        public IActionResult Upsert(Noticias nti, IFormFile? file)
        {
         
            var rootPath = _host.WebRootPath;

            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var path = Path.Combine(rootPath, @"imagenes\noticias");
                    using (var fileStream = new FileStream(Path.Combine(path, filename), FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    nti.Imagen = @"\imagenes\noticias\" + filename;
                }
             
                if (nti.Id != 0)
                {
                    var oldNoticia = _unitOfWork.NoticiasRepository.Get(u => u.Id == nti.Id);
                    nti.Fecha = oldNoticia.Fecha;
                   

                    if (!String.IsNullOrEmpty(oldNoticia.Imagen)) {
                        var imagen = Path.Combine(rootPath, oldNoticia.Imagen.TrimStart('\\'));
                        if (System.IO.File.Exists(imagen))
                        {

                            System.IO.File.Delete(imagen);
                        }
                    }
                    nti.Imagen = oldNoticia.Imagen;
                    _unitOfWork.NoticiasRepository.UpdateNoticias(nti);
                    _unitOfWork.save();
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    nti.Fecha = DateTime.Now;
                    _unitOfWork.NoticiasRepository.Add(nti);
                    _unitOfWork.save();
                    return RedirectToAction(nameof(Index));
                }


            }
            return View(nti);

        }



        #region
        [HttpDelete,ActionName("Delete")]
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return Json(new { success = false, message = "No pudimos eliminar el registro" });
            }

            var delete = _unitOfWork.NoticiasRepository.Get(u => u.Id == id);
            _unitOfWork.NoticiasRepository.Remove(delete);
            _unitOfWork.save();
            return Json(new { success = true, message = "Registro Eliminado" });


        }
        [HttpGet]
        public IActionResult GetAll() {

            return Json(new { data = _unitOfWork.NoticiasRepository.GetAll() });

        }

        #endregion
    }
}
