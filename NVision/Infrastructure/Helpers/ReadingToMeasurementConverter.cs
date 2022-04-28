using Core.Models;
using Core.Repositories;
using Infrastructure.DTOs;
using System;
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
            var sensorType = StringToEnumParser<SensorType>.ToEnum(sensorReading.Type);

            return new SensorMeasurement()
            {
                SubjectId = subjectId,
                Timestamp = DateTime.Parse(sensorReading.Timestamp),
                Value = sensorReading.Value,
                SensorType = sensorType
            };
        }

        
    }
}
