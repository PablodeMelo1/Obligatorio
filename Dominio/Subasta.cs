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
                      
        }     

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
            OfertaSubasta buscado = null;
            int i = _listaOferta.Count - 1;
            while (buscado == null && i >= 0)
            {
                Cliente c = _listaOferta[i].Cliente as Cliente;
                if (c.Saldo >= _listaOferta[i].Monto)
                {
                    buscado = _listaOferta[i]; // devolvemos la primera oferta válida
                }
                i--;
            }

            return buscado; // Si no hay ninguna oferta válida
        }
        //private OfertaSubasta ObtenerPrimeraOfertaConSaldo()
        //{
        //    foreach (OfertaSubasta oferta in _listaOferta)
        //    {
        //        Cliente c = oferta.Cliente as Cliente;
        //        if (c.Saldo >= oferta.Monto)
        //        {
        //            return oferta; // Devolvemos la primera oferta válida
        //        }
        //    }
        //    return null; // Si no hay ninguna oferta válida
        //}


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
