using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntityFramework.ClasesEntidad;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.OperacionesBD
{
    public class OperacionesTareas : IOperacionesTareas
    {
        internal DbContext _context;

        public OperacionesTareas(DbContext context)
        {
            _context = context;
        }

        public async Task<Tarea> ActualizarTarea(Tarea actualizarTarea)
        {
            _context.Entry(actualizarTarea).State = EntityState.Modified;
            return await Task.FromResult(actualizarTarea);
        }

        public async Task<Tarea> CrearTarea(Tarea crearTarea)
        {
           return (await _context.Set<Tarea>().AddAsync(crearTarea)).Entity;
        }

        public async Task EliminarTarea(Tarea eliminarTarea)
        {
            await Task.Factory.StartNew(() =>
            {
                _context.Set<Tarea>().Remove(eliminarTarea);
            });  
        }

        public async Task<IQueryable<Tarea>> EncontrarTareas(Expression<Func<Tarea, bool>> expresion)
        {
            IQueryable<Tarea> query = _context.Set<Tarea>().Where(expresion);
            return await Task.FromResult(query);
        }

        public async Task<Tarea> ObtenerPorId(int codigoTarea)
        {
            return (await EncontrarTareas(t => t.Codigo == codigoTarea)).FirstOrDefault(); ;
        }

        public async Task<IQueryable<Tarea>> ObtenerTodo()
        {
            IQueryable<Tarea> query = _context.Set<Tarea>();
            return await Task.FromResult(query);
        }

        public void Guardar()
        {
            _context.SaveChanges();
        }
    }
}
