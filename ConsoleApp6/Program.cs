namespace ConsoleApp6
{
    internal class Program
    {
        static void Main(string[] args)
        {
            

            Console.WriteLine("Menú\n");

            Console.WriteLine("Ingrese nombre del usuario: ");

            String Nombre = Console.ReadLine();           

            Gestor miOperatoria = new Gestor();

            String Opcion;

            do
            {
                Console.WriteLine("Hola " + Nombre + ", que operatoria necesita realizar?\n");
                Console.WriteLine("1. Agregar Ingreso");
                Console.WriteLine("2. Agregar Gasto");
                Console.WriteLine("3. Agregar Tarjeta");
                Console.WriteLine("4. Ver todos los gastos");
                Console.WriteLine("5. Ver cuotas restantes");
                Console.WriteLine("6. Ver dinero restante");
                Console.WriteLine("7. Cuanto va gastado de tarjeta");
                Console.WriteLine("8. Salir");
                Console.WriteLine("Seleccione una opcion: ");
                Opcion = Console.ReadLine();

                switch (Opcion) 
                {
                    case "1":
                        Console.WriteLine("\nEscriba el ingreso en pesos argentinos: $ ");
                        String Ingreso = Console.ReadLine();
                        double IngresoUsuario;
                        while(!double.TryParse(Ingreso , out IngresoUsuario))
                        {
                            Console.WriteLine("Numero invalido, por favor escriba el ingreso en precios argentinos: $");
                            Ingreso = Console.ReadLine();
                        }
                        //Agregar Ingreso
                        miOperatoria.AgregarIngreso(IngresoUsuario);
                        Console.WriteLine("Ingreso agregado con exito\n");
                        break;
                    case "2":
                        Console.WriteLine("\nIngrese el gasto: ");
                        String Gasto = Console.ReadLine();
                        double GastoUsuario;
                        while(!double.TryParse(Gasto ,out GastoUsuario))
                        {
                            Console.WriteLine("Numero invalido, por favor escriba el gasto nuevamente: ");
                            Gasto = Console.ReadLine();
                        }
                        Console.WriteLine("El gasto es para tarjeta?: ");
                        String UsaTarjeta = Console.ReadLine();
                        if (UsaTarjeta == "si")
                        {
                            miOperatoria.UsaTarjeta(GastoUsuario);
                        }
                        Console.WriteLine("Cuantas cuotas?: ");
                        String Cuotas = Console.ReadLine();
                        double CuotasUsuario;
                        while (!double.TryParse(Cuotas, out CuotasUsuario))
                        {
                            Console.WriteLine("Numero invalido, por favor escriba las cuotas nuevamente: ");
                            Cuotas = Console.ReadLine();
                        }
                        miOperatoria.CuotasTarjeta(CuotasUsuario);
                        miOperatoria.AgregarGasto(GastoUsuario);
                        Console.WriteLine("Gasto agregado con exito\n");
                        break;
                    case "3":
                        Console.WriteLine("\nIngresar tarjeta: ");
                        String Tarjeta = Console.ReadLine();
                        miOperatoria.AgregarTarjeta(Tarjeta);
                        break;
                    case "4":
                        miOperatoria.VerGastos();
                        break;
                    case "5":

                        break;
                    case "6":
                        miOperatoria.DineroRestante();
                        break;
                    case "7":

                        break;
                    case "8":

                        break;


                }



            }
            while (Opcion !="7");




        }
    }
}
