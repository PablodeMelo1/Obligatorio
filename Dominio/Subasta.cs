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


        //FALTA ACCEDER AL MONTO DE OfertaSubasta
        public void ModificarOfertaSubasta(double nuevaOferta)
        {
            if (nuevaOferta <= 0) throw new Exception("El saldo debe ser mayor a 0");
            OfertaSubasta.Monto = nuevaOferta;
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
            // Validar si el usuario es un Administrador
            if (!(usuario is Administrador administrador))
            {
                throw new Exception("Solo un administrador puede cerrar la subasta.");
            }

            // Validar si hay una oferta válida con saldo suficiente
            OfertaSubasta mejorOferta = ObtenerPrimeraOfertaConSaldo();
            if (mejorOferta == null)
            {
                throw new Exception("No hay una oferta válida con saldo suficiente.");
            }

            // Si pasa las validaciones, proceder con el cierre de la subasta
            mejorOferta.Cliente.DescontarSaldo(mejorOferta.Monto);
            _comprador = mejorOferta.Cliente; // Asignar el comprador
            _estado = TipoEstado.CERRADA;
            _usuarioCierre = administrador;
            _fechaCierre = DateTime.Now;
        }

        private OfertaSubasta ObtenerPrimeraOfertaConSaldo()
        {

            foreach (OfertaSubasta oferta in _listaOferta)
            {
                if (oferta.Cliente.Saldo >= oferta.Monto)
                {
                    return oferta; // Devolvemos la primera oferta válida
                }
            }

            return null; // Si no hay ninguna oferta válida
        }

    }
}
