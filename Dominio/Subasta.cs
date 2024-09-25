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
        private List<OfertaSubasta> _listaOferta = new List<OfertaSubasta>(); //Agregue el "new List<OfertaSubasta>();" para crear el objeto y cuando apliquemos add, pueda agregar las ofertas
        private Publicacion _publicacion;

        public Subasta(string nombre, TipoEstado estado, DateTime fechaPublicacion, List<Articulo> listaArticulos, Cliente comprador, Administrador usuarioCierre, DateTime fechaCierre):base(nombre, estado, fechaPublicacion, listaArticulos, comprador, usuarioCierre, fechaCierre)
        {
            
        }

        public void Validar()
        {
            if (_listaOferta.Count == 0) throw new Exception ("La subasta debe tener al menos una oferta.");
            if (_publicacion.Estado != TipoEstado.ABIERTA) throw new Exception ("La subasta solo puede realizarse en una publicación abierta.");
            ValidarSaldo();


        }

        public void ValidarSaldo()
        {
            foreach (OfertaSubasta oferta in _listaOferta)
            {
                if (oferta.Ofertas.Cliente.Saldo < oferta.Ofertas.Monto) throw new Exception ($"El Cliente no tiene saldo suficiente para cubrir la oferta.");
                
            }
        }

        //public void ListarOfertas(List<Oferta> listaOfertas)
        //{
        //    //Falta armar este metodo de listar las ofertas
        //}


        public override string ToString()
        {
            return $"Ofertas: - Publicacion {_publicacion}"; //{ListarOfertas()}
        }
    }
}
