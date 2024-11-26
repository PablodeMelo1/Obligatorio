using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Administrador : Usuario
    {
        public Administrador(string nombre, string apellido, string email, string contrasena):base(nombre, apellido, email, contrasena)
        {
            
        }
        public int Id
        {
            get { return _id; }
        }        

        public override string Rol()
        {
            return "administrador";
        }

        public override bool EsCliente()
        {
            return false;
        }
    }
}
