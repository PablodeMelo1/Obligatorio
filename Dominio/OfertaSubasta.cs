using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class OfertaSubasta : IValidable
    {
        private Oferta _oferta;
        private DateTime _fecha;

        public OfertaSubasta(Oferta oferta, DateTime fecha)
        {
            _oferta = oferta;
            _fecha = fecha;
        }

        public Oferta OfertasRealizadas
        {
            get
            {
                return _oferta;
            }
        }

        public void Validar()
        {
            if (_oferta == null) throw new Exception("La oferta no puede ser nula");
        }

        public override string ToString()
        {
            return $"oferta: {_oferta} - fecha {_fecha}";
        }





    }
}
