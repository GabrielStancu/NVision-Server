using Core.Models;
using Core.Repositories;
using Infrastructure.DTOs;
using System;
using System.Linq;
using System.Runtime.Serialization;
using System.Threading.Tasks;

namespace Infrastructure.Helpers
{
    public interface IReadingToMeasurementConverter
    {
        Task<SensorMeasurement> ConvertAsync(SensorReadingDto sensorReading);
    }

    public class ReadingToMeasurementConverter : IReadingToMeasurementConverter
    {
        private readonly IDeviceRepository _deviceRepository;

        public ReadingToMeasurementConverter(IDeviceRepository deviceRepository)
        {
            _deviceRepository = deviceRepository;
        }
        public async Task<SensorMeasurement> ConvertAsync(SensorReadingDto sensorReading)
        {
            int subjectId = await _deviceRepository.GetOwnerSubjectIdAsync(sensorReading.DeviceSerial);
            var sensorType = ToEnum<SensorType>(sensorReading.Type);

            return new SensorMeasurement()
            {
                SubjectId = subjectId,
                Timestamp = DateTime.Parse(sensorReading.Timestamp),
                Value = sensorReading.Value,
                SensorType = sensorType
            };
        }

        private T ToEnum<T>(string str)
        {
            var enumType = typeof(T);
            foreach (var name in Enum.GetNames(enumType))
            {
                var enumMemberAttribute = ((EnumMemberAttribute[])enumType.GetField(name).GetCustomAttributes(typeof(EnumMemberAttribute), true)).Single();
                if (enumMemberAttribute.Value == str) return (T)Enum.Parse(enumType, name);
            }
            
            return default(T);
        }
    }
}
