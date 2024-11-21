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
        public string Email
        {
            get { return _email; }
        }

        public string Contrasena
        {
            get { return _contrasena; }
        }
        public int Id
        {
            get { return _id; }
        }

        public double Saldo { get; internal set; }

        public abstract string Rol();
        public abstract bool EsCliente();

        public void Validar()
        {
            if (string.IsNullOrEmpty(_nombre) || string.IsNullOrEmpty(_apellido) ||
                string.IsNullOrEmpty(_email) || string.IsNullOrEmpty(_contrasena))
                throw new Exception("Los campos de nombre, apellido, contraseña y email son obligatorios.");

            // Validación de formato de email
            if (!_email.Contains("@") || !_email.Contains(".")) throw new Exception("El formato del campo email no es válido");

            // Validación de la longitud de la contraseña 
            if (_contrasena.Length < 8) throw new Exception("La contraseña debe tener al menos 8 caracteres");

        }        

        public override string ToString()
        {
            return $"nombre: {_nombre} - apellido: {_apellido} email: {_email}";
        }

        public void DescontarSaldo(double monto)
        {
                Saldo -= monto;
        }
    }
}
