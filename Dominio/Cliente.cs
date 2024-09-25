using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Cliente : IValidable
    {
        private double _saldo;
        private Usuario _usuario;

        public Cliente(double saldo, Usuario usuario)
        {
            _saldo = saldo;
            _usuario = usuario;
        }

        public double Saldo
        {
            get
            {
                return _saldo;
            }          
        }

        public void Validar()
        {
            if (_saldo < 0) throw new Exception("El saldo no puede ser negativo");
        }

        public override string ToString()
        {
            return $"saldo: {_saldo} {_usuario}";
        }
    }
}
