using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace EntityFramework.ClasesEntidad
{
    public class Tarea
    {
        [ForeignKey("Usuario")]
        public int UsuarioRefId { get; set; }
        public Usuario Usuario { get; set; }

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Codigo { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        public DateTime UltimaActualizacion { get; set; }
        public string Descripcion { get; set; }
        public bool Finalizada { get; set; }
        public DateTime FechaVencimiento { get; set; }
    }
}
