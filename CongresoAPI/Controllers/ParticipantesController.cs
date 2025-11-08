using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CongresoAPI.Models;

namespace CongresoAPI.Controllers
{
    [ApiController]
    [Route("api")]
    public class ParticipantesController : ControllerBase
    {
        private readonly BdCongresoContext _context;

        public ParticipantesController(BdCongresoContext context)
        {
            _context = context;
        }

        // GET: /api/listado
        // Devuelve todos los participantes
        [HttpGet("listado")]
        public async Task<IActionResult> GetListado([FromQuery] string? q)
        {
            var query = _context.Participantes.AsQueryable();

            // 2. /api/listado?q=:query
            if (!string.IsNullOrWhiteSpace(q))
            {
                string qLower = q.ToLower();
                query = query.Where(p =>
                    p.Nombre.ToLower().Contains(qLower) ||
                    p.Apellidos.ToLower().Contains(qLower) ||
                    (p.Nombre + " " + p.Apellidos).ToLower().Contains(qLower) ||
                    p.Twitter.ToLower().Contains(qLower) ||
                    p.Ocupacion.ToLower().Contains(qLower)
                );
            }

            var participantes = await query
                .OrderBy(p => p.Nombre)
                .Select(p => new
                {
                    p.Id,
                    p.Nombre,
                    p.Apellidos,
                    p.Email,
                    p.Twitter,
                    p.Ocupacion,
                    p.Avatar
                })
                .ToListAsync();

            return Ok(participantes);
        }

        // 3. GET: /api/participante/{id}
        [HttpGet("participante/{id:int}")]
        public async Task<IActionResult> GetParticipante(int id)
        {
            var participante = await _context.Participantes.FindAsync(id);
            if (participante == null)
                return NotFound(new { message = "Participante no encontrado" });

            return Ok(participante);
        }

        //4. POST: /api/registro
        [HttpPost("registro")]
        public async Task<IActionResult> RegistrarParticipante([FromBody] CrearParticipanteDto dto)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Verificar si el email ya existe
            bool existe = await _context.Participantes.AnyAsync(p => p.Email == dto.Email);
            if (existe)
                return Conflict(new { message = "Ya existe un participante con ese correo electrónico" });

            var nuevo = new Participante
            {
                Nombre = dto.Nombre.Trim(),
                Apellidos = dto.Apellidos.Trim(),
                Email = dto.Email.Trim(),
                Twitter = dto.Twitter?.Trim() ?? "",
                Ocupacion = dto.Ocupacion?.Trim() ?? "",
                Avatar = dto.Avatar?.Trim() ?? "https://i.pravatar.cc/150",
                FechaRegistro = DateTime.UtcNow
            };

            _context.Participantes.Add(nuevo);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetParticipante), new { id = nuevo.Id }, nuevo);
        }
    }

    public class CrearParticipanteDto
    {
        public string Nombre { get; set; } = "";
        public string Apellidos { get; set; } = "";
        public string Email { get; set; } = "";
        public string? Twitter { get; set; }
        public string? Ocupacion { get; set; }
        public string? Avatar { get; set; }
    }
}
