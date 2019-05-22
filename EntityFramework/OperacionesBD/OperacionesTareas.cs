using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntityFramework.ClasesEntidad;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.OperacionesBD
{
    public class OperacionesTareas : IOperacionesTareas
    {
        private TareasDbContext _context;

        public OperacionesTareas(TareasDbContext context)
        {
            _context = context;
        }

        public async Task<Tarea> ActualizarTarea(Tarea actualizarTarea)
        {
            var entry = _context.Tarea.Update(actualizarTarea);
            entry.State = EntityState.Modified;
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task<Tarea> CrearTarea(Tarea crearTarea)
        {
            var entry = await _context.Tarea.AddAsync(crearTarea);
            await _context.SaveChangesAsync();
            return entry.Entity;
        }

        public async Task EliminarTarea(Tarea eliminarTarea)
        {
            _context.Tarea.Remove(eliminarTarea);
            await _context.SaveChangesAsync();
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

        public async Task<List<Tarea>> ObtenerTodo()
        {
            return await _context.Tarea.ToListAsync();
        }

        public void Guardar()
        {
            _context.SaveChanges();
        }
    }
}
