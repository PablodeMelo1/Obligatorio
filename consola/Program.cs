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
                        Opcion1();
                        break;
                    case "2":
                        ListarArticulosPorNombreCategoria();
                        break;
                    case "3":
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
            CambioDeColor("***********************************************", ConsoleColor.Yellow);
            CambioDeColor("                     MENU                      ", ConsoleColor.Yellow);
            CambioDeColor("***********************************************", ConsoleColor.Yellow);
            Console.WriteLine();
            Console.WriteLine("1 - Listado de todos los clientes");
            Console.WriteLine("2 - Listado de articulos por nombre de categoria");
            Console.WriteLine("3 - Alta de Articulos");
            Console.WriteLine("4 - Listar publicaciones por fecha");
            Console.WriteLine("0 - Salir");
        }

        static void Opcion1()
        {
            
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
        static void Opcion4()
        {

        }
        #endregion
        #region METODOS ADICIONALES
        static void PressToContinue()
        {
            Console.WriteLine();
            Console.WriteLine("Presione una tecla para continuar ...");
            Console.ReadKey();
        }
        static string PedirPalabras(string mensaje)
        {
            Console.Write(mensaje);
            string datosPedidos = Console.ReadLine();
            return datosPedidos;
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
