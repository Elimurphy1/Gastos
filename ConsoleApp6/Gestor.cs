using System;
using System.Collections.Generic;
using System.Text;

namespace ConsoleApp6
{
    internal class Gestor
    {

        List<Productos> Producto = new List<Productos>();
        List<Productos> Tarjeta = new List<Productos>();

        public void AgregarIngreso(double Ingreso)
        {
            Productos ingreso = new Productos();

            ingreso.Ingreso = Ingreso;

            Producto.Add(ingreso);            

        }
        public void AgregarGasto(double Gasto)
        {
            Productos gasto = new Productos();

            gasto.Gasto = Gasto;

            Producto.Add(gasto);
        }
        public void AgregarTarjeta(String Tarjeta)
        {
            Productos tarjeta = new Productos();
            tarjeta.Tarjeta = Tarjeta;
            Producto.Add(tarjeta);
        }
        public void VerGastos()
        {

            Productos totalGastos = new Productos();

            double total = 0;

            foreach(Productos g in Producto)
            {
                total += g.Gasto;
            }

            Console.WriteLine("El valor total de los gastos es: " + total + "\n");

        }

        public void DineroRestante()
        {
            Productos dineroRestante = new Productos();

            double totalGasto = 0;
            double totalDinero = 0;
            foreach (Productos g in Producto)
            {
                totalGasto += g.Gasto;
                totalDinero += g.Ingreso;
            }

            double dineroRes = totalDinero - totalGasto;

            Console.WriteLine("El dinero restante es: " + dineroRes);

        }

        public void UsaTarjeta(double Gasto)
        {
            Productos tarjeta = new Productos();

            tarjeta.Gasto= Gasto;

            Producto.Add(tarjeta);

        }

        public void CuotasTarjeta(double Cuotas)
        {
            Productos tarjeta = new Productos();

        }




    }
}
