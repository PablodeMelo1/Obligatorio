using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class ClienteController : Controller
    {
        Sistema miSistema = Sistema.Instancia;

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
