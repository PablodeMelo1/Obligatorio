using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Cliente : Usuario
    {
        private double _saldo;

        public Cliente(string nombre, string apellido, string email, string contrasena, double saldo):base(nombre, apellido, email, contrasena)
        {
            _saldo = saldo;
        }       
        public int Id
        {
            get { return _id; }
        }
        public string Nombre
        {
            get { return _nombre; }
        }
        public string Apellido
        {
            get { return _apellido; }
        }

        public double Saldo
        {
            get { return _saldo; }
            set { _saldo = value; }
        }

        public override void Validar()
        {
            
        }
       
        
        //metodo para modificar el saldo del cliente
        public void ModificarSaldo(double nuevoSaldo)
        {
            if (nuevoSaldo <= 0) throw new Exception("El saldo debe ser mayor a 0");
            _saldo = nuevoSaldo;
        }

        public override string ToString()
        {
            return $"id: {_id} - Nombre: {_nombre} {_apellido} - Email: {_email} -  saldo: ${_saldo}";
        }

        public override bool Equals(object? obj)
        {
            Cliente c = obj as Cliente;
            return c != null && this._id.Equals(c._id);
        }

        public override string Rol()
        {
            return "cliente";
        }

        public override bool EsCliente()
        {
            return true;
        }

        public void DescontarSaldo(double monto)
        {
            if (_saldo >= monto)
            {
                Saldo -= monto;
            } else
            {
                throw new Exception("Saldo insuficiente");
            }
        }


    }
}
