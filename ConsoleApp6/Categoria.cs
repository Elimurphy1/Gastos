using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinanzas
{
    internal class Categoria
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;

        // Puede ser "Ingreso" o "Gasto"
        public string TipoOperacion { get; set; } = string.Empty;

        public List<Gasto> Gastos { get; set; } = new List<Gasto>();
        public List<Ingreso> Ingresos { get; set; } = new List<Ingreso>();

        public int UsuarioId { get; set; }
    }
}
