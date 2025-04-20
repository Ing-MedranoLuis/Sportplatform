using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto.data.IRepositories;
using Proyecto.models.Models;
using Proyecto.models.VM;
using Proyecto.roles;

namespace Proyecto.Areas.Admin.Controllers
{
    [Area("Admin")/*,Authorize(Roles =Roles.Admin)*/]
    
    public class JugadorController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IWebHostEnvironment _host;

        public JugadorController(IUnitOfWork unitOfWork, IWebHostEnvironment host)
        {
            _unitOfWork = unitOfWork;
            _host = host;
        }
        public IActionResult Index()
        {

            var jugador = _unitOfWork.JugadorRepository.GetAll(IncludeProperty:"equipo");
            return View(jugador);
        }
        [HttpGet]
        public IActionResult upsert(int? id)
        {
            if (id != null && id != 0)
            {

                var editJugador = new JugadoresVM()
                {

                    jugador = _unitOfWork.JugadorRepository.Get(u => u.Id == id,IncludeProperty:"equipo"),
                    equipoLista = _unitOfWork.EquipoRepository.equipoLista()
                };
                return View(editJugador);
            }
            var JugadorVm = new JugadoresVM()
            {
                jugador = new models.Models.Jugador(),
                equipoLista = _unitOfWork.EquipoRepository.equipoLista()
            };
            return View(JugadorVm);
        }

        [HttpPost]
        public IActionResult upsert(JugadoresVM VM ,IFormFile? file)
        {

            var oldImage = VM.jugador;
            if (VM.jugador.Id != 0)
            {
                oldImage = _unitOfWork.JugadorRepository.Get(u => u.Id == VM.jugador.Id);
            }
           
            var wwwroot = _host.WebRootPath;

            if (ModelState.IsValid)
            {
                if(file != null)  
                {

                    string filename = Guid.NewGuid().ToString() + Path.GetExtension(file.FileName);
                    var ruta=Path.Combine(wwwroot,@"imagenes\jugador");

                    if (!String.IsNullOrEmpty(oldImage.Imagen))
                    {
                        var rutaImagen = Path.Combine(wwwroot, oldImage.Imagen.TrimStart('\\'));
                        if (System.IO.File.Exists(rutaImagen))
                        {
                            System.IO.File.Delete(rutaImagen);
                        }
                    }

                    using (var stream =new FileStream(Path.Combine( ruta,filename), FileMode.Create))
                    {

                        file.CopyTo(stream);
                    }
                    VM.jugador.Imagen = @"\imagenes\jugador\" + filename;
                }
                else
                {
                    VM.jugador.Imagen = oldImage.Imagen;
                }
                if (VM.jugador.Id == 0)
                {
                 
                    _unitOfWork.JugadorRepository.Add(VM.jugador);
                    _unitOfWork.save();
                }
                else
                {
                   
                    _unitOfWork.JugadorRepository.Update(VM.jugador);
                    _unitOfWork.save();

                }


                return RedirectToAction(nameof(Index));


            }
           
            return View();
        }
        [HttpDelete, ActionName("DELETE")]
        public IActionResult Delete(int? id)
        {
            var wwwRoot = _host.WebRootPath;
            if (id == null)
            {
                return Json(new { success = false, message = "No pudimos eliminar el jugador" });
            }
            var objetoDB = _unitOfWork.JugadorRepository.Get(U => U.Id == id);
          
            if (!String.IsNullOrEmpty(objetoDB.Imagen))
            {
                var ruta = Path.Combine(wwwRoot, objetoDB.Imagen.TrimStart('\\'));
                if (System.IO.File.Exists(ruta))
                {
                    System.IO.File.Delete(ruta);
                }
            }
            _unitOfWork.JugadorRepository.Remove(objetoDB);
            _unitOfWork.save();
            return Json(new { success = true, message = "Jugador eliminado" });
        }    
        #region


        [HttpGet]
        public IActionResult GetAll() {

            return Json(new { data = _unitOfWork.JugadorRepository.GetAll(IncludeProperty: "equipo") });
        }
        #endregion
    }
}
