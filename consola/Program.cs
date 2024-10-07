using Dominio;
namespace consola
{
    internal class Program
    {

        private static Sistema miSistema;
        static void Main(string[] args)
        {
            miSistema = new Sistema();

            string opcion = "";

            while (opcion != "0")
            {
                MostrarMenu();
                opcion = PedirPalabras("Ingrese una opcion -> ");

                switch (opcion)
                {
                    case "1":
                        CatalogoArticulos();
                        break;
                    case "2":
                        ListarArticulosPorNombreCategoria();
                        break;
                    case "3":
                        AltaArticulo();
                        break;
                    case "4":
                        ListarPublicacionesEntreFechas();
                        break;
                    case "5":
                        ListarPublicaciones();
                        break;
                    case "0":
                        Console.WriteLine("Salir ...");
                        break;
                    default:
                        MostrarError("Debe ingresar una opcion valida");
                        PressToContinue();
                        break;
                }
            }
        }

        #region METODOS DE MENU

        static void MostrarMenu()
        {
            Console.Clear();
            CambioDeColor("***********************************************", ConsoleColor.Yellow);
            CambioDeColor("                     MENU                      ", ConsoleColor.Yellow);
            CambioDeColor("***********************************************", ConsoleColor.Yellow);
            Console.WriteLine();
            Console.WriteLine("1 - Catalogo de Articulos");
            Console.WriteLine("2 - Listado de articulos por nombre de categoria");
            Console.WriteLine("3 - Alta de Articulos");
            Console.WriteLine("4 - Listar publicaciones por fecha");
            Console.WriteLine("0 - Salir");
        }

        static void CatalogoArticulos()
        {
            Console.Clear();
            CambioDeColor("Catalogo de Articulos", ConsoleColor.Yellow);
            Console.WriteLine();

            foreach (Articulo a in miSistema.Articulos)
            {
                Console.WriteLine(a);
            }
            PressToContinue();
        }
        static void ListarArticulosPorNombreCategoria()
        {
            Console.Clear();
            CambioDeColor("LISTA DE ARTICULOS POR CATEGORIA", ConsoleColor.Yellow);
            Console.WriteLine();

            string pedirCategoria = PedirPalabras("Ingrese la categoria para buscar: ");
            
            try
            { 
            List<Articulo> articulosBuscados = miSistema.ArticulosPorCategoria(pedirCategoria);
            if(articulosBuscados.Count == 0)
            {
                MostrarError("No existen articulos con dicha categoria!");
            }
            else
            {
                foreach(Articulo a in articulosBuscados)
                {
                    Console.WriteLine(a);
                }
            }
            }
            catch(Exception ex)
            {
                MostrarError(ex.Message);
            }

            PressToContinue();
        }
        static void AltaArticulo()
        {
            Console.Clear();
            CambioDeColor("ALTA DE ARTICULOS", ConsoleColor.Yellow);
            Console.WriteLine();

            string nombre = PedirPalabras("Ingrese el nombre de un articulo: ");
            string categoria = PedirPalabras("Ingrese una categoria: ");
            bool exito = false;
            double precioVenta = 0;

            do
            {
                Console.Write("Ingrese un precio de venta: ");
                exito = double.TryParse(Console.ReadLine(), out precioVenta);
                if (!exito) MostrarError("ERROR: Debe ingresar solo números.");
            }
            while (!exito);

            try
            {                
                Articulo miArticulo = new Articulo(nombre, categoria, precioVenta);
                miSistema.AltaArticulo(miArticulo);
                MostrarExito("Articulo dado de alta con exito!");
            }
            catch (Exception ex)
            {
                MostrarError(ex.Message);
            }

            PressToContinue();

        }
        static void ListarPublicacionesEntreFechas()
        {
            Console.Clear();
            CambioDeColor("LISTAR PUBLICACIONES ENTRE DOS FECHAS", ConsoleColor.Yellow);
            Console.WriteLine();

            DateTime fechaInicio = PedirFecha("Ingrese la fecha de inicio");
            DateTime fechaFin = PedirFecha("Ingrese la fecha de fin");

            // Listar publicaciones entre las fechas dadas
            List<Publicacion> listarPub = miSistema.ListarPublicacionesEntreFechas(fechaInicio, fechaFin);
            if(listarPub.Count == 0) 
            {
                    MostrarError($"No existen publicaciones entre las fechas {fechaInicio} y {fechaFin}");
            }
            else
            {
                foreach(Publicacion p in listarPub)
                {
                    Console.WriteLine(p);
                }
            }

            PressToContinue();
        }

        static void ListarPublicaciones()
        {
            Console.Clear();
            CambioDeColor("LISTA DE PUBLICACIONES", ConsoleColor.Yellow);
            Console.WriteLine();

            foreach(Publicacion p in miSistema.ListarPublicaciones())
            {
                Console.WriteLine("\n" + p);
            }

            PressToContinue();
        }


        #endregion

        #region METODOS ADICIONALES

        static string PedirPalabras(string mensaje)
        {
            Console.Write(mensaje);
            string datos = Console.ReadLine();
            return datos;
        }
        static void PressToContinue()
        {
            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar ...");
            Console.ReadKey();
        }
        static DateTime PedirFecha(string mensaje)
        {
            bool exito = false;
            DateTime fecha = new DateTime();

            while (!exito)
            {
                Console.Write($"{mensaje} [dd/MM/yyyy]:");
                exito = DateTime.TryParse(Console.ReadLine(), out fecha);

                if (!exito)
                {
                    MostrarError("ERROR: La fecha no respeta el formato dd/MM/yyyy");
                }
            }

            return fecha;
        }

        static void MostrarError(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(mensaje);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        static void MostrarExito(string mensaje)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(mensaje);
            Console.ForegroundColor = ConsoleColor.Gray;
        }
        static void CambioDeColor(string mensaje, ConsoleColor color)
        {
            Console.ForegroundColor = color;
            Console.WriteLine(mensaje);
            Console.ForegroundColor = ConsoleColor.Gray;
        }

        #endregion
    }
}
