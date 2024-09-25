using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Sistema
    {
        private List<Venta> _listaVentas = new List<Venta>();
        private List<Subasta> _listaSubastas = new List<Subasta>();
        private List<Articulo> _listaArticulos = new List<Articulo>();
        //private List<Cliente> _listaCliente = new List<Cliente>();

        #region properties
        public List<Venta> Ventas
        {
            get
            {
                return _listaVentas;
            }
        }
        public List<Articulo> Articulos
        {
            get
            {
                return _listaArticulos;
            }
        }
        public List<Subasta> Subastas
        {
            get
            {
                return _listaSubastas;
            }
        }
        #endregion

        public Sistema()
        {
            PrecargarArticulos();
        }

        private void PrecargarArticulos()
        {
            AltaArticulo(new Articulo("Televisor", "Electronica", 450.99));
            AltaArticulo(new Articulo("Laptop", "Electronica", 999.99));
            AltaArticulo(new Articulo("Smartphone", "Electronica", 699.50));
            AltaArticulo(new Articulo("Cámara", "Fotografia", 299.99));
            AltaArticulo(new Articulo("Refrigerador", "Electrodomesticos", 1200.00));
            AltaArticulo(new Articulo("Lavadora", "Electrodomesticos", 800.75));
            AltaArticulo(new Articulo("Microondas", "Cocina", 150.99));
            AltaArticulo(new Articulo("Aspiradora", "Hogar", 250.00));
            AltaArticulo(new Articulo("Monitor", "Computacion", 199.99));
            AltaArticulo(new Articulo("Impresora", "Oficina", 120.50));
        }

        public void AltaArticulo(Articulo articulo)
        {
            if (articulo == null) throw new Exception("El articulo no puede ser vacio o nulo");
            articulo.Validar();
            _listaArticulos.Add(articulo);
        }        

        public List<Articulo> ArticulosPorCategoria(string categoria)
        {
            List<Articulo> buscados = new List<Articulo>();
            foreach (Articulo a in _listaArticulos)
            {
                if(a.Categoria.ToLower() == categoria.ToLower()) buscados.Add(a);
            }
            return buscados;

        }

    }
}
