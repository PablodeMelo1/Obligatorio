using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio
{
    public class Sistema
    {
        private List<Publicacion> _listaPublicaciones = new List<Publicacion>();
        private List<Articulo> _listaArticulos = new List<Articulo>();
        private List<Usuario> _listaUsuarios = new List<Usuario>();

        #region properties
        public List<Usuario> Usuario
        {
            get
            {
                return _listaUsuarios;
            }
        }
        public List<Articulo> Articulos
        {
            get
            {
                return _listaArticulos;
            }
        }
        public List<Publicacion> Publicacion
        {
            get
            {
                return _listaPublicaciones;
            }
        }
        #endregion

        
        public Sistema()
        {
            // precargas de Publicaciones, articulos a publicaciones y ofertas con chat gpt: https://chatgpt.com/share/67005140-8680-8000-a19b-bb11919a3ae5
            PrecargarArticulos();
            PrecargarUsuarios();
            PrecargarPublicaciones();
            PrecargarArticulosAPublicaciones();
            PrecargarOfertasASubastas();
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
        
        private void PrecargarPublicaciones()
        {
            //Ventas
            // Ventas ABIERTAS
            AltaPublicacion(new Venta(false, "Venta aleatoria 1", TipoEstado.ABIERTA, new DateTime(2024, 10, 07), null, null, null));
            AltaPublicacion(new Venta(true, "Venta aleatoria 2", TipoEstado.ABIERTA, new DateTime(2024, 09, 27), null, null, null));
            AltaPublicacion(new Venta(false, "Venta aleatoria 3", TipoEstado.ABIERTA, new DateTime(2024, 10, 01), null, null, null));
            AltaPublicacion(new Venta(true, "Venta aleatoria 4", TipoEstado.ABIERTA, new DateTime(2024, 09, 29), null, null, null));

            // Ventas CERRADAS
            AltaPublicacion(new Venta(false, "Venta cerrada 1", TipoEstado.CERRADA, new DateTime(2024, 09, 24), ObtenerClientePorId(5), ObtenerAdministradorPorId(1), new DateTime(2024, 09, 28)));
            AltaPublicacion(new Venta(true, "Venta cerrada 2", TipoEstado.CERRADA, new DateTime(2024, 09, 30), ObtenerClientePorId(3), ObtenerAdministradorPorId(2), new DateTime(2024, 10, 03)));
            AltaPublicacion(new Venta(false, "Venta cerrada 3", TipoEstado.CERRADA, new DateTime(2024, 10, 02), ObtenerClientePorId(7), ObtenerAdministradorPorId(1), new DateTime(2024, 10, 04)));

            // Ventas CANCELADAS
            AltaPublicacion(new Venta(true, "Venta cancelada 1", TipoEstado.CANCELADA, new DateTime(2024, 09, 26), ObtenerClientePorId(1), ObtenerAdministradorPorId(2), new DateTime(2024, 09, 27)));
            AltaPublicacion(new Venta(true, "Venta cancelada 2", TipoEstado.CANCELADA, new DateTime(2024, 10, 01), ObtenerClientePorId(9), ObtenerAdministradorPorId(1), new DateTime(2024, 10, 02)));
            AltaPublicacion(new Venta(false, "Venta cancelada 3", TipoEstado.CANCELADA, new DateTime(2024, 09, 25), ObtenerClientePorId(6), ObtenerAdministradorPorId(2), new DateTime(2024, 09, 29)));


            //Subastas
            // Subastas ABIERTAS
            AltaPublicacion(new Subasta("Subasta aleatoria 1", TipoEstado.ABIERTA, new DateTime(2024, 10, 04), null, null, null));
            AltaPublicacion(new Subasta("Subasta aleatoria 2", TipoEstado.ABIERTA, new DateTime(2024, 09, 30), null, null, null));
            AltaPublicacion(new Subasta("Subasta aleatoria 3", TipoEstado.ABIERTA, new DateTime(2024, 10, 01), null, null, null));
            AltaPublicacion(new Subasta("Subasta aleatoria 4", TipoEstado.ABIERTA, new DateTime(2024, 09, 27), null, null, null));

            // Subastas CERRADAS
            AltaPublicacion(new Subasta("Subasta cerrada 1", TipoEstado.CERRADA, new DateTime(2024, 09, 26), ObtenerClientePorId(4), ObtenerAdministradorPorId(1), new DateTime(2024, 09, 29)));
            AltaPublicacion(new Subasta("Subasta cerrada 2", TipoEstado.CERRADA, new DateTime(2024, 09, 29), ObtenerClientePorId(7), ObtenerAdministradorPorId(2), new DateTime(2024, 10, 01)));
            AltaPublicacion(new Subasta("Subasta cerrada 3", TipoEstado.CERRADA, new DateTime(2024, 09, 28), ObtenerClientePorId(2), ObtenerAdministradorPorId(1), new DateTime(2024, 10, 02)));

            // Subastas CANCELADAS
            AltaPublicacion(new Subasta("Subasta cancelada 1", TipoEstado.CANCELADA, new DateTime(2024, 09, 24), ObtenerClientePorId(5), ObtenerAdministradorPorId(2), new DateTime(2024, 09, 27)));
            AltaPublicacion(new Subasta("Subasta cancelada 2", TipoEstado.CANCELADA, new DateTime(2024, 09, 30), ObtenerClientePorId(3), ObtenerAdministradorPorId(1), new DateTime(2024, 10, 03)));
            AltaPublicacion(new Subasta("Subasta cancelada 3", TipoEstado.CANCELADA, new DateTime(2024, 09, 25), ObtenerClientePorId(6), ObtenerAdministradorPorId(2), new DateTime(2024, 09, 29)));

        }
        private void PrecargarOfertasASubastas()
        {
            // Ofertas para la subasta con ID 12
            AgregarOfertaASubasta(12, 3, 1500, new DateTime(2024, 10, 03));
            AgregarOfertaASubasta(12, 5, 2000, new DateTime(2024, 10, 04));
            AgregarOfertaASubasta(12, 7, 2500, new DateTime(2024, 10, 05));

            // Ofertas para la subasta con ID 18
            AgregarOfertaASubasta(11, 11, 1200, new DateTime(2024, 10, 01));
            AgregarOfertaASubasta(11, 4, 1800, new DateTime(2024, 10, 02));
            AgregarOfertaASubasta(11, 9, 2200, new DateTime(2024, 10, 03));
        }
        private void PrecargarArticulosAPublicaciones()
        {
            // Artículos para las ventas y subastas
            AgregarArticuloAPublicacion(1, 1);
            AgregarArticuloAPublicacion(1, 2);

            AgregarArticuloAPublicacion(2, 3);
            AgregarArticuloAPublicacion(2, 4);

            AgregarArticuloAPublicacion(3, 5);
            AgregarArticuloAPublicacion(3, 6);

            AgregarArticuloAPublicacion(4, 7);
            AgregarArticuloAPublicacion(4, 8);

            AgregarArticuloAPublicacion(5, 9);
            AgregarArticuloAPublicacion(5, 10);

            AgregarArticuloAPublicacion(6, 11);
            AgregarArticuloAPublicacion(6, 12);

            AgregarArticuloAPublicacion(7, 13);
            AgregarArticuloAPublicacion(7, 14);

            AgregarArticuloAPublicacion(8, 15);
            AgregarArticuloAPublicacion(8, 16);

            AgregarArticuloAPublicacion(9, 17);
            AgregarArticuloAPublicacion(9, 18);

            AgregarArticuloAPublicacion(10, 19);
            AgregarArticuloAPublicacion(10, 20);

            AgregarArticuloAPublicacion(11, 21);
            AgregarArticuloAPublicacion(11, 22);

            AgregarArticuloAPublicacion(12, 23);
            AgregarArticuloAPublicacion(12, 24);

            AgregarArticuloAPublicacion(13, 25);
            AgregarArticuloAPublicacion(13, 26);

            AgregarArticuloAPublicacion(14, 27);
            AgregarArticuloAPublicacion(14, 28);

            AgregarArticuloAPublicacion(15, 29);
            AgregarArticuloAPublicacion(15, 30);

            AgregarArticuloAPublicacion(16, 31);
            AgregarArticuloAPublicacion(16, 32);

            AgregarArticuloAPublicacion(17, 33);
            AgregarArticuloAPublicacion(17, 34);

            AgregarArticuloAPublicacion(18, 35);
            AgregarArticuloAPublicacion(18, 36);

            AgregarArticuloAPublicacion(19, 37);
            AgregarArticuloAPublicacion(19, 38);

            AgregarArticuloAPublicacion(20, 39);
            AgregarArticuloAPublicacion(20, 40);

        }
        public void AgregarArticuloAPublicacion(int idPubli, int idArt)
        {
            Articulo a = ObtenerArticulosPorId(idArt);
            if (a == null) throw new Exception("El articulo no puede ser nulo");
            Publicacion p = ObtenerPublicacionPorId(idPubli);
            if (p == null) throw new Exception("La publicacion no puede ser nula");
            p.RegistrarArticulo(a);
        }

        public void AgregarOfertaASubasta(int idSub, int idClie,double monto, DateTime fecha)
        {
            Subasta s = ObtenerSubastaPorId(idSub);
            if (s == null) throw new Exception("La subasta no puede ser nula");
            Oferta o = new Oferta(ObtenerClientePorId(idClie), monto);
            OfertaSubasta ofe = new OfertaSubasta(o, fecha);
            s.RegistrarOferta(ofe);
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

        public void AltaPublicacion(Publicacion publi)
        {
            if (publi == null) throw new Exception("La publicacion no puede ser nula");
            publi.Validar();
            _listaPublicaciones.Add(publi);
            
        }

        public List<Articulo> ArticulosPorCategoria(string categoria)
        {
            List<Articulo> buscados = new List<Articulo>();
            foreach (Articulo a in _listaArticulos)
            {
                if (a.Categoria.ToLower() == categoria.ToLower()) buscados.Add(a);
            }
            return buscados;

        }

        public List<Publicacion> ListarPublicacionesEntreFechas(DateTime fechaInicio, DateTime fechaFin)
        {

            Console.WriteLine($"Publicaciones entre {fechaInicio:dd/MM/yyyy} y {fechaFin:dd/MM/yyyy}:");
            List<Publicacion> buscados = new List<Publicacion>();
            foreach (Publicacion p in _listaPublicaciones)
            {
                if (p.FechaPublicacion >= fechaInicio && p.FechaPublicacion <= fechaFin) buscados.Add(p);
            }
            return buscados;
        }

        public List<Publicacion> ListarPublicaciones()
        {
            List<Publicacion> buscados = new List<Publicacion>();
            foreach (Publicacion p in _listaPublicaciones)
            {
                buscados.Add(p);
            }
            return buscados;
        }
        public Cliente ObtenerClientePorId(int id)
        {
            Cliente buscado = null;
            int i = 0;

            while (i < _listaUsuarios.Count && buscado == null)
            {
                // Intentamos convertir el usuario a Cliente usando 'as'
                Cliente cliente = _listaUsuarios[i] as Cliente;

                // Si la conversión fue exitosa (cliente no es null) y el ID coincide
                if (cliente != null && cliente.Id == id)
                {
                    buscado = cliente;
                }

                i++;
            }

            return buscado;
        }
        public Administrador ObtenerAdministradorPorId(int id)
        {
            Administrador buscado = null;
            int i = 0;

            while (i < _listaUsuarios.Count && buscado == null)
            {
                // Intentamos convertir el usuario a Administrador usando 'as'
                Administrador administrador = _listaUsuarios[i] as Administrador;

                // Si la conversión fue exitosa (administrador no es null) y el ID coincide
                if (administrador != null && administrador.Id == id)
                {
                    buscado = administrador;
                }

                i++;
            }

            return buscado;
        }
        public Publicacion ObtenerPublicacionPorId(int id)
        {
            Publicacion buscado = null;
            int i = 0;
            while (i < _listaPublicaciones.Count && buscado == null)
            {
                if (_listaPublicaciones[i].Id == id) buscado = _listaPublicaciones[i];
                i++;
            }
            return buscado;
        }
        public Articulo ObtenerArticulosPorId(int id)
        {
            Articulo buscado = null;
            int i = 0;
            while (i < _listaArticulos.Count && buscado == null)
            {
                if (_listaArticulos[i].Id == id) buscado = _listaArticulos[i];
                i++;
            }
            
            return buscado;
        }

        public Subasta ObtenerSubastaPorId(int id)
        {
            Subasta buscado = null;
            int i = 0;
            while (i < _listaPublicaciones.Count && buscado == null)
            {
                Subasta sub = _listaPublicaciones[i] as Subasta;

                if (_listaPublicaciones[i].Id == id) buscado = sub;
                i++;
            }

            return buscado;
        }
    }
}
