using Modelos;
using EntityFramework.ClasesEntidad;
using EntityFramework.OperacionesBD;
using System.Threading.Tasks;
using System.Linq;
using System;
using System.Security.Claims;
using Newtonsoft.Json;
using AutoMapper;

namespace Logica
{
    public class LogicaUsuarios : ILogicaUsuarios
    {
        private readonly IOperacionesUsuarios _operaciones;
        private readonly IMapper _mapper;

        public LogicaUsuarios(IOperacionesUsuarios operaciones,
            IMapper mapper)
        {
            _operaciones = operaciones;
            _mapper = mapper;
        }

        /// <summary>
        /// Función para loguearse el usuario
        /// </summary>
        /// <param name="login"></param>
        /// <returns></returns>
        public async Task<ObtenerUsuario> Loguin(LoginUsuario login)
        {
            try
            {
                var usuario = (await _operaciones.EncontrarUsuario(u => u.User == login.User && u.Password == login.Password)).FirstOrDefault();
                if(usuario != null)
                {
                    return _mapper.Map<ObtenerUsuario>(usuario);
                }

                return null;
            }
            catch (Exception e)
            {
                Console.WriteLine("Excepcion : " + e.Message);
                throw;
            }
        }

        /// <summary>
        /// Función para obtener el usuario que se encuentra logueado en el momento
        /// </summary>
        /// <returns></returns>
        public async Task<ObtenerUsuario> ObtenerUsuarioLogueado()
        {
            try
            {
                ClaimsIdentity claims = new ClaimsIdentity("Login");
                var jsonUserInfo = claims.Claims.FirstOrDefault().Value;

                var usuario = JsonConvert.DeserializeObject<ObtenerUsuario>(jsonUserInfo);
                return await Task.FromResult(usuario);
            }
            catch (Exception e)
            {
                Console.WriteLine("Excepcion : " + e.Message);
                throw;
            }
        }

        /// <summary>
        /// Función para crear un usuario
        /// </summary>
        /// <param name="usuario">Usuario a crear</param>
        /// <returns>Retorna el usuario creada</returns>
        public async Task<ObtenerUsuario> CrearUsuario(CrearUsuario crearUsuario)
        {
            try
            {
                var buscarUsuario = (await _operaciones.EncontrarUsuario(u => u.Identificacion == crearUsuario.Identificacion)).FirstOrDefault();
                var usuario = _mapper.Map<Usuario>(crearUsuario);
                if (buscarUsuario == null)
                {
                    await _operaciones.CrearUsuario(usuario);
                }
                return _mapper.Map<ObtenerUsuario>(usuario);
            }
            catch (Exception e)
            {
                Console.WriteLine("Excepcion : " + e.Message);
                throw;
            }
        }

        /// <summary>
        /// Función para eliminar un usuario
        /// </summary>
        /// <param name="codigoUsuario">Código de el usuario a eliminar</param>
        public async Task EliminarUsuario(int codigoUsuario)
        {
            try
            {
                var usuarioBd = await _operaciones.ObtenerPorId(codigoUsuario);
                if (usuarioBd != null)
                {
                    await _operaciones.EliminarUsuario(usuarioBd);
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Excepcion : " + e.Message);
                throw;
            }
        }

        /// <summary>
        /// Función para actualizar usuario
        /// </summary>
        /// <param name="actualizarUsuario">Usuario a actualizar</param>
        /// <returns>Retorna el usuario actualizada</returns>
        public async Task<ObtenerUsuario> ActualizarUsuario(ActualizarUsuario actualizarUsuario)
        {
            try
            {
                var usuarioBd = await _operaciones.ObtenerPorId(actualizarUsuario.Codigo);
                if (usuarioBd != null)
                {
                    var usuario = _mapper.Map<Usuario>(actualizarUsuario);
                    await _operaciones.ActualizarUsuario(usuario);
                }
                return _mapper.Map<ObtenerUsuario>(usuarioBd);
            }
            catch (Exception e)
            {
                Console.WriteLine("Excepcion : " + e.Message);
                throw;
            }
        }
    }
}
