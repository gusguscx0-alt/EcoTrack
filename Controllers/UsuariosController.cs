using Microsoft.AspNetCore.Mvc;
using Integradora.Models;

namespace Integradora.Controllers
{
    public class UsuariosController : Controller
    {
        Usuarios modelo = new Usuarios();

        // 1. Carga la pantalla principal de ECOTRACK (Login y Registro)
        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }

        // 2. Método que procesa el Iniciar Sesión
        [HttpPost]
        public IActionResult Login(string correo, string password_hash)
        {
            Usuarios usuarioEncontrado = modelo.IniciarSesion(correo, password_hash);

            if (usuarioEncontrado != null)
            {
                // Si los datos son correctos, va a la vista del mapa
                return RedirectToAction("Index", "Reportes");
            }

            ViewBag.Error = "Correo o contraseña incorrectos. Intenta nuevamente.";
            return View("Index");
        }

        // 3. Método que procesa el Crear Cuenta (este es el que te faltaba y causaba el 404)
        [HttpPost]
        public IActionResult Registro(Usuarios nuevoUsuario)
        {
            // Deja la llamada a tu método tal cual como lo tienes nombrado:
            int resultado = modelo.AddUsuario(nuevoUsuario);

            if (resultado > 0)
            {
                ViewBag.Mensaje = "¡Tu cuenta ha sido creada exitosamente! Ya puedes iniciar sesión.";
            }
            else if (resultado == -1)
            {
                // Si entra aquí, significa que el correo ya existía
                ViewBag.Error = "Ese correo ya está registrado. Intenta con otro o inicia sesión.";
            }
            else
            {
                ViewBag.Error = "Ocurrió un error al registrar la cuenta. Verifica los datos.";
            }

            return View("Index");
        }
    }
}