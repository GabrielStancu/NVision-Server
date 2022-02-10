using AutoMapper;
using Core.Models;

namespace Infrastructure.DTOs
{
    public class DtoMappingModule : Profile
    {
        private string _sensorMeasurementPrefix = "SensorMeasurement";
        public DtoMappingModule()
        {
            CreateMap<Watcher, LoginResultDto>();
            CreateMap<Subject, LoginResultDto>();
            CreateMap<UserRegisterRequestDto, Watcher>();
            CreateMap<UserRegisterRequestDto, Subject>();
            CreateMap<Subject, SubjectWithoutMeasurementsDto>();
            CreateMap<Subject, SubjectWithMeasurementsReplyDto>();
            CreateMap<SensorMeasurement, SensorMeasurementDto>()
                .ForMember(dest => dest.SensorName,
                    map => map.MapFrom(
                        src => src.GetType().Name.Replace(_sensorMeasurementPrefix, string.Empty)));
            CreateMap<Alert, AlertDto>()
                .ForMember(dest => dest.SubjectName,
                    map => map.MapFrom(
                        src => src.Subject.FullName));
            CreateMap<Subject, DashboardSubjectDataDto>()
                .ForMember(dest => dest.Name,
                    map => map.MapFrom(
                        src => src.FullName));
            CreateMap<Alert, DashboardAlertDataDto>()
                .ForMember(dest => dest.SubjectName,
                    map => map.MapFrom(
                        src => src.Subject.FullName));
            CreateMap<Watcher, WatcherProfileDataDto>()
                .ForMember(dest => dest.RepeatedPassword,
                    map => map.MapFrom(
                        src => src.Password))
                .ReverseMap();
        }
    }
}
