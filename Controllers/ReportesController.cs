using Microsoft.AspNetCore.Mvc;

namespace Integradora.Controllers
{
    public class ReportesController : Controller
    {
        // GET: /Reportes/Index (Pantalla principal con Mapa y Menú)
        [HttpGet]
        public IActionResult Index()
        {
            // Puedes enviar datos del usuario logueado usando ViewData o Session
            ViewData["UsuarioNombre"] = "Julio Manuel";
            ViewData["UsuarioRol"] = "Inspector Ambiental";
            ViewData["PuntosTotales"] = 120;

            return View();
        }
    }
}