using API.DTOs;
using API.Extensions;
using API.Persistence.Entities;
using AutoMapper;

namespace API.Common;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        CreateMap<TaskDTO, TaskEntity>()
            .ForMember(x => x.Id, opt => opt.MapFrom(y => y.Id))
            .ForMember(x => x.Name, opt => opt.MapFrom(y => y.Name))
            .ForMember(x => x.Status, opt => opt.MapFrom(y => y.Status))
            .ForMember(x => x.Priority, opt => opt.MapFrom(y => y.PriorityValue));

        CreateMap<TaskEntity, TaskDTO>()
            .ForMember(x => x.Id, opt => opt.MapFrom(y => y.Id))
            .ForMember(x => x.Name, opt => opt.MapFrom(y => y.Name))
            .ForMember(x => x.Status, opt => opt.MapFrom(y => y.Status))
            .ForMember(x => x.StatusText, opt => opt.MapFrom(y => y.Status == null ? string.Empty : ((StatusesEnum)y.Status).GetName()))
            .ForMember(x => x.PriorityValue, opt => opt.MapFrom(y => y.Priority));
    }
}
