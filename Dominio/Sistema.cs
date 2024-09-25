using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Sistema
    {
        private List<Publicacion> _listaPublicaciones = new List<Subasta>();
        private List<Articulo> _listaArticulos = new List<Articulo>();
        private List<Usuario> _listaUsuarios = new List<Cliente>();

        #region properties
        public List<Venta> Ventas
        {
            get
            {
                return _listaVentas;
            }
        }
        public List<Articulo> Articulos
        {
            get
            {
                return _listaArticulos;
            }
        }
        public List<Subasta> Subastas
        {
            get
            {
                return _listaSubastas;
            }
        }
        #endregion

        public Sistema()
        {
            PrecargarArticulos();
            PrecargarUsuarios();
        }
        // Precargados 50 articulos con la ayuda de ChatGPT
        private void PrecargarArticulos()
        {
            AltaArticulo(new Articulo("Televisor", "Electronica", 450.99));
            AltaArticulo(new Articulo("Laptop", "Electronica", 999.99));
            AltaArticulo(new Articulo("Smartphone", "Electronica", 699.50));
            AltaArticulo(new Articulo("Cámara", "Fotografia", 299.99));
            AltaArticulo(new Articulo("Refrigerador", "Electrodomesticos", 1200.00));
            AltaArticulo(new Articulo("Lavadora", "Electrodomesticos", 800.75));
            AltaArticulo(new Articulo("Microondas", "Cocina", 150.99));
            AltaArticulo(new Articulo("Aspiradora", "Hogar", 250.00));
            AltaArticulo(new Articulo("Monitor", "Computacion", 199.99));
            AltaArticulo(new Articulo("Impresora", "Oficina", 120.50));
            AltaArticulo(new Articulo("Tablet", "Electronica", 350.49));
            AltaArticulo(new Articulo("Parlante Bluetooth", "Electronica", 79.99));
            AltaArticulo(new Articulo("Auriculares Inalámbricos", "Electronica", 120.89));
            AltaArticulo(new Articulo("Teclado Mecánico", "Computacion", 89.99));
            AltaArticulo(new Articulo("Ratón Inalámbrico", "Computacion", 45.99));
            AltaArticulo(new Articulo("Disco Duro Externo", "Computacion", 75.50));
            AltaArticulo(new Articulo("SSD", "Computacion", 150.00));
            AltaArticulo(new Articulo("Cámara de Seguridad", "Seguridad", 199.99));
            AltaArticulo(new Articulo("Termómetro Digital", "Salud", 35.99));
            AltaArticulo(new Articulo("Purificador de Aire", "Hogar", 299.99));
            AltaArticulo(new Articulo("Freidora de Aire", "Cocina", 180.49));
            AltaArticulo(new Articulo("Licuadora", "Cocina", 99.99));
            AltaArticulo(new Articulo("Horno Eléctrico", "Cocina", 249.50));
            AltaArticulo(new Articulo("Plancha", "Electrodomesticos", 65.99));
            AltaArticulo(new Articulo("Secadora", "Electrodomesticos", 950.75));
            AltaArticulo(new Articulo("Batidora", "Cocina", 55.99));
            AltaArticulo(new Articulo("Cafetera", "Cocina", 120.00));
            AltaArticulo(new Articulo("Reloj Inteligente", "Electronica", 199.99));
            AltaArticulo(new Articulo("Proyector", "Electronica", 499.99));
            AltaArticulo(new Articulo("Router Wi-Fi", "Electronica", 75.99));
            AltaArticulo(new Articulo("Silla de Oficina", "Oficina", 175.50));
            AltaArticulo(new Articulo("Escritorio", "Oficina", 299.99));
            AltaArticulo(new Articulo("Lámpara LED", "Hogar", 29.99));
            AltaArticulo(new Articulo("Aire Acondicionado", "Electrodomesticos", 899.99));
            AltaArticulo(new Articulo("Tostadora", "Cocina", 45.99));
            AltaArticulo(new Articulo("Ventilador", "Hogar", 69.99));
            AltaArticulo(new Articulo("Humidificador", "Hogar", 85.50));
            AltaArticulo(new Articulo("Calefactor", "Hogar", 120.00));
            AltaArticulo(new Articulo("Lava Vajillas", "Cocina", 600.75));
            AltaArticulo(new Articulo("Cepillo Eléctrico", "Salud", 45.99));
            AltaArticulo(new Articulo("Báscula Digital", "Salud", 50.99));
            AltaArticulo(new Articulo("Smartband", "Electronica", 99.99));
            AltaArticulo(new Articulo("Bicicleta Eléctrica", "Transporte", 1500.00));
            AltaArticulo(new Articulo("Scooter Eléctrico", "Transporte", 1200.00));
            AltaArticulo(new Articulo("Tijeras Eléctricas", "Jardineria", 99.99));
            AltaArticulo(new Articulo("Robot Aspiradora", "Hogar", 399.99));
            AltaArticulo(new Articulo("Papelera Inteligente", "Hogar", 45.00));
            AltaArticulo(new Articulo("Cámara Instantánea", "Fotografia", 120.00));
            AltaArticulo(new Articulo("Trípode", "Fotografia", 79.99));
        }

        private void PrecargarUsuarios()
        {

            AltaUsuario(new Administrador("Pablo", "de Melo", "pablodemelo@gmail.com", "pablo123"));
            AltaUsuario(new Administrador("Fabian", "Fernandez", "fabianfernandez@gmail.com", "fabianfer333"));

            //Alta de clientes con ayuda de ChatGPT
            AltaUsuario(new Cliente("Cristian", "Rodriguez", "cristian12@gmail.com", "abc12", 23000.00));
            AltaUsuario(new Cliente("Sofia", "Martinez", "sofia.martinez@gmail.com", "pass123", 15000.00));
            AltaUsuario(new Cliente("Lucas", "Fernandez", "lucas.fernandez@gmail.com", "fernandez456", 18000.00));
            AltaUsuario(new Cliente("Valentina", "Gomez", "valentina.gomez@gmail.com", "vale789", 12000.00));
            AltaUsuario(new Cliente("Mateo", "Lopez", "mateo.lopez@gmail.com", "lopez321", 20000.00));
            AltaUsuario(new Cliente("Camila", "Garcia", "camila.garcia@gmail.com", "cami654", 25000.00));
            AltaUsuario(new Cliente("Juan", "Perez", "juan.perez@gmail.com", "juan987", 17000.00));
            AltaUsuario(new Cliente("Martina", "Ruiz", "martina.ruiz@gmail.com", "ruiz111", 22000.00));
            AltaUsuario(new Cliente("Joaquin", "Mendez", "joaquin.mendez@gmail.com", "joaquin222", 14000.00));
            AltaUsuario(new Cliente("Renata", "Silva", "renata.silva@gmail.com", "silva333", 26000.00));

        }

        public void AltaArticulo(Articulo articulo)
        {
            if (articulo == null) throw new Exception("El articulo no puede ser nulo");
            articulo.Validar();
            _listaArticulos.Add(articulo);
        }
        public void AltaUsuario(Usuario usuario)
        {
            if (usuario == null) throw new Exception("El Usuario no puede ser nulo");
            usuario.Validar();
            _listaUsuarios.Add(usuario);
        }

        public List<Articulo> ArticulosPorCategoria(string categoria)
        {
            List<Articulo> buscados = new List<Articulo>();
            foreach (Articulo a in _listaArticulos)
            {
                if(a.Categoria.ToLower() == categoria.ToLower()) buscados.Add(a);
            }
            return buscados;

        }

    }
}
