using System.ComponentModel.DataAnnotations;

namespace EntityFramework.ClasesEntidad
{
    public class Usuario
    {
        [Key]
        public int Codigo { get; set; }

        public string Identificacion { get; set; }
        public string Nombre { get; set; }
        public string Correo { get; set; }
        public string User { get; set; }
        public string Password { get; set; }
    }
}
