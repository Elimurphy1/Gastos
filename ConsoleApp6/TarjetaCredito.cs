using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinanzas
{
    internal class TarjetaCredito
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = string.Empty;
        public string Banco { get; set; } = string.Empty;
        public double LimiteCredito { get; set; }
        public int DiaCierre { get; set; }
        public int DiaVencimiento { get; set; }

        // --- LA NUEVA RELACIÓN ---
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; } = null!;

        public List<Gasto> Gastos { get; set; } = new List<Gasto>();


    }
}
