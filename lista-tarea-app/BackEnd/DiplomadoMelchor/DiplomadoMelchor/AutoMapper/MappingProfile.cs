using AutoMapper;
using Tareas.Dtos;
using Tareas.Entidades;
using Tareas.Enumeradores;

namespace DiplomadoMelchor.AutoMapper
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {

            CreateMap<ProyectoSetDto, ProyectoEntity>()
                .ForMember(campo => campo.EstaActivo, asignar=> asignar.MapFrom(valor=> true));
            CreateMap<ProyectoEntity, ProyectoGetDto>();

            CreateMap<EmpleadoSetDto, EmpleadosEntity>()
                .ForMember(campo => campo.EstaActivo, asignar => asignar.MapFrom(valor => true));
            CreateMap<EmpleadosEntity, EmpleadoGetDto>();

            CreateMap<TareaSetDto, TareaEntity>()
                .ForMember(campo => campo.EstadoTarea, asignar => asignar.MapFrom(valor => EstadoTareaEnum.Pendiente))
                .ForMember(campo => campo.EstaActivo, asignar => asignar.MapFrom(valor => true));
            CreateMap<TareaEntity, TareaGetDto>();

        }
    }
}
