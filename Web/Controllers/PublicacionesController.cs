using Dominio;
using Microsoft.AspNetCore.Mvc;

namespace Web.Controllers
{
    public class PublicacionesController : Controller
    {
        Sistema miSistema = Sistema.Instancia;

        [HttpGet]
        public IActionResult ListadoSubastas()
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "administrador")
            {
                return View("NoAutorizado");
            }

            List<Publicacion> subastas = new List<Publicacion>();

            // Filtrar las publicaciones que son de tipo Subasta
            foreach (Publicacion publicacion in miSistema.Publicacion)
            {
                if (publicacion is Subasta subasta)
                {
                    subastas.Add(subasta);
                }
            }

            ViewBag.ListadoSubastas = subastas;
            return View();
        }

        [HttpGet]
        public IActionResult CambiarOfertaSubasta(int idSubasta)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "administrador")
            {
                return View("NoAutorizado");
            }

            ViewBag.idSubasta = idSubasta;
            return View();
        }


        //FALTA TERMINAR ESTE METODO PARA MODIFICAR EL IMPORTE DE LAS OFERTAS QUE SE LE HACE A UNA SUBASTA
        [HttpPost]
        public IActionResult CambiarOfertaSubasta(int idSubasta, double nuevaOferta)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "administrador")
            {
                return View("NoAutorizado");
            }

            try
            {                
                if (nuevaOferta < 0) throw new Exception("El saldo no puede ser negativo");

                miSistema.ModificarSaldoDeCliente(idSubasta, nuevaOferta);
                ViewBag.Exito = $"Se modificó el saldo del cliente {idSubasta} - Nuevo saldo: ${nuevaOferta}";
            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
            }
            return View();

        }
    }
}
