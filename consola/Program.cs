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
                        ListadoClientes();
                        break;
                    case "2":
                        ListarArticulosPorNombreCategoria();
                        break;
                    case "3":
                        ListarPublicacionesEntreFechas();
                        break;
                    case "4":
                        AltaArticulo();
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
            CambioDeColor("****************************************************", ConsoleColor.Yellow);
            CambioDeColor("*                      MENU                        *", ConsoleColor.Yellow);
            CambioDeColor("****************************************************", ConsoleColor.Yellow);
            CambioDeColor("*                                                  *", ConsoleColor.Yellow);
            CambioDeColor("* 1 - Listado de clientes                          *", ConsoleColor.Yellow);
            CambioDeColor("* 2 - Listado de articulos por nombre de categoria *", ConsoleColor.Yellow);
            CambioDeColor("* 3 - Listado de publicaciones por fecha           *", ConsoleColor.Yellow);
            CambioDeColor("* 4 - Alta de Articulos                            *", ConsoleColor.Yellow);
            CambioDeColor("* 0 - Salir                                        *", ConsoleColor.Yellow);
            CambioDeColor("****************************************************", ConsoleColor.Yellow);
        }

        static void ListadoClientes()
        {
            Console.Clear();
            CambioDeColor("Listado de Clientes", ConsoleColor.Yellow);
            Console.WriteLine();
            int cont = 1;
            foreach (Usuario u in miSistema.Usuario)
            {
                if (u is Cliente cliente) Console.WriteLine(u);// comprueba que Usuario u sea un Cliente y no Administrador y lo muestra en pantalla.

            }
            PressToContinue();
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
            Console.WriteLine();

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

            DateTime fechaInicio;
            DateTime fechaFin;

            while (true)
            {
                fechaInicio = PedirFecha("Ingrese la fecha de inicio");
                fechaFin = PedirFecha("Ingrese la fecha de fin");

                // Validar que la fecha de inicio no sea mayor que la fecha de fin
                if (fechaInicio > fechaFin)
                {
                    MostrarError($"La fecha de inicio no puede ser mayor que la fecha de fin. Intente de nuevo.");
                }
                else
                {
                    break; // Salir del bucle si las fechas son válidas
                }
            }

            List<Publicacion> listarPub = miSistema.ListarPublicacionesEntreFechas(fechaInicio, fechaFin);

            if (listarPub.Count == 0)
            {
                MostrarError($"No existen publicaciones entre las fechas {fechaInicio} y {fechaFin}");
            }
            else
            {
                foreach (Publicacion p in listarPub)
                {
                    Console.WriteLine(p);
                }
            }

            PressToContinue();
        }

        //static void ListarPublicaciones()
        //{
        //    Console.Clear();
        //    CambioDeColor("LISTA DE PUBLICACIONES", ConsoleColor.Yellow);
        //    Console.WriteLine();

        //    foreach(Publicacion p in miSistema.ListarPublicaciones())
        //    {
        //        Console.WriteLine("\n" + p);
        //    }

        //    PressToContinue();
        //}


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
