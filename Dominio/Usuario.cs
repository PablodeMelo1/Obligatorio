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
        private int _id;
        private int s_ultId = 1;
        private string _nombre;
        private string _apellido;
        private string _email;
        private string _contrasena;

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
