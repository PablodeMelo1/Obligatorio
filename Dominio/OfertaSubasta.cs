﻿using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class OfertaSubasta : IValidable
    {
        private int _id;
        private int s_ultId = 1;
        private Cliente _cliente;
        private double _monto;
        private DateTime _fecha;

        public OfertaSubasta(DateTime fecha, Cliente cliente, double monto)
        {
            _id = s_ultId;
            s_ultId++;
            _cliente = cliente;
            _monto = monto;
            _fecha = fecha;
        }

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
            if (_monto <= 0) throw new Exception("El monto no puede ser negativo o cero");
        }

        public override string ToString()
        {
            return $"id: {_id} cliente: {_cliente} - monto: {_monto} - fecha {_fecha}";
        }





    }
}
