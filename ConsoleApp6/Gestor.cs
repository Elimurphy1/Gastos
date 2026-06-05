using System;
using System.Collections.Generic;
using System.Linq;

namespace ProyectoFinanzas
{
    internal class Gestor
    {
        public List<Gasto> Gastos { get; set; } = new List<Gasto>();
        public List<Ingreso> Ingresos { get; set; } = new List<Ingreso>();
        public List<Categoria> Categorias { get; set; } = new List<Categoria>();
        public List<TarjetaCredito> Tarjetas { get; set; } = new List<TarjetaCredito>();
        public List<Usuario> Usuarios { get; set; } = new List<Usuario>(); 
        public void AgregarIngreso(Ingreso nuevoIngreso)
        {
            Ingresos.Add(nuevoIngreso);
            Console.WriteLine($"\n[+] Ingreso '{nuevoIngreso.Descripcion}' de ${nuevoIngreso.Monto} agregado exitosamente.");
        }

        public void AgregarGasto(Gasto nuevoGasto)
        {
            Gastos.Add(nuevoGasto);
            Console.WriteLine($"\n[-] Gasto '{nuevoGasto.Descripcion}' de ${nuevoGasto.Monto} agregado exitosamente.");
        }

        public void VerResumen()
        {
            Console.WriteLine("\n--- RESUMEN FINANCIERO AL " + DateTime.Now.ToString("dd/MM/yyyy HH:mm") + " ---");
            double totalIngresos = Ingresos.Sum(i => i.Monto);
            double totalGastos = Gastos.Sum(g => g.Monto);
            double saldo = totalIngresos - totalGastos;

            Console.WriteLine($"Total Ingresos: ${totalIngresos}");
            Console.WriteLine($"Total Gastos:   ${totalGastos}");
            Console.WriteLine($"Saldo Líquido:  ${saldo}");
            Console.WriteLine("------------------------------------------");
        }

        public void VerMovimientosDetallados()
        {
            Console.WriteLine("\n--- HISTORIAL DETALLADO ---");

            Console.WriteLine("\nINGRESOS:");
            if (Ingresos.Count == 0) Console.WriteLine("No hay ingresos registrados.");
            foreach (var ing in Ingresos)
            {
                string cat = ing.Categoria != null ? ing.Categoria.Nombre : "Sin categoría";
                Console.WriteLine($"{ing.FechaHora.ToString("dd/MM/yy HH:mm")} | {ing.Descripcion} | +${ing.Monto} | Rubro: {cat}");
            }

            Console.WriteLine("\nGASTOS:");
            if (Gastos.Count == 0) Console.WriteLine("No hay gastos registrados.");
            foreach (var gasto in Gastos)
            {
                string tarjeta = gasto.TarjetaCredito != null ? $"{gasto.TarjetaCredito.Nombre} {gasto.TarjetaCredito.Banco}" : "Efectivo/Débito";
                string cat = gasto.Categoria != null ? gasto.Categoria.Nombre : "Sin categoría";
                Console.WriteLine($"{gasto.FechaHora.ToString("dd/MM/yy HH:mm")} | {gasto.Descripcion} | -${gasto.Monto} | Rubro: {cat} | Pago: {tarjeta} (Cuotas: {gasto.Cuotas})");
            }
        }

        // --- NUEVA FUNCIONALIDAD 1: REPORTES ---
        public void GenerarReportePorCategoria()
        {
            Console.WriteLine("\n--- REPORTE DE GASTOS POR CATEGORÍA ---");
            if (Gastos.Count == 0)
            {
                Console.WriteLine("No hay gastos registrados para analizar.");
                return;
            }

            var reporte = Gastos
                .Where(g => g.Categoria != null)
                .GroupBy(g => g.Categoria.Nombre)
                .Select(grupo => new
                {
                    Categoria = grupo.Key,
                    TotalGastado = grupo.Sum(g => g.Monto)
                })
                .OrderByDescending(r => r.TotalGastado);

            foreach (var item in reporte)
            {
                Console.WriteLine($"- {item.Categoria}: ${item.TotalGastado:F2}");
            }
            Console.WriteLine("---------------------------------------");
        }

        // --- NUEVA FUNCIONALIDAD 2: PROYECCIÓN DE TARJETA ---
        public void ProyectarResumenTarjeta(int idTarjeta)
        {
            var tarjeta = Tarjetas.FirstOrDefault(t => t.Id == idTarjeta);
            if (tarjeta == null)
            {
                Console.WriteLine("Tarjeta no encontrada.");
                return;
            }

            Console.WriteLine($"\n--- PROYECCIÓN RESUMEN: {tarjeta.Nombre} {tarjeta.Banco} ---");
            Console.WriteLine($"Cierra el día {tarjeta.DiaCierre} | Vence el día {tarjeta.DiaVencimiento}");

            var gastosTarjeta = Gastos.Where(g => g.TarjetaCreditoId == idTarjeta).ToList();

            if (gastosTarjeta.Count == 0)
            {
                Console.WriteLine("No hay consumos registrados en esta tarjeta.");
                return;
            }

            double totalProximoResumen = 0;
            double deudaTotal = 0;

            Console.WriteLine("\nDetalle de consumos a pagar este mes:");
            foreach (var gasto in gastosTarjeta)
            {
                double valorCuota = gasto.Monto / gasto.Cuotas;
                totalProximoResumen += valorCuota;
                deudaTotal += gasto.Monto;

                Console.WriteLine($"> {gasto.FechaHora:dd/MM} | {gasto.Descripcion} | Total: ${gasto.Monto} | Cuota a pagar: ${valorCuota:F2} (de {gasto.Cuotas} cuotas)");
            }

            double disponible = tarjeta.LimiteCredito - deudaTotal;

            Console.WriteLine("------------------------------------------------");
            Console.WriteLine($"TOTAL ESTIMADO PRÓXIMO RESUMEN: ${totalProximoResumen:F2}");
            Console.WriteLine($"Límite disponible para compras: ${disponible:F2} (de ${tarjeta.LimiteCredito})");
        }
    }
}