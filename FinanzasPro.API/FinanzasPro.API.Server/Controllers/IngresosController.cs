using Microsoft.AspNetCore.Mvc;
using ProyectoFinanzas; // Si al copiar los archivos no les cambiaste el namespace, siguen llamándose así por dentro

namespace FinanzasPro.API.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class IngresosController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public IngresosController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public IActionResult AgregarIngreso([FromBody] Ingreso nuevoIngreso)
        {
            try
            {
                _context.Ingresos.Add(nuevoIngreso);
                _context.SaveChanges();

                return Ok(new { mensaje = $"Ingreso '{nuevoIngreso.Descripcion}' de ${nuevoIngreso.Monto} guardado exitosamente." });
            }
            catch (System.Exception ex)
            {
                return StatusCode(500, new { error = ex.Message });
            }
        }
    }
}