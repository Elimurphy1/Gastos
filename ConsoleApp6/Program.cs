using System;
using System.Linq;

namespace ProyectoFinanzas
{
    internal class Program
    {
        static void Main(string[] args)
        {
            ApplicationDbContext contexto = new ApplicationDbContext();
            Gestor miGestor = new Gestor(contexto);
            bool salir = false;

            Console.WriteLine("Ingrese nombre de usuario: ");
            string NombreUsuario = Console.ReadLine(); 
            
            var usuarioActual = miGestor.Usuarios.FirstOrDefault(u => u.Nombre == NombreUsuario);
            if(usuarioActual != null)
            {
                Console.WriteLine($"¡Bienvenido de nuevo, {usuarioActual.Nombre}!");
            }
            else
            {
                // 1. Instanciamos el usuario y lo guardamos en la variable
                usuarioActual = new Usuario { Id = miGestor.Usuarios.Count + 1, Nombre = NombreUsuario, Email = "eliasisa33@gmail.com" };

                // 2. Agregamos ESE usuario a la lista
                miGestor.Usuarios.Add(usuarioActual);

                Console.WriteLine($"\nUsuario nuevo creado. ¡Bienvenido, {usuarioActual.Nombre}!");
            }

            while (!salir)
            {
                Console.WriteLine("\n=== CONTROL DE FINANZAS PRO ===");
                Console.WriteLine("1. Registrar Ingreso");
                Console.WriteLine("2. Registrar Gasto");
                Console.WriteLine("3. Ver Resumen General");
                Console.WriteLine("4. Ver Movimientos Detallados");
                Console.WriteLine("5. Reporte de Gastos por Categoría");
                Console.WriteLine("6. Proyectar Resumen de Tarjeta");
                Console.WriteLine("7. Salir");
                Console.Write("Seleccione una opción: ");

                string opcion = Console.ReadLine() ?? "";

                switch (opcion)
                {
                    case "1":
                        Console.Write("Descripción (ej. Honorarios): ");
                        string descIngreso = Console.ReadLine() ?? "Ingreso";
                        Console.Write("Monto: $");
                        double.TryParse(Console.ReadLine(), out double montoIngreso);

                        Console.WriteLine("Categorías de Ingreso:");
                        var catIngresos = miGestor.Categorias.Where(c => c.TipoOperacion == "Ingreso").ToList();
                        foreach (var cat in catIngresos) Console.WriteLine($"{cat.Id}. {cat.Nombre}");
                        Console.Write("ID Categoría: ");
                        int.TryParse(Console.ReadLine(), out int idCatIngreso);

                        miGestor.AgregarIngreso(new Ingreso
                        {
                            UsuarioId = usuarioActual.Id,
                            Descripcion = descIngreso,
                            Monto = montoIngreso,
                            FechaHora = DateTime.Now,
                            CategoriaId = idCatIngreso,
                            Categoria = catIngresos.FirstOrDefault(c => c.Id == idCatIngreso)!
                        });
                        break;

                    case "2":
                        Console.Write("Descripción (ej. Nafta): ");
                        string descGasto = Console.ReadLine() ?? "Gasto";
                        Console.Write("Monto: $");
                        double.TryParse(Console.ReadLine(), out double montoGasto);

                        Console.WriteLine("Categorías de Gasto:");
                        var catGastos = miGestor.Categorias.Where(c => c.TipoOperacion == "Gasto").ToList();
                        foreach (var cat in catGastos) Console.WriteLine($"{cat.Id}. {cat.Nombre}");
                        Console.Write("ID Categoría: ");
                        int.TryParse(Console.ReadLine(), out int idCatGasto);

                        Console.WriteLine("Medio de pago (0 para Efectivo/Débito):");
                        foreach (var t in miGestor.Tarjetas) Console.WriteLine($"{t.Id}. {t.Nombre} {t.Banco}");
                        Console.Write("ID Medio de pago: ");
                        int.TryParse(Console.ReadLine(), out int idTarjeta);
                        var tarjetaSel = miGestor.Tarjetas.FirstOrDefault(t => t.Id == idTarjeta);

                        int cuotas = 1;
                        if (tarjetaSel != null)
                        {
                            Console.Write("Cantidad de cuotas: ");
                            int.TryParse(Console.ReadLine(), out cuotas);
                        }

                        miGestor.AgregarGasto(new Gasto
                        {
                            UsuarioId = usuarioActual.Id,
                            Descripcion = descGasto,
                            Monto = montoGasto,
                            FechaHora = DateTime.Now,
                            CategoriaId = idCatGasto,
                            Categoria = catGastos.FirstOrDefault(c => c.Id == idCatGasto)!,
                            TarjetaCreditoId = tarjetaSel?.Id,
                            TarjetaCredito = tarjetaSel,
                            Cuotas = cuotas > 0 ? cuotas : 1
                        });
                        break;

                    case "3":
                        miGestor.VerResumen();
                        break;

                    case "4":
                        miGestor.VerMovimientosDetallados();
                        break;

                    case "5":
                        miGestor.GenerarReportePorCategoria();
                        break;

                    case "6":
                        Console.WriteLine("\nSeleccione la tarjeta a consultar:");
                        foreach (var t in miGestor.Tarjetas) Console.WriteLine($"{t.Id}. {t.Nombre} {t.Banco}");
                        Console.Write("ID Tarjeta: ");
                        if (int.TryParse(Console.ReadLine(), out int tarjId))
                        {
                            miGestor.ProyectarResumenTarjeta(tarjId);
                        }
                        break;

                    case "7":
                        salir = true;
                        Console.WriteLine("Saliendo del programa...");
                        break;

                    default:
                        Console.WriteLine("Opción inválida.");
                        break;
                }
            }
        }
    }
}