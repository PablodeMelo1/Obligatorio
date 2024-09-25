using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Oferta : IValidable
    {
        private int _id;
        private int s_ultId = 1;
        private Cliente _cliente;
        private double _monto;
        private DateTime _fecha;

        public Oferta(Cliente cliente, double monto, DateTime fecha)
        {
            _id = s_ultId;
             s_ultId++;
            _cliente = cliente;
            _monto = monto;
            _fecha = fecha;
        }

        //Hice publico la variable monto y cliente para poder utilizarlo en el metodo ValidarSaldo() en la clase Subasta
        public Cliente Cliente
        {
            get { return _cliente; }            
        }

        public double Monto
        {
            get { return _monto; }            
        }

        public void Validar()
        {
            if (_monto <= 0) throw new Exception("El monto debe ser mayor a cero");
            if (_cliente.Saldo < _monto) throw new Exception("No tiene suficiente saldo");
        }

        public override string ToString()
        {
            return $"{_cliente} - monto: {_monto} - fecha: {_fecha}";
        }
    }
}
