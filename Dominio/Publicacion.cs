using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Publicacion : IValidable
    {
        private int _id;
        private int s_ultId = 1;
        private string _nombre;
        private TipoEstado _estado;
        private DateTime _fechaPublicacion;
        private List<Articulo> _listaArticulos = new List<Articulo>(); //List<Articulo> _listaArticulos?
        private Cliente _comprador;
        private Usuario _usuarioCierre;
        private DateTime _fechaCierre;

        public Publicacion(string nombre, TipoEstado estado, DateTime fechaPublicacion, List<Articulo> listaArticulos, Cliente comprador, Usuario usuarioCierre, DateTime fechaCierre)
        {
            _id = s_ultId;
             s_ultId++;
            _nombre = nombre;
            _estado = estado;
            _fechaPublicacion = fechaPublicacion;
            _listaArticulos = listaArticulos;
            _comprador = comprador;
            _usuarioCierre = usuarioCierre;
            _fechaCierre = fechaCierre;
        }

        public TipoEstado Estado
        {
            get { return _estado; }            
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(_nombre)) throw new Exception("El nombre no puede ser vacio");
        }

        // Falta listar los articulos con este metodo
        //public void ListarArticulos()
        //{
        
            
        //}
        public override string ToString()
        {
            return $"nombre: {_nombre} estado: {_estado} - fecha: {_fechaPublicacion} - Comprador: {_comprador} - " +
                $"usuarioCierre {_usuarioCierre} fecha cierre: {_fechaCierre}"; 

        }
    }
}
