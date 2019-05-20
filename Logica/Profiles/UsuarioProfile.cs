using AutoMapper;
using Modelos;
using EntityFramework.ClasesEntidad;

namespace Logica.Profiles
{
    public class UsuarioProfile : Profile
    {
        public UsuarioProfile()
        {
            CreateMap<CrearUsuario, Usuario>();
            CreateMap<ActualizarUsuario, Usuario>();
            CreateMap<Usuario, ObtenerUsuario>();
        }
    }
}
