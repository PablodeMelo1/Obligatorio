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

        public void Validar(){
            ValidarSaldo();
            ValidarEstado();
            ValidarOfertasValidas();
        }
        #region Metodos que no son requeridos para la primer entrega. (Cerrar subasta y distintas validaciones)
        public void ValidarSaldo()
        {
            foreach (OfertaSubasta of in _listaOferta)
            {
                if (of.OfertasRealizadas.Cliente.Saldo < of.OfertasRealizadas.Monto)
                    throw new Exception($"El Cliente {of.OfertasRealizadas.Cliente.Id} no tiene saldo suficiente para cubrir la oferta.");

                // Descontar el saldo si es suficiente
                of.OfertasRealizadas.Cliente.DescontarSaldo(of.OfertasRealizadas.Monto);
            }
        }

        public void ValidarOfertasValidas()
        {
            // Validar que la lista de ofertas no esté vacía
            if (_listaOferta.Count == 0)
            {
                throw new Exception("La subasta debe tener al menos una oferta.");
            }

            foreach (OfertaSubasta of in _listaOferta)
            {
                // Validar que la oferta no sea nula
                if (of == null)
                {
                    throw new Exception("La oferta no puede ser nula.");
                }

                // Validar que el monto de la oferta sea positivo
                if (of.OfertasRealizadas.Monto <= 0)
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
                if (of.OfertasRealizadas.Cliente.Saldo >= of.OfertasRealizadas.Monto)
                {
                    if (mejorOferta == null || of.OfertasRealizadas.Monto > mejorOferta.OfertasRealizadas.Monto)
                    {
                        mejorOferta = of;
                    }
                }
            }

            // Validar si hay una oferta válida con saldo suficiente
            if (mejorOferta != null)
            {
                mejorOferta.OfertasRealizadas.Cliente.DescontarSaldo(mejorOferta.OfertasRealizadas.Monto);
                _comprador = mejorOferta.OfertasRealizadas.Cliente;
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
            double ultMonto = 0;
            if (_listaOferta.Count > 0)
            {
                ultMonto = _listaOferta[(_listaOferta.Count - 1)].OfertasRealizadas.Monto;
            }
            if (ofe == null) throw new Exception("La oferta no puede ser nula");
            if (ofe.OfertasRealizadas.Monto < ultMonto) throw new Exception("La oferta no puede ser menor a la oferta anterior");
            ofe.Validar();
            _listaOferta.Add(ofe);
        }

    }
}
