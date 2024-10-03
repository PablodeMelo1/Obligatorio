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
        public double Saldo
        {
            get
            {
                return _saldo;
            }          
        }

        public override string ToString()
        {
            return $"saldo: {_saldo}";
        }
    }
}
