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

        public void Validar()        {
            
            ValidarSaldo();
            ValidarEstado();
            ValidarOfertasValidas();
        }

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
        //Validacion agregada
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
        //Validacion agregada
        public void ValidarEstado()
        {
            if (_estado != TipoEstado.ABIERTA) throw new Exception("La subasta no está en estado ABIERTA.");
        }
        //Validacion agregada
        public void CerrarSubasta()
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
        //Validacion agregada
        public void FinalizarSubasta(Administrador admin)
        {
            if (admin == null) throw new Exception("Solo un administrador puede cerrar la subasta.");

            CerrarSubasta(); // Cierra la subasta si las condiciones son correctas.
        }


        public void RegistrarOferta(OfertaSubasta ofe)
        {
            if (ofe == null) throw new Exception("La oferta no puede ser nula");
            ofe.Validar();
            _listaOferta.Add(ofe);
        }


        //public override string ToString()
        //{
        //    string retorno =   $"Nombre {_nombre} - Estado {_estado}" +
        //        $" - Fecha {_fechaPublicacion} - Comprador? {_comprador} - Admin Cierre: {_usuarioCierre} - Fecha Cierre: {_fechaCierre}";

        //    if (_listaArticulos.Count == 0)
        //    {
        //        retorno += $"\n NO TIENE ARTICULOS ASOCIADOS";
        //    }
        //    else
        //    {
        //        foreach (Articulo a in _listaArticulos)
        //        {
        //            retorno += $"\n {a.ToString()}";
        //        }
        //    }

        //    if (_listaOferta.Count == 0)
        //    {
        //        retorno += $"\n NO TIENE OFERTAS";
        //    }
        //    else
        //    {
        //        foreach (OfertaSubasta o in _listaOferta)
        //        {
        //            retorno += $"\n {o.ToString()}";
        //        }
        //    }
        //    return retorno;

        //}
    }
}
