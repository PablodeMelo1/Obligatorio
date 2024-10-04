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

        public void Validar()
        {
            if (_listaOferta.Count == 0) throw new Exception ("La subasta debe tener al menos una oferta.");
            if (_estado != TipoEstado.ABIERTA) throw new Exception ("La subasta solo puede realizarse en una publicación abierta.");
            ValidarSaldo();


        }

        public void ValidarSaldo()
        {
            foreach (OfertaSubasta oferta in _listaOferta)
            {
                if (oferta.Ofertas.Cliente.Saldo < oferta.Ofertas.Monto) throw new Exception ($"El Cliente no tiene saldo suficiente para cubrir la oferta.");
                
            }
        }

        public void RegistrarOferta(OfertaSubasta ofe)
        {
            if (ofe == null) throw new Exception("La oferta no puede ser nula");
            ofe.Validar();
            _listaOferta.Add(ofe);
        }


        public override string ToString()
        {
            string retorno =   $"nombre {_nombre} - estado {_estado}" +
                $" - fecha {_fechaPublicacion} - comprador si tiene: {_comprador} - Admin cierre: {_usuarioCierre} fecha cierre: {_fechaCierre}";

            if (_listaArticulos.Count == 0)
            {
                retorno += $"\n NO TIENE ARTICULOS ASOCIADOS";
            }
            else
            {
                foreach (Articulo a in _listaArticulos)
                {
                    retorno += $"\n {a.ToString()}";
                }
            }

            if (_listaOferta.Count == 0)
            {
                retorno += $"\n NO TIENE OFERTAS";
            }
            else
            {
                foreach (OfertaSubasta o in _listaOferta)
                {
                    retorno += $"\n {o.ToString()}";
                }
            }
            return retorno;

        }
    }
}
