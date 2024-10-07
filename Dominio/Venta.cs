using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Runtime.InteropServices.JavaScript.JSType;

namespace Dominio
{
    public class Venta : Publicacion
    {
        private bool _ofertaRelampago;
        //modifique el usuario cierre y le puse el objeto Cliente
        public Venta(bool ofertaRelampago, string nombre, TipoEstado estado, DateTime fechaPublicacion, Cliente? comprador, Cliente? usuarioCierre, DateTime? fechaCierre) :base(nombre, estado, fechaPublicacion, comprador, usuarioCierre, fechaCierre)
        {
            _ofertaRelampago = ofertaRelampago;
        }

        public string TieneOfertaRelampago()
        {
            if (_ofertaRelampago) return "Si";
            else return "No";
        }
        //public override string ToString()
        //{   string retorno = $"Tiene oferta relampago? {TieneOfertaRelampago()} - Nombre: {_nombre} - Estado: {_estado}" +
        //        $" - Fecha: {_fechaPublicacion} - Comprador? {_comprador} - Admin Cierre: {_usuarioCierre} Fecha Cierre: {_fechaCierre}";

        //    if (_listaArticulos.Count == 0)
        //    {
        //        retorno += $"\n NO TIENE ARTICULOS";
        //    } else 
        //    {
        //        foreach (Articulo a in _listaArticulos)
        //        {
        //            retorno += $"\n {a.ToString()}";
        //        }
        //    }
        //    return retorno;
                    
        //}         

        public void CambioDeColor(string mensaje, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(mensaje);
            Console.ForegroundColor = ConsoleColor.Gray;
        }


    }
}
