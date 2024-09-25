using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Venta : IValidable
    {
        private bool _ofertaRelampago;
        private Publicacion _publicacion;

        public Venta(bool ofertaRelampago, Publicacion publicacion)
        {

            // Agregue esta validacion ya que una venta necesita una publicacion asociada para funcionar.
            if (publicacion == null) throw new Exception("La publicación no puede ser nula.");
            

            _ofertaRelampago = ofertaRelampago;
            _publicacion = publicacion;
        }

        public void Validar()
        {
            //Creo que no hay que validar nada. El bool tiene valor por defeco y el Publicacion ya viene validado
            if (_publicacion.Estado != TipoEstado.ABIERTA) throw new Exception("La publicación debe estar en estado ABIERTA para realizar una venta.");
            
        }

        public string TieneOfertaRelampago()
        {
            if (_ofertaRelampago) return "Si";
            else return "No";
        }
        public override string ToString()
        {
            return $"Tiene oferta relampago? {TieneOfertaRelampago()} - Publicacion: {_publicacion} ";
        }

        
    }
}
