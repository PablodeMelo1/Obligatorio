using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Publicacion : IValidable
    {
        protected int _id;
        protected static int s_ultId = 1;
        protected string _nombre;
        protected TipoEstado _estado;
        protected DateTime _fechaPublicacion;
        protected List<Articulo> _listaArticulos = new List<Articulo>();
        protected Cliente? _comprador;
        protected Usuario _usuarioCierre; //modifique el usuario cierre colocando Usuario en vez de Administrador
        protected DateTime? _fechaCierre;

        public Publicacion(string nombre, TipoEstado estado, DateTime fechaPublicacion, Cliente? comprador, Usuario usuarioCierre, DateTime? fechaCierre)
        {
            _id = s_ultId;
            s_ultId++;
            _nombre = nombre;
            _estado = estado;
            _fechaPublicacion = fechaPublicacion;
            _comprador = comprador;
            _usuarioCierre = usuarioCierre;
            _fechaCierre = fechaCierre;
        }

        public TipoEstado Estado
        {
            get { return _estado; }
        }
        public DateTime FechaPublicacion
        {
            get { return _fechaPublicacion; }
        }
        public int Id
        {
            get { return _id; }
        }
        public string Nombre
        {
            get { return _nombre; }
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(_nombre)) throw new Exception("El nombre no puede ser vacio");
            if (_estado != TipoEstado.ABIERTA) throw new Exception("La publicación no se encuentra en estado ABIERTA.");
        }
        
        public void ValidarArticulosPublicacion()
        {
            if(_listaArticulos.Count == 0) throw new Exception("La publicación debe contener al menos un artículo.");
        }


        public override string ToString()
        { 
            return $"ID: {_id} - Nombre: {_nombre} - Estado: {_estado} - Fecha de Publicación: {_fechaPublicacion}";         

        }

        public void RegistrarArticulo(Articulo a)
        {
            if (a == null) throw new Exception("El articulo no puede ser nulo");
            a.Validar();
            _listaArticulos.Add(a);
        }

    }
}
