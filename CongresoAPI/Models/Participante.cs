namespace CongresoAPI.Models
{
    public class Participante
    {
        public int Id { get; set; }
        public string Nombre { get; set; } = "";
        public string Apellidos { get; set; } = "";
        public string Email { get; set; } = "";
        public string Twitter { get; set; } = "";
        public string Ocupacion { get; set; } = "";
        public string Avatar { get; set; } = ""; // url o base64 (recomendado url)
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;
    }
}
