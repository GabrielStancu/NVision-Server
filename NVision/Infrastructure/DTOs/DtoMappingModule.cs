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
            CreateMap<WatcherRegisterRequestDto, Watcher>();
            CreateMap<SubjectRegisterRequestDto, Subject>();
            CreateMap<Subject, SubjectWithoutMeasurementsDto>();
            CreateMap<Subject, SubjectWithMeasurementsDto>();
            CreateMap<AirflowSensorMeasurement, SensorMeasurementDto>()
                .ForMember(dest => dest.SensorName, 
                    map => map.MapFrom(
                        src => src.GetType().Name.Replace(_sensorMeasurementPrefix, string.Empty)));
            CreateMap<BloodPressureSensorMeasurement, SensorMeasurementDto>()
                .ForMember(dest => dest.SensorName,
                    map => map.MapFrom(
                        src => src.GetType().Name.Replace(_sensorMeasurementPrefix, string.Empty)));
            CreateMap<EcgSensorMeasurement, SensorMeasurementDto>()
                .ForMember(dest => dest.SensorName,
                    map => map.MapFrom(
                        src => src.GetType().Name.Replace(_sensorMeasurementPrefix, string.Empty)));
            CreateMap<GsrSensorMeasurement, SensorMeasurementDto>()
                .ForMember(dest => dest.SensorName,
                    map => map.MapFrom(
                        src => src.GetType().Name.Replace(_sensorMeasurementPrefix, string.Empty)));
            CreateMap<PulseOxygenHeartRateSensorMeasurement, SensorMeasurementDto>()
                .ForMember(dest => dest.SensorName,
                    map => map.MapFrom(
                        src => src.GetType().Name.Replace(_sensorMeasurementPrefix, string.Empty)));
            CreateMap<TemperatureSensorMeasurement, SensorMeasurementDto>()
                .ForMember(dest => dest.SensorName,
                    map => map.MapFrom(
                        src => src.GetType().Name.Replace(_sensorMeasurementPrefix, string.Empty)));
            CreateMap<Alert, AlertDto>();
        }
    }
}
