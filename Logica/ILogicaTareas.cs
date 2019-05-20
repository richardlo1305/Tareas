using Modelos;
using System.Threading.Tasks;

namespace Logica
{
    public interface ILogicaTareas
    {
        Task<ListaTareas> ObtenerTodasTareas();
        Task<ObtenerTarea> CrearTarea(CrearTarea crearTarea);
        Task EliminarTarea(int codigoTarea);
        Task<ObtenerTarea> ActualizarTarea(ActualizarTarea actualizarTarea);
        Task<ListaTareas> ObtenerPorFiltro(FiltrosTareas filtros);
    }
}
