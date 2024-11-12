using Dominio;
using Microsoft.AspNetCore.Mvc;
using static System.Runtime.InteropServices.JavaScript.JSType;

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
        public IActionResult ListadoPublicaciones()
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "cliente")
            {
                return View("NoAutorizado");
            }

            List<Publicacion> publicaciones = new List<Publicacion>();
            publicaciones = miSistema.Publicacion;
            ViewBag.ListadoPublicaciones = publicaciones;
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
        
        public IActionResult comprarOfertar(int idP)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "cliente")
            {
                return View("NoAutorizado");
            }

            try
            {
                Usuario u = miSistema.ObtenerUsuarioPorEmail(HttpContext.Session.GetString("email"));
                Publicacion p = miSistema.ObtenerPublicacionPorId(idP);
                if (p is Subasta s)
                {
                    OfertaSubasta oferta = new OfertaSubasta(new DateTime(2024, 10, 5), miSistema.ObtenerUsuarioPorEmail(HttpContext.Session.GetString("email")), 5000);
                    s.RegistrarOferta(oferta);
                }
                else if (p is Venta v)
                {
                    p.FinalizarPublicacion(u);
                }
                TempData["Exito"] = $"Publicacion comprada con exito!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            
            return RedirectToAction("ListadoPublicaciones");
        }



        ////FALTA TERMINAR ESTE METODO PARA MODIFICAR EL IMPORTE DE LAS OFERTAS QUE SE LE HACE A UNA SUBASTA
        //[HttpPost]
        //public IActionResult CambiarOfertaSubasta(int idSubasta, double nuevaOferta)
        //{
        //    if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "administrador")
        //    {
        //        return View("NoAutorizado");
        //    }

        //    try
        //    {                
        //        if (nuevaOferta < 0) throw new Exception("El saldo no puede ser negativo");

        //        miSistema.ModificarSaldoDeCliente(idSubasta, nuevaOferta);
        //        ViewBag.Exito = $"Se modificó el saldo del cliente {idSubasta} - Nuevo saldo: ${nuevaOferta}";
        //    }
        //    catch (Exception ex)
        //    {
        //        ViewBag.Error = ex.Message;
        //    }
        //    return View();

        //}
    }
}
