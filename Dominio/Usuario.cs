using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public abstract class Usuario : IValidable
    {
        protected int _id;
        protected static int s_ultId = 1;
        protected string _nombre;
        protected string _apellido;
        protected string _email;
        protected string _contrasena;

        public Usuario(string nombre, string apellido, string email, string contrasena)
        {
            _id = s_ultId;
             s_ultId++;
            _nombre = nombre;
            _apellido = apellido;
            _email = email;
            _contrasena = contrasena;
        }

        public void Validar()
        {
            if (string.IsNullOrEmpty(_nombre)) throw new Exception("El nombre no puede ser vacio");
            if (string.IsNullOrEmpty(_nombre)) throw new Exception("El apellido no puede ser vacio");
            if (string.IsNullOrEmpty(_email)) throw new Exception("El email no puede ser vacio");
            if (string.IsNullOrEmpty(_email)) throw new Exception("La contraseña no puede ser vacia");
        }

        public override string ToString()
        {
            return $"nombre: {_nombre} - apellido: {_apellido} email: {_email}";
        }
    }
}
