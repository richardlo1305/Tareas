using Modelos;
using System.Threading.Tasks;

namespace Logica
{
    public interface ILogicaUsuarios
    {
        Task<ObtenerUsuario> Loguin(LoginUsuario login);
        Task<ObtenerUsuario> ObtenerUsuarioLogueado();
        Task<ObtenerUsuario> CrearUsuario(CrearUsuario crearUsuario);
        Task EliminarUsuario(int codigoUsuario);
        Task<ObtenerUsuario> ActualizarUsuario(ActualizarUsuario actualizarUsuario);
    }
}
