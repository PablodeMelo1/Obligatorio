namespace consola
{
    internal class Program
    {
        static void Main(string[] args)
        {
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
                        Opcion2();
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
            Console.WriteLine("2 - Listado de articulos por nombre");
            Console.WriteLine("3 - Alta de articulos");
            Console.WriteLine("4 - Listar publicaciones por fecha");
            Console.WriteLine("0 - Salir");
        }

        static void Opcion1()
        {
            
        }
        static void Opcion2()
        {

        }
        static void Opcion3()
        {

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
