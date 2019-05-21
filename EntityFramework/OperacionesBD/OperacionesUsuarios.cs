using System;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using EntityFramework.ClasesEntidad;
using Microsoft.EntityFrameworkCore;

namespace EntityFramework.OperacionesBD
{
    public class OperacionesUsuarios : IOperacionesUsuarios
    {
        private TareasDbContext _context;

        public OperacionesUsuarios(TareasDbContext context)
        {
            _context = context;
        }

        public async Task ActualizarUsuario(Usuario actualizarUsuario)
        {
            _context.Usuario.Update(actualizarUsuario).State = EntityState.Modified;
            await _context.SaveChangesAsync();
        }

        public async Task CrearUsuario(Usuario crearUsuario)
        {
            await _context.Usuario.AddAsync(crearUsuario);
            await _context.SaveChangesAsync();
        }

        public async Task EliminarUsuario(Usuario eliminarUsuario)
        {
            _context.Usuario.Remove(eliminarUsuario);
            await _context.SaveChangesAsync();
        }

        public async Task<IQueryable<Usuario>> EncontrarUsuario(Expression<Func<Usuario, bool>> expresion)
        {
            IQueryable<Usuario> query = _context.Usuario.Where(expresion);
            return await Task.FromResult(query);
        }

        public async Task<Usuario> ObtenerPorId(int codigoUsuario)
        {
            return (await EncontrarUsuario(t => t.Codigo == codigoUsuario)).FirstOrDefault(); ;
        }

        public async Task<IQueryable<Usuario>> ObtenerTodo()
        {
            IQueryable<Usuario> query = _context.Usuario;
            return await Task.FromResult(query);
        }

        public void Guardar()
        {
            _context.SaveChanges();
        }
    }
}
