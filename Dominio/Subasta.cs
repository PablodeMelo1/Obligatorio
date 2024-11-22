using Dominio.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Subasta : Publicacion, IComparable<Subasta>
    {
        private List<OfertaSubasta>? _listaOferta = new List<OfertaSubasta>(); //Agregue el "new List<OfertaSubasta>();" para crear el objeto y cuando apliquemos add, pueda agregar las ofertas

        public Subasta(string nombre, TipoEstado estado, DateTime fechaPublicacion, Usuario comprador, Usuario usuarioCierre, DateTime? fechaCierre):base(nombre, estado, fechaPublicacion, comprador, usuarioCierre, fechaCierre)
        {
            
        }
        public List<OfertaSubasta> Ofertas
        {
            get { return _listaOferta; }
        }

        public override void Validar(){            
            //ValidarEstado();            
        }     
              
     
        //public void ValidarEstado()
        //{
        //    if (_estado != TipoEstado.ABIERTA) throw new Exception("La subasta no está en estado ABIERTA.");
        //}


        //FALTA ACCEDER AL MONTO DE OfertaSubasta
        //public void ModificarOfertaSubasta(double nuevaOferta)
        //{
        //    if (nuevaOferta <= 0) throw new Exception("El saldo debe ser mayor a 0");
        //    OfertaSubasta.Monto = nuevaOferta;
        //}
        public override double CalculoUltimaOfertaPrecioFinal()
        {
            if (_listaOferta.Count >= 1)
            {
                return _listaOferta[(_listaOferta.Count) - 1].Monto;
            }
            else return 0;
            
        }
        public bool ValidarUnicaOferta(Cliente cliente)
        {
            foreach (OfertaSubasta o in _listaOferta)
            {
                if (o.Cliente.Equals(cliente)) return true;
            }
            return false;
        }
        public void RegistrarOferta(OfertaSubasta ofe)

        {
            foreach (OfertaSubasta o in _listaOferta) //comprobamos que un cliente realize unicamente una oferta
            {
                if (o.Cliente.Equals(ofe.Cliente)) throw new Exception("El cliente ya realizo una oferta en esta publicacion.");
            }

            //comprobamos que un cliente realize una oferta con el monto mas alto al anterior
            double ultMonto = 0; 
            if (_listaOferta.Count > 0) ultMonto = _listaOferta[(_listaOferta.Count - 1)].Monto;
            if (ofe == null) throw new Exception("La oferta no puede ser nula");
            if (ofe.Monto <= ultMonto) throw new Exception("La oferta no puede ser menor o igual a la oferta anterior");
            ofe.Validar();
            _listaOferta.Add(ofe);
        }

        //public override void FinalizarPublicacion(Usuario usuario)
        //{
        //    // Validar si el usuario es un Administrador
        //    if (!(usuario is Administrador administrador))
        //    {
        //        throw new Exception("Solo un administrador puede cerrar la subasta.");
        //    }

        //    // Validar si hay una oferta válida con saldo suficiente
        //    OfertaSubasta mejorOferta = ObtenerPrimeraOfertaConSaldo();
        //    if (mejorOferta == null)
        //    {
        //        throw new Exception("No hay una oferta válida con saldo suficiente.");
        //    }
        //    Cliente clienteFinal = mejorOferta.Cliente as Cliente;
        //    // Si pasa las validaciones, proceder con el cierre de la subasta
        //    clienteFinal.DescontarSaldo(mejorOferta.Monto);
        //    _comprador = mejorOferta.Cliente; // Asignar el comprador
        //    _estado = TipoEstado.CERRADA;
        //    _usuarioCierre = administrador;
        //    _fechaCierre = DateTime.Now;
        //}

        //public override void FinalizarPublicacion(Usuario usuario)
        //{
        //    // Validar si el usuario es un Administrador
        //    if (!usuario.EsCliente())
        //    {
        //        Administrador administrador = (Administrador)usuario;  // Hacer el cast directo

        //        // Validar si hay una oferta válida con saldo suficiente
        //        OfertaSubasta mejorOferta = ObtenerPrimeraOfertaConSaldo();
        //        if (mejorOferta == null) 
        //        {
        //            throw new Exception("No hay una oferta válida con saldo suficiente.");
        //        }

        //        Cliente clienteFinal = (Cliente)mejorOferta.Cliente; // Cast directo, ya sabemos que es un Cliente
        //                                                             // Si pasa las validaciones, proceder con el cierre de la subasta
        //        clienteFinal.DescontarSaldo(mejorOferta.Monto);
        //        _comprador = mejorOferta.Cliente;  // Asignar el comprador
        //        _estado = TipoEstado.CERRADA;
        //        _usuarioCierre = administrador;
        //        _fechaCierre = DateTime.Now;
        //    }
        //    else
        //    {
        //        throw new Exception("Solo un administrador puede cerrar la subasta.");
        //    }
        //}

        public override void FinalizarPublicacion(Usuario usuario)
        {
            // Lanzar excepción si el usuario no es un Administrador
            if (usuario.EsCliente())
            {
                throw new Exception("Solo un administrador puede cerrar la subasta.");
            }

            Administrador administrador = (Administrador)usuario; // Hacer el cast directo

            // Ciclo para verificar si hay una oferta válida
            bool subastaFinalizada = false; // Control para finalizar el bucle
            while (!subastaFinalizada)
            {
                OfertaSubasta mejorOferta = ObtenerPrimeraOfertaConSaldo();

                if (mejorOferta == null)
                {
                    // Si la oferta es null o sea igual a 0, cambiar estado a CANCELADA
                    _estado = TipoEstado.CANCELADA;
                    _usuarioCierre = administrador;
                    _fechaCierre = DateTime.Now;
                    subastaFinalizada = true; // Salir del bucle
                }
                else
                {
                    // Procesar la oferta si es válida
                    Cliente clienteFinal = (Cliente)mejorOferta.Cliente; // Cast directo
                    clienteFinal.DescontarSaldo(mejorOferta.Monto);
                    _comprador = mejorOferta.Cliente; // Asignar el comprador
                    _estado = TipoEstado.CERRADA;
                    _usuarioCierre = administrador;
                    _fechaCierre = DateTime.Now;
                    subastaFinalizada = true; // Salir del bucle
                }
            }
        }

        private OfertaSubasta ObtenerPrimeraOfertaConSaldo()
        {
            foreach (OfertaSubasta oferta in _listaOferta)
            {
                Cliente c = oferta.Cliente as Cliente;
                if (c.Saldo >= oferta.Monto)
                {
                    return oferta; // Devolvemos la primera oferta válida
                }
            }
            return null; // Si no hay ninguna oferta válida
        }


        public double ObtenerOfertaDeCliente(Cliente c)
        {
            foreach (OfertaSubasta os in _listaOferta)
            {
                if (os.Cliente.Equals(c)) return os.Monto;
            }
            return 0.0;
        }
        public override bool EsSubasta()
        {
            return true; // Modifico para confirmar que es una subasta
        }

        public int CompareTo(Subasta? other)
        {
            return other.FechaPublicacion.CompareTo(this.FechaPublicacion);
        }
    }
}
