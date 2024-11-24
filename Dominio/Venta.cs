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

        public override bool EsSubasta()
        {
            return false;
        }

        public string TieneOfertaRelampago() //retornar si la venta tiene oferta relampago
        {
            if (_ofertaRelampago) return "Si";
            else return "No";
        }
        
        public override void FinalizarPublicacion(Usuario usuario)
        {
            if (usuario.EsCliente())
            {
                Cliente cliente = (Cliente)usuario;  // Hacer el cast directo, ya que sabemos que es un Cliente
                double precioFinal = CalculoUltimaOfertaPrecioFinal();

                if (cliente.Saldo < precioFinal)
                {
                    throw new Exception("Saldo insuficiente para completar la compra.");
                }

                cliente.DescontarSaldo(precioFinal);
                _comprador = cliente;
                _estado = TipoEstado.CERRADA;
                _usuarioCierre = cliente;
                _fechaCierre = DateTime.Now;
            }
            else
            {
                throw new Exception("Solo un cliente puede finalizar esta venta.");
            }
        }

        public override double CalculoUltimaOfertaPrecioFinal()
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
