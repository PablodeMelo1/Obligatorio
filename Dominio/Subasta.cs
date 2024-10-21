using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Subasta : Publicacion
    {
        private List<OfertaSubasta>? _listaOferta = new List<OfertaSubasta>(); //Agregue el "new List<OfertaSubasta>();" para crear el objeto y cuando apliquemos add, pueda agregar las ofertas

        public Subasta(string nombre, TipoEstado estado, DateTime fechaPublicacion, Usuario comprador, Usuario usuarioCierre, DateTime? fechaCierre):base(nombre, estado, fechaPublicacion, comprador, usuarioCierre, fechaCierre)
        {
            
        }
        public List<OfertaSubasta> Ofertas
        {
            get { return _listaOferta; }
        }
        public override void Validar(){            
            ValidarEstado();            
        }     
              
     
        public void ValidarEstado()
        {
            if (_estado != TipoEstado.ABIERTA) throw new Exception("La subasta no está en estado ABIERTA.");
        }
    

        public void RegistrarOferta(OfertaSubasta ofe)
        {

            foreach (OfertaSubasta o in _listaOferta) //comprobamos que un cliente realize unicamente una oferta
            {
                if (o.Cliente.Equals(ofe.Cliente)) throw new Exception("El cliente ya realizo una oferta en esta publicacion.");
            }

            //comprobamos que un cliente realize una oferta con el monto mas alto al anterior
            double ultMonto = 0; 
            if (_listaOferta.Count > 0) ultMonto = _listaOferta[(_listaOferta.Count - 1)].Monto;
            if (ofe == null) throw new Exception("La oferta no puede ser nula");
            if (ofe.Monto < ultMonto) throw new Exception("La oferta no puede ser menor a la oferta anterior");
            ofe.Validar();
            _listaOferta.Add(ofe);
        }

        public override void FinalizarPublicacion(Usuario usuario)
        {
            if (usuario is Administrador)
            {
                OfertaSubasta mejorOferta = ObtenerMejorOferta();
                if (mejorOferta != null && mejorOferta.Cliente.Saldo >= mejorOferta.Monto)
                {
                    mejorOferta.Cliente.DescontarSaldo(mejorOferta.Monto);
                    _comprador = mejorOferta.Cliente;
                    _estado = TipoEstado.CERRADA;
                    _usuarioCierre = usuario;
                    _fechaCierre = DateTime.Now;
                }
                else
                {
                    throw new Exception("No hay una oferta válida con saldo suficiente.");
                }
            }
            else
            {
                throw new Exception("Solo un administrador puede cerrar la subasta.");
            }
        }

      private OfertaSubasta ObtenerMejorOferta()
{
    OfertaSubasta mejorOferta = null;

    foreach (OfertaSubasta oferta in _listaOferta)
    {
        if (mejorOferta == null || oferta.Monto > mejorOferta.Monto)
        {
            mejorOferta = oferta;
        }
    }

    return mejorOferta;
}

    }
}
