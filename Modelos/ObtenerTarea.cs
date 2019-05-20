using System;

namespace Modelos
{
    public class ObtenerTarea
    {
        public int Codigo { get; set; }
        public DateTime FechaDeCreacion { get; set; }
        public DateTime UltimaActualizacion { get; set; }
        public string Descripcion { get; set; }
        public bool Finalizada { get; set; }
        public DateTime FechaVencimiento { get; set; }
        public int CodigoUsuario { get; set; }
    }
}
