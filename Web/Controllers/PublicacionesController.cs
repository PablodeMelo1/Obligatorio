using Dominio;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;
using System;
using System.Net.Http;
using System.Runtime.Intrinsics.Arm;
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
            // Obtengo las subastas ordenadas. El método SubastasOrdenadasPorFecha() y devuelve una lista de Subasta.
            // Por lo tanto, no es necesario volver a verificar si un elemento es una Subasta recorriendo en un bucle foreach.
            List<Subasta> subastas = miSistema.SubastasOrdenadasPorFecha();

            // Pasar las subastas a la vista
            ViewBag.ListadoSubastas = subastas;
            return View();
        }


        public IActionResult CerrarSubasta(int idSubasta)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "administrador")
            {
                return View("NoAutorizado");
            }

            try
            {
                Usuario u = miSistema.ObtenerUsuarioPorEmail(HttpContext.Session.GetString("email"));
                Subasta s = miSistema.ObtenerPublicacionPorId(idSubasta) as Subasta;
                s.FinalizarPublicacion(u);
                Publicacion p = miSistema.ObtenerPublicacionPorId(idSubasta);
                if (p.Estado == TipoEstado.CANCELADA)
                {
                    TempData["Exito"] = $"No hay ofertas validas, CANCELADA con exito!";
                }
                else TempData["Exito"] = $"Subasta cerrada con exito!";

            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }

            return RedirectToAction("ListadoSubastas");
        }

        [HttpGet]
        public IActionResult ListadoPublicaciones()
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "cliente")
            {
                return View("NoAutorizado");
            }

            try
            {      

            List<Publicacion> publicaciones = new List<Publicacion>();
            publicaciones = miSistema.Publicacion;
            ViewBag.ListadoPublicaciones = publicaciones;
            return View();

            }
            catch (Exception ex)
            {
                ViewBag.Error = ex.Message;
                return View("Error");
            }
        }
        

        public IActionResult Comprar(int idP)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "cliente")
            {
                return View("NoAutorizado");
            }

            try
            {
                Usuario u = miSistema.ObtenerUsuarioPorEmail(HttpContext.Session.GetString("email"));
                Publicacion p = miSistema.ObtenerPublicacionPorId(idP);
                if (!p.EsSubasta())
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

        [HttpGet]
        public IActionResult CambiarOfertarSubasta(int idS)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "cliente")
            {
                return View("NoAutorizado");
            }
            Cliente c = miSistema.ObtenerUsuarioPorEmail(HttpContext.Session.GetString("email")) as Cliente;
            Subasta s = miSistema.ObtenerSubastaPorId(idS);
            if (s.ValidarUnicaOferta(c))
            {
                ViewBag.Valor = s.ObtenerOfertaDeCliente(c);
            }
            
            ViewBag.idSubasta = idS;
            return View();
        }

        [HttpPost]
        public IActionResult CambiarOfertarSubasta(int idS, double nuevaOferta)
        {
            if (HttpContext.Session.GetString("rol") == null || HttpContext.Session.GetString("rol") != "cliente")
            {
                return View("NoAutorizado");
            }

            try
            {
                Subasta s = miSistema.ObtenerSubastaPorId(idS);
                if (nuevaOferta < 0) throw new Exception("el saldo no puede ser negativo");
                OfertaSubasta oferta = new OfertaSubasta(DateTime.Now, miSistema.ObtenerUsuarioPorEmail(HttpContext.Session.GetString("email")), nuevaOferta);
                s.RegistrarOferta(oferta);
                TempData["Exito"] = $"Oferta realizada con exito!";
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
            }
            return RedirectToAction("ListadoPublicaciones");


        }
    }
}
