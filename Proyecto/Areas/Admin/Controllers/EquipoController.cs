using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto.data.IRepositories;
using Proyecto.models.Models;
using Proyecto.roles;

namespace Proyecto.Areas.Admin.Controllers
{
    [Area("Admin")/*,Authorize(Roles = Roles.Admin)*/]
    public class EquipoController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _host;
        public EquipoController(IUnitOfWork unitOfWork, IWebHostEnvironment webHostEnvironment)
        {
            _unitOfWork = unitOfWork;
            _host = webHostEnvironment;
        }
        
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Upsert(int? id)
        {
            if(id!=null && id != 0)
            {

             var equipo =  _unitOfWork.EquipoRepository.Get(u => u.Id == id);
                return View(equipo);
            }
           var equi=new Equipo();

            return View(equi);
        }
        [HttpPost]
        public IActionResult Upsert(Equipo equipo,IFormFile? files)
        {
            var oldImage=equipo;
            if (equipo.Id != 0)
            {
                oldImage = _unitOfWork.EquipoRepository.Get(u => u.Id == equipo.Id);
            }
            var rootPath = _host.WebRootPath;
            if (equipo.Nombre.Length<3)
            {
                ModelState.AddModelError("nombre", "Debe ser mayor que tres caracteres");
            }
            if (ModelState.IsValid)
            {
            

                if (files != null)
                {
                    var fileName = Guid.NewGuid().ToString() + Path.GetExtension(files.FileName);
                    var equipoPath = Path.Combine(rootPath, @"imagenes\equipos");


                    if (oldImage.Imagen != null)
                    {

                        var ruta = Path.Combine(rootPath, oldImage.Imagen.TrimStart('\\'));


                        if (System.IO.File.Exists(ruta))
                        {
                            System.IO.File.Delete(ruta);
                        }
                    }
                  
                    using (var fileStream = new FileStream(Path.Combine(equipoPath, fileName), FileMode.Create))
                    {
                        files.CopyTo(fileStream);

                    }
                    equipo.Imagen = @"\imagenes\equipos\" + fileName;
                }
                else
                {
                    equipo.Imagen = oldImage.Imagen;
                }

                if (equipo.Id == 0)
                {

                    TempData["success"] = "Equipo agregado correctamente";
                _unitOfWork.EquipoRepository.Add(equipo);
                _unitOfWork.save();

                }
                else
                {
                    TempData["success"] = "Equipo actualizado correctamente";
                    _unitOfWork.EquipoRepository.Update(equipo);
                    _unitOfWork.save();

                }
                return RedirectToAction(nameof(Index));
            }
            return View(equipo);




        }
        
        public IActionResult Delete(int Id)
        {
            var rootPath = _host.WebRootPath;
            var getEntity = _unitOfWork.EquipoRepository.Get(u => u.Id == Id);
            if (getEntity == null)
            {

                return Json(new { success = false, message = "No se pudo borrar" });
            }
            else {
                if (!String.IsNullOrEmpty(getEntity.Imagen)) 
                {
                    var ruta = Path.Combine(rootPath, getEntity.Imagen.TrimStart('\\'));
                    if (System.IO.File.Exists(ruta))
                    {

                        System.IO.File.Delete(ruta);
                    }
                }
                _unitOfWork.EquipoRepository.Remove(getEntity);
                _unitOfWork.save();

                return Json(new { success = false, message = "Borrado exitoso" });

            }
        }





        #region
        [HttpGet]
        public IActionResult GetAll()
        {
            return Json(new{data= _unitOfWork.EquipoRepository.GetAll() });
        }
        #endregion
    }
}
