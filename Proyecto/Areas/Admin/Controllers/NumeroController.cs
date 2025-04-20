using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Proyecto.data.IRepositories;
using Proyecto.models.Models;
using Proyecto.models.VM;
using Proyecto.roles;
using System.Linq.Expressions;

namespace Proyecto.Areas.Admin.Controllers
{
    [Area("Admin")/*, Authorize(Roles = Roles.Admin)*/]
    public class NumeroController : Controller
    {

        private readonly IUnitOfWork _unitOfWork;
        public NumeroController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        [HttpGet]
        public IActionResult Index()
        { var allNumeros = _unitOfWork.NumeroRepository.GetAll(IncludeProperty: "jugador");
            return View(allNumeros);
        }

        [HttpGet]
        public IActionResult upsert(int? id)
        {

            ViewData["listaEquipo"] = _unitOfWork.EquipoRepository.equipoLista();
            if (id != null && id != 0)
            {
                var allnumeros = new NumeJugadorVM()
                {
                    numero = _unitOfWork.NumeroRepository.Get(u => u.Id == id, IncludeProperty: "jugador"),
                    listajugador = _unitOfWork.JugadorRepository.jugadorLista(),
                    

                };

                return View(allnumeros);
            }
            var numeros = new NumeJugadorVM()
            {
                numero = new models.Models.Numero(),
                listajugador = _unitOfWork.JugadorRepository.jugadorLista()

            };
            return View(numeros);

        }

        [HttpPost]
        public IActionResult upsert(NumeJugadorVM VM)
        {
            if (ModelState.IsValid)
            {

                double hits = VM.numero.H1 + VM.numero.H2 + VM.numero.H3 + VM.numero.H4;

                double turnosOfficiales = hits + VM.numero.OUT + VM.numero.KK;
                VM.numero.TURNOSOFICIALES =Convert.ToInt32(turnosOfficiales);
                double avg = hits / VM.numero.TURNOSOFICIALES;
                var convert = Math.Truncate(avg * 1000);
                VM.numero.Avg = convert;
                VM.numero.TURNOS = VM.numero.TURNOSOFICIALES + VM.numero.BB;
     

                   
               
                if (VM.numero.Id != 0)
                {

                    _unitOfWork.NumeroRepository.Update(VM.numero);
                    _unitOfWork.save();
                    return RedirectToAction(nameof(Index));

                }
                else
                {
                    _unitOfWork.NumeroRepository.Add(VM.numero);
                    _unitOfWork.save();
                    return RedirectToAction(nameof(Index));


                }

            }
            else
            {
                return View(VM.numero);
            }



        }
        [HttpGet]
        public JsonResult GetJugadorJson(int equiId)
        {

            return Json(_unitOfWork.JugadorRepository.GetAll(u=>u.equipo.Id== equiId, IncludeProperty: "equipo"));
        }


        #region


        [HttpDelete, ActionName("Delete")]
        public IActionResult Delete(int? id)
        {


            if (id == null)
            {
                return Json(new { success = false, message = "No se pudo eliminar registro" });
            }

            var numero = _unitOfWork.NumeroRepository.Get(u => u.Id == id);
            _unitOfWork.NumeroRepository.Remove(numero);
            _unitOfWork.save();
            return Json(new { success = true, message = "Registro eliminado" });
        }

        [HttpGet]
        public IActionResult GetAll() {



            return Json(new { data = _unitOfWork.NumeroRepository.GetAll(IncludeProperty: "jugador") });
        }
        #endregion
    }
}
