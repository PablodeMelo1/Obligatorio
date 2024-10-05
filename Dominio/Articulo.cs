using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Articulo : IValidable
    {
        private int _id;
        private static int s_ultId = 1;
        private string _nombre;
        private string _categoria;
        private double _precioVenta;

        public Articulo(string nombre, string categoria, double precioVenta)
        {
            _id = s_ultId;
            s_ultId++;
            _nombre = nombre;
            _categoria = categoria;
            _precioVenta = precioVenta;
        }
        public double PrecioVenta
        {
            get { return _precioVenta; }
        }
        public int Id
        {
            get { return _id; }
        }
        public string Nombre
        {
            get { return _nombre; }
        }

        public string Categoria
        {
            get { return _categoria; }
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(_nombre)) throw new Exception("Por favor! Ingrese el nombre del articulo!");
            if (string.IsNullOrEmpty(_categoria)) throw new Exception("Por favor! Ingrese una categoria!");
            if (_precioVenta <= 0) throw new Exception("El precio debe ser mayor a 0");
        }

        public override string ToString()
        {
            return $"Articulo: {_nombre} - Categoria: {_categoria} - Precio de Venta: {_precioVenta}";
        }

        //public override bool Equals(object? obj)
        //{
        //    Articulo a = obj as Articulo;
        //    return a != null && this._nombre == a._nombre;
        //}

    }
}
