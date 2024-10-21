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

        public override void FinalizarPublicacion(Usuario usuario)
        {
            if (usuario is Cliente c && c.Equals(_comprador))
            {
                // Verificar saldo disponible
                if (c.Saldo >= CalcularPrecioFinal())
                {
                    c.DescontarSaldo(CalcularPrecioFinal());
                    _estado = TipoEstado.CERRADA;
                    _usuarioCierre = usuario;
                    _fechaCierre = DateTime.Now;
                }
                else
                {
                    throw new Exception("Saldo insuficiente para completar la compra.");
                }
            }
            else
            {
                throw new Exception("Solo el comprador puede finalizar esta venta.");
            }
        }

        private double CalcularPrecioFinal()
        {
            double precioTotal = 0;

            // Sumar los precios de los artículos manualmente
            foreach (var articulo in _listaArticulos)
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
