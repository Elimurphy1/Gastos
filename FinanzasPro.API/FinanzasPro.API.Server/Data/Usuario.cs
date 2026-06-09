using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinanzas
{
    public class Usuario
    {

        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Email { get; set; } = string.Empty;

        // Un usuario es "dueño" de todas estas listas
        public List<Gasto> Gastos { get; set; } = new List<Gasto>();
        public List<Ingreso> Ingresos { get; set; } = new List<Ingreso>();
        public List<Categoria> Categorias { get; set; } = new List<Categoria>();
        public List<TarjetaCredito> Tarjetas { get; set; } = new List<TarjetaCredito>();

    }
}
