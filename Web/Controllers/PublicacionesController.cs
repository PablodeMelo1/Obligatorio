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

            List<Publicacion> subastas = miSistema.SubastasOrdenadasPorFecha();

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
                TempData["Exito"] = $"Subasta cerrada con exito!";
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
            // Agregar Validacion      

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
                if (p is Venta v)
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
