using Core.Models;
using Core.Repositories;
using Infrastructure.DTOs;
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
            int subjectId = await _deviceRepository.GetOwnerSubjectIdAsync(sensorReading.DeviceSerialNumber);
            return new SensorMeasurement(sensorReading.SensorType)
            {
                SubjectId = subjectId,
                Timestamp = sensorReading.Timestamp,
                Value = sensorReading.Value
            };
        }
    }
}
