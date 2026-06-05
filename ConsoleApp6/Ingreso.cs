using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinanzas
{
    internal class Ingreso
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public double Monto { get; set; }

        public DateTime FechaHora { get; set; } // Guarda día, mes, año, hora, minuto y segundo

        // Relación con Categoría
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;

        public int UsuarioId { get; set; }
    }
}
