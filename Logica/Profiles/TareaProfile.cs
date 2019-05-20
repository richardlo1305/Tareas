using AutoMapper;
using Modelos;
using EntityFramework.ClasesEntidad;
using System.Collections.Generic;

namespace Logica.Profiles
{
    public class TareaProfile : Profile
    {
        public TareaProfile()
        {
            CreateMap<Tarea, CrearTarea>()
                .ForMember(s => s.CodigoUsuario, d => d.MapFrom(t => t.UsuarioRefId));

            CreateMap<CrearTarea, Tarea>()
                .ForMember(s => s.Codigo, d => d.Ignore())
                .ForMember(s => s.Usuario, d => d.Ignore())
                .ForMember(s => s.UsuarioRefId, d => d.MapFrom(t => t.CodigoUsuario));

            CreateMap<ActualizarTarea, Tarea>()
                .ForMember(s => s.Usuario, d => d.Ignore())
                .ForMember(s => s.UsuarioRefId, d => d.Ignore());

            CreateMap<Tarea, ObtenerTarea>()
                .ForMember(s => s.CodigoUsuario, d => d.MapFrom(t => t.UsuarioRefId));

            CreateMap<List<Tarea>, List<ObtenerTarea>>();
        }
    }
}
