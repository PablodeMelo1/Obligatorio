using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ObligatorioController : Controller
    {
        private Sistema miSistema = Sistema.Instancia;

        [HttpGet]
        public IActionResult RegistrarCliente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult RegistrarCliente(string nombre, string apellido, string email, string contrasena)
        {
            try
            {
                if (string.IsNullOrEmpty(nombre)) throw new Exception("El campo nombre no puede ser vacío");
                if (string.IsNullOrEmpty(apellido)) throw new Exception("El campo apellido no puede ser vacío");
                if (string.IsNullOrEmpty(email)) throw new Exception("El campo email no puede ser vacío");
                if (string.IsNullOrEmpty(contrasena)) throw new Exception("El campo contraseña no puede ser vacío");

                Usuario u = new Cliente(nombre, apellido, email, contrasena, 0);
                miSistema.AltaUsuario(u);

                ViewBag.Exito = $"Cliente {nombre} {apellido} dado de alta con éxito!";
            }
            catch (Exception ex) 
            {
                ViewBag.Error = ex.Message;
                ViewBag.Nombre = nombre;
                ViewBag.Apellido = apellido;
                ViewBag.Email = email;
                ViewBag.Contrasena = contrasena;
            }
            return View();
        }
    }
}
