using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ObligatorioController : Controller
    {
        private Sistema miSistema = Sistema.Instancia;
        public IActionResult RegistrarCliente()
        {
            return View();
        }

        public IActionResult ProcesarRegistroDeCliente(string nombre, string apellido, string email, string contrasena)
        {
            try
            {
                if (string.IsNullOrEmpty(nombre)) throw new Exception("El campo nombre no puede ser vacío");
                if (string.IsNullOrEmpty(apellido)) throw new Exception("El campo apellido no puede ser vacío");
                if (string.IsNullOrEmpty(email)) throw new Exception("El campo email no puede ser vacío");
                if (string.IsNullOrEmpty(contrasena)) throw new Exception("El campo contraseña no puede ser vacío");

                // Validación de formato de email
                if (!email.Contains("@") || !email.Contains(".")) throw new Exception("El formato del campo email no es válido");

                // Validación de la longitud de la contraseña 
                if (contrasena.Length < 8) throw new Exception("La contraseña debe tener al menos 8 caracteres");

                Usuario u = new Cliente(nombre, apellido, email, contrasena, 3000);
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
            return View("RegistrarCliente");
        }
    }
}
