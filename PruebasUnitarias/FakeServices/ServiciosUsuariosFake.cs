using System;
using System.Threading.Tasks;
using EntityFramework;
using EntityFramework.ClasesEntidad;
using EntityFramework.OperacionesBD;
using Logica;
using Modelos;
using System.Linq;

namespace PruebasUnitarias.FakeServices
{
    public class ServiciosUsuariosFake : ILogicaUsuarios
    {
        private TareasDbContext dbContext;

        public ServiciosUsuariosFake()
        {
            var reference = new TareasDbContextFactory();
            dbContext = reference.CreateDbContext(null);
            dbContext.Usuario.Add(new Usuario {  Codigo = 1, Correo = "ricardo.lindarte@unillanos.edu.co", Identificacion = "100234567", Nombre = "Ricardo", Password = "123456", User = "ricardo"});
            dbContext.Usuario.Add(new Usuario { Codigo = 2, Correo = "jorge.molina@unillanos.edu.co", Identificacion = "100256345", Nombre = "Jorge", Password = "123456", User = "jorge" });
            dbContext.SaveChanges();
        }

        public async Task<ObtenerUsuario> ActualizarUsuario(ActualizarUsuario actualizarUsuario)
        {
            var usuario = new Usuario
            {
                Codigo = actualizarUsuario.Codigo,
                Correo = actualizarUsuario.Correo,
                Identificacion = actualizarUsuario.Identificacion,
                Nombre = actualizarUsuario.Nombre,
                Password = actualizarUsuario.Password,
                User = actualizarUsuario.User
            };
            var entry = dbContext.Usuario.Update(usuario);
            try
            {
                await dbContext.SaveChangesAsync();
                var entidad = entry.Entity;
                return new ObtenerUsuario {  Codigo = entidad.Codigo, Correo = entidad.Correo, Identificacion = entidad.Identificacion, Nombre = entidad.Nombre, Password = entidad.Password, User = entidad.User};
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<ObtenerUsuario> CrearUsuario(CrearUsuario crearUsuario)
        {
            var usuario = new Usuario
            {
                Correo = crearUsuario.Correo,
                Identificacion = crearUsuario.Identificacion,
                Nombre = crearUsuario.Nombre,
                Password = crearUsuario.Password,
                User = crearUsuario.User
            };

            var entry = dbContext.Usuario.Add(usuario);

            try
            {
                await dbContext.SaveChangesAsync();
                var entidad = entry.Entity;
                return new ObtenerUsuario { Codigo = entidad.Codigo, Correo = entidad.Correo, Identificacion = entidad.Identificacion, Nombre = entidad.Nombre, Password = entidad.Password, User = entidad.User };
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task EliminarUsuario(int codigoUsuario)
        {
            var usuario = dbContext.Usuario.Where(t => t.Codigo == codigoUsuario).FirstOrDefault();
            if(usuario != null)
            {
                dbContext.Usuario.Remove(usuario);
                try
                {
                    await dbContext.SaveChangesAsync();
                }
                catch (Exception)
                {
                    throw;
                }
            }
            else
            {
                throw new Exception();
            } 
        }

        public async Task<ObtenerUsuario> Loguin(LoginUsuario login)
        {
            var usuario = dbContext.Usuario.Where(t => t.User == login.User && t.Password == login.Password).FirstOrDefault();
            if(usuario != null)
            {
                var obtenerUsuario = new ObtenerUsuario { Codigo = usuario.Codigo, Correo = usuario.Correo, Identificacion = usuario.Identificacion, Nombre = usuario.Nombre, Password = usuario.Password, User = usuario.User };
                return await Task.FromResult(obtenerUsuario);
            }
            return null;
        }

        public Task<ObtenerUsuario> ObtenerUsuarioLogueado()
        {
            throw new System.NotImplementedException();
        }
    }
}
