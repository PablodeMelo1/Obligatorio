using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    internal class OfertaSubasta : IValidable
    {
        private Oferta _oferta;
        private DateTime _fecha;

        public OfertaSubasta(Oferta oferta, DateTime fecha)
        {
            _oferta = oferta;
            _fecha = fecha;
        }

        public void Validar()
        {
            if (_oferta == null) throw new Exception("La oferta no puede ser nula");
        }

        

        
    }
}
