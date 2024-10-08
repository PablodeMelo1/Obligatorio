﻿using Dominio.Interfaces;
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
        public void DescontarSaldo(double monto) // metodo no requerido para la primer entrega.
        {
            if (monto > _saldo)
            {
                throw new Exception("Saldo insuficiente.");
            }
            else
            {
                _saldo -= monto;
            }
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
    }
}
