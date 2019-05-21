using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework.ClasesEntidad
{
    public class Tarea
    {
        [ForeignKey("Usuario")]
        public int UsuarioId { get; set; }
        public Usuario Usuario { get; set; }

        public int Codigo { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        public DateTime UltimaActualizacion { get; set; }
        public string Descripcion { get; set; }
        public bool Finalizada { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}
