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
        internal DbContext _context;

        public OperacionesUsuarios(DbContext context)
        {
            _context = context;
        }

        public async Task ActualizarUsuario(Usuario actualizarUsuario)
        {
            await Task.Factory.StartNew(() =>
            {
                _context.Entry(actualizarUsuario).State = EntityState.Modified;
            });

        }

        public async Task CrearUsuario(Usuario crearUsuario)
        {
            await Task.Factory.StartNew(() =>
            {
                _context.Set<Usuario>().Add(crearUsuario);
            });
        }

        public async Task EliminarUsuario(Usuario eliminarUsuario)
        {
            await Task.Factory.StartNew(() =>
            {
                _context.Set<Usuario>().Remove(eliminarUsuario);
            });
        }

        public async Task<IQueryable<Usuario>> EncontrarUsuario(Expression<Func<Usuario, bool>> expresion)
        {
            IQueryable<Usuario> query = _context.Set<Usuario>().Where(expresion);
            return await Task.FromResult(query);
        }

        public async Task<Usuario> ObtenerPorId(int codigoUsuario)
        {
            return (await EncontrarUsuario(t => t.Codigo == codigoUsuario)).FirstOrDefault(); ;
        }

        public async Task<IQueryable<Usuario>> ObtenerTodo()
        {
            IQueryable<Usuario> query = _context.Set<Usuario>();
            return await Task.FromResult(query);
        }

        public void Guardar()
        {
            _context.SaveChanges();
        }
    }
}
