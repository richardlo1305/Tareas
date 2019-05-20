using EntityFramework.ClasesEntidad;
using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace EntityFramework.OperacionesBD
{
    public interface IOperacionesTareas
    {
        Task<Tarea> CrearTarea(Tarea crearTarea);
        Task EliminarTarea(Tarea eliminarTarea);
        Task<Tarea> ActualizarTarea(Tarea actualizarTarea);
        Task<IQueryable<Tarea>> ObtenerTodo();
        Task<IQueryable<Tarea>> EncontrarTareas(Expression<Func<Tarea, bool>> expresion);
        Task<Tarea> ObtenerPorId(int codigoTarea); 
    }
}
