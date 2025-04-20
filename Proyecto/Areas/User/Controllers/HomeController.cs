using Microsoft.AspNetCore.Mvc;
using Proyecto.data.IRepositories;
using Proyecto.models;
using Proyecto.models.VM;
using System.Diagnostics;

namespace Proyecto.Areas.User.Controllers
{
    [Area("User")]
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unit)
        {
            _unitOfWork= unit;
        }

        public IActionResult Index()
        {
            var homeVM = new HomeVM()
            {

                Jugador = _unitOfWork.JugadorRepository.GetAll(IncludeProperty: "equipo"),
                Numero = _unitOfWork.NumeroRepository.GetAll(IncludeProperty: "jugador"),
                noticias = _unitOfWork.NoticiasRepository.GetAll(),
                sliderPromociones = _unitOfWork.SliderPromocionesRepository.GetAll()
            };
            return View(homeVM);
        }
        [HttpGet]
        public IActionResult Portafolio()
        {
            return View();
        }
        public IActionResult Estadisticas(string? parameter)
        {


            var homeVM = new HomeVM()
            {

                Jugador = _unitOfWork.JugadorRepository.GetAll(IncludeProperty: "equipo"),
                Numero = _unitOfWork.NumeroRepository.GetAll(IncludeProperty: "jugador"),
                noticias = _unitOfWork.NoticiasRepository.GetAll()
            };
            if (!String.IsNullOrEmpty(parameter))
            {
                homeVM.Numero = _unitOfWork.NumeroRepository.getByname(parameter);


            }

            return View(homeVM);
        }
        [HttpGet]
        public IActionResult DetallesNoticias(int id)
        {
            var noticia = _unitOfWork.NoticiasRepository.Get(u => u.Id == id);
            return View(noticia);
        }
            [HttpGet]
        public IActionResult PerfilJugador(int Id)
        {if (Id == 0)
            {
                   
                return View(NoContent());
            }
          
            var jugador = _unitOfWork.NumeroRepository.Get(u => u.Id == Id, IncludeProperty: "jugador");
           
           return View(jugador);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}