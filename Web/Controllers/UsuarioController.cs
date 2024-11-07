using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class UsuariosController : Controller
    {
        Sistema miSistema = Sistema.Instancia;

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

        [HttpGet]
        public IActionResult ModificarSaldoCliente()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ModificarSaldoCliente(int idUsuario, double nuevoSaldo)
        {
            try
            {
                if (idUsuario < 0) throw new Exception("El id del cliente no es valido");
                if (nuevoSaldo < 0) throw new Exception("El saldo no puede ser negativo");

                miSistema.ModificarSaldoDeCliente(idUsuario, nuevoSaldo);
                ViewBag.Exito = $"Se modificó el saldo del cliente {idUsuario} - Nuevo saldo: ${nuevoSaldo}";
            }
            catch(Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }
    }
}
