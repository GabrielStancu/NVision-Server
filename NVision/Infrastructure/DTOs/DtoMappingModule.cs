using AutoMapper;
using Core.Models;
using Infrastructure.Helpers;

namespace Infrastructure.DTOs
{
    public class DtoMappingModule : Profile
    {
        private string _sensorMeasurementPrefix = "SensorMeasurement";
        public DtoMappingModule(IProfilePictureUrlResolver profilePictureUrlResolver)
        {
            CreateMap<Watcher, LoginResultDto>();
            CreateMap<Subject, LoginResultDto>();
            CreateMap<UserRegisterRequestDto, Watcher>();
            CreateMap<UserRegisterRequestDto, Subject>();
            CreateMap<Subject, SubjectWithoutMeasurementsDto>()
                .ForMember(dest => dest.ProfilePictureSrc,
                    map => map.MapFrom(
                        src => profilePictureUrlResolver.Resolve(src)));
            CreateMap<Subject, SubjectWithMeasurementsReplyDto>()
                .ForMember(dest => dest.ProfilePictureSrc,
                    map => map.MapFrom(
                        src => profilePictureUrlResolver.Resolve(src)));
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
                        src => src.FullName))
                .ForMember(dest => dest.ProfilePictureSrc,
                    map => map.MapFrom(
                        src => profilePictureUrlResolver.Resolve(src)));
            CreateMap<Alert, DashboardAlertDataDto>()
                .ForMember(dest => dest.SubjectName,
                    map => map.MapFrom(
                        src => src.Subject.FullName));
        }
    }
}
