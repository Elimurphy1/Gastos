using System;
using System.Collections.Generic;
using System.Text;

namespace ProyectoFinanzas
{
    internal class Gasto
    {
        public int Id { get; set; }
        public string Descripcion { get; set; } = string.Empty;
        public double Monto { get; set; }

        public DateTime FechaHora { get; set; } // Guarda día, mes, año, hora, minuto y segundo

        // Relación obligatoria: Todo gasto debe tener una categoría
        public int CategoriaId { get; set; }
        public Categoria Categoria { get; set; } = null!;

        // Relación opcional: Puede ser en efectivo (sin tarjeta)
        public int? TarjetaCreditoId { get; set; }
        public TarjetaCredito? TarjetaCredito { get; set; }

        public int Cuotas { get; set; }

        public int UsuarioId { get; set; }
    }
}
