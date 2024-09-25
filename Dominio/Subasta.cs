using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Subasta : IValidable
    {
        private List<Oferta> _listaOferta = new List<Oferta>(); //Agregue el "new List<Oferta>();" para crear el objeto y cuando apliquemos add, pueda agregar las ofertas
        private Publicacion _publicacion;

        public Subasta(List<Oferta> listaOferta, Publicacion publicacion)
        {
            _listaOferta = listaOferta;
            _publicacion = publicacion;
        }

        public void Validar()
        {
            if (_listaOferta.Count == 0) throw new Exception ("La subasta debe tener al menos una oferta.");
            if (_publicacion.Estado != TipoEstado.ABIERTA) throw new Exception ("La subasta solo puede realizarse en una publicación abierta.");
            ValidarSaldo();


        }

        public void ValidarSaldo()
        {
            foreach (Oferta oferta in _listaOferta)
            {
                if (oferta.Cliente.Saldo < oferta.Monto) throw new Exception ($"El Cliente no tiene saldo suficiente para cubrir la oferta.");
                
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
