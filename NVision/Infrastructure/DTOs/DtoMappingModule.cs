using AutoMapper;
using Core.Models;

namespace Infrastructure.DTOs
{
    public class DtoMappingModule : Profile
    {
        public DtoMappingModule()
        {
            CreateMap<Watcher, LoginResultDto>();
            CreateMap<Subject, LoginResultDto>();
            CreateMap<WatcherRegisterRequestDto, Watcher>();
            CreateMap<SubjectRegisterRequestDto, Subject>();
        }
    }
}
