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
       
        public Venta(bool ofertaRelampago, string nombre, TipoEstado estado, DateTime fechaPublicacion, Cliente? comprador, Cliente? usuarioCierre, DateTime? fechaCierre) :base(nombre, estado, fechaPublicacion, comprador, usuarioCierre, fechaCierre)
        {
            _ofertaRelampago = ofertaRelampago;
        }

        public string TieneOfertaRelampago() //retornar si la venta tien oferta relampago
        {
            if (_ofertaRelampago) return "Si";
            else return "No";
        }    



    }
}
