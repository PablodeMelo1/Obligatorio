using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class UsuariosController : Controller
    {
        Sistema miSistema = Sistema.Instancia;

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(string email, string contrasena)
        {
            try
            {
                if (string.IsNullOrEmpty(email)) throw new Exception("Debe ingresar un email");
                if (string.IsNullOrEmpty(contrasena)) throw new Exception("Debe ingresar una contraseña");
                Usuario usuario = miSistema.Login(email, contrasena);
                if (usuario == null) throw new Exception("Email o contraseña incorrectas");

                
                //Variables para el inicio de sesion
                HttpContext.Session.SetString("email", email); //IDENTIFICA EL USUARIO
                HttpContext.Session.SetString("rol", usuario.Rol()); //PERMITE SABER EL ROL QUE CUMPLE

                return RedirectToAction("Index", "Home");

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View();
            }
            
        }

        public IActionResult Logout()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Login");
        }

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

                ViewBag.Exito = $"Cliente {nombre} {apellido} registrado con éxito!";
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
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "cliente")
            {
                return View("NoAutorizado");
            }
            
            return View();
            
        }

        [HttpPost]
        public IActionResult ModificarSaldoCliente(double nuevoSaldo)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "cliente")
            {
                return View("NoAutorizado");
            }

            try
            {

                Cliente c = miSistema.ObtenerClientePorId(miSistema.ObtenerUsuarioPorEmail(HttpContext.Session.GetString("email")).Id);
                //c.ModificarSaldo(nuevoSaldo);
                // falta try and catch
                if (nuevoSaldo < 0) throw new Exception("El monto no puede ser negativo");

                miSistema.ModificarSaldoDeCliente(c.Id, nuevoSaldo);
                ViewBag.Exito = $"Se modificó el saldo del cliente {c.Nombre} - Nuevo saldo: ${c.Saldo}";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();
        }
    }
}
