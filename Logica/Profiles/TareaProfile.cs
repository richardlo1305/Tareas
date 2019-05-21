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
                .ForMember(s => s.CodigoUsuario, d => d.MapFrom(t => t.UsuarioId));

            CreateMap<CrearTarea, Tarea>()
                .ForMember(s => s.Codigo, d => d.Ignore())
                .ForMember(s => s.Usuario, d => d.Ignore())
                .ForMember(s => s.UsuarioId, d => d.MapFrom(t => t.CodigoUsuario));

            CreateMap<ActualizarTarea, Tarea>()
                .ForMember(s => s.Usuario, d => d.Ignore())
                .ForMember(s => s.UsuarioId, d => d.Ignore());

            CreateMap<Tarea, ObtenerTarea>()
                .ForMember(s => s.CodigoUsuario, d => d.MapFrom(t => t.UsuarioId));

            CreateMap<List<Tarea>, List<ObtenerTarea>>();
        }
    }
}
