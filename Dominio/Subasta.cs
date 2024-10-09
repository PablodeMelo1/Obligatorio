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

        public Subasta(string nombre, TipoEstado estado, DateTime fechaPublicacion, Cliente? comprador, Administrador? usuarioCierre, DateTime? fechaCierre):base(nombre, estado, fechaPublicacion, comprador, usuarioCierre, fechaCierre)
        {
            
        }
        public List<OfertaSubasta> Ofertas
        {
            get { return _listaOferta; }
        }
        public override void Validar(){
            ValidarSaldo();
            ValidarEstado();
            ValidarOfertasValidas();
        }
        #region Metodos que no son requeridos para la primer entrega. (Cerrar subasta y distintas validaciones)
        public void ValidarSaldo()
        {
            foreach (OfertaSubasta of in _listaOferta)
            {
                if (of.Cliente.Saldo < of.Monto)
                    throw new Exception($"El Cliente {of.Cliente.Id} no tiene saldo suficiente para cubrir la oferta.");

                // Descontar el saldo si es suficiente
                of.Cliente.DescontarSaldo(of.Monto);
            }
        }

        public void ValidarOfertasValidas()
        {
            foreach (OfertaSubasta of in _listaOferta)
            {
                // Validar que la oferta no sea nula
                if (of == null)
                {
                    throw new Exception("La oferta no puede ser nula.");
                }

                // Validar que el monto de la oferta sea positivo
                if (of.Monto <= 0)
                {
                    throw new Exception("La oferta debe tener un monto positivo.");
                }

            }
        }

        public void ValidarEstado()
        {
            if (_estado != TipoEstado.ABIERTA) throw new Exception("La subasta no está en estado ABIERTA.");
        }

        public void Cerrar()
        {
            ValidarEstado(); // Verificar que la subasta esté ABIERTA.

            OfertaSubasta mejorOferta = null;

            // Recorrer la lista de ofertas para encontrar la mejor oferta con saldo suficiente
            foreach (OfertaSubasta of in _listaOferta)
            {
                if (of.Cliente.Saldo >= of.Monto)
                {
                    if (mejorOferta == null || of.Monto > mejorOferta.Monto)
                    {
                        mejorOferta = of;
                    }
                }
            }

            // Validar si hay una oferta válida con saldo suficiente
            if (mejorOferta != null)
            {
                mejorOferta.Cliente.DescontarSaldo(mejorOferta.Monto);
                _comprador = mejorOferta.Cliente;
                _estado = TipoEstado.CERRADA;
                _fechaCierre = DateTime.Now;
            }
            else
            {
                throw new Exception("Ningún oferente tiene saldo suficiente para adjudicarse la subasta.");
            }
        }

        public void FinalizarSubasta(Administrador admin)
        {
            if (admin == null) throw new Exception("Solo un administrador puede cerrar la subasta.");

            Cerrar(); // Cierra la subasta si las condiciones son correctas.
        }
        #endregion

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

    }
}
