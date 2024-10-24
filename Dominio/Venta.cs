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
       
        public Venta(bool ofertaRelampago, string nombre, TipoEstado estado, DateTime fechaPublicacion, Usuario comprador, Usuario usuarioCierre, DateTime? fechaCierre) :base(nombre, estado, fechaPublicacion, comprador, usuarioCierre, fechaCierre)
        {
            _ofertaRelampago = ofertaRelampago;
        }

        public string TieneOfertaRelampago() //retornar si la venta tien oferta relampago
        {
            if (_ofertaRelampago) return "Si";
            else return "No";
        }

        public override void FinalizarPublicacion(Usuario usuario)
        {
            // Validar si el usuario es un Cliente
            if (!(usuario is Cliente cliente))
            {
                throw new Exception("Solo un cliente puede finalizar esta venta.");
            }

            // Validar si el cliente tiene saldo suficiente
            double precioFinal = CalcularPrecioFinal();
            if (cliente.Saldo < precioFinal)
            {
                throw new Exception("Saldo insuficiente para completar la compra.");
            }

            // Si pasa las validaciones, proceder con el cierre de la venta
            cliente.DescontarSaldo(precioFinal);
            _comprador = cliente;  // Asignar el comprador al cerrar la venta
            _estado = TipoEstado.CERRADA;
            _usuarioCierre = cliente;
            _fechaCierre = DateTime.Now;
        }

        private double CalcularPrecioFinal()
        {
            double precioTotal = 0;

            
            foreach (Articulo articulo in _listaArticulos) 
            {
                precioTotal += articulo.PrecioVenta;
            }

            // Aplicar descuento del 20% si hay una oferta relámpago
            if (_ofertaRelampago)
            {
                precioTotal *= 0.8;
            }

            return precioTotal;
        }


    }



}
