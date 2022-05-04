using Core.Models;
using Core.Repositories;
using Infrastructure.DTOs;
using Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IAlertingService
    {
        Task<HealthAlert> CreateAlertAsync(DeviceAlertDto alertDto);
    }

    public class AlertingService : IAlertingService
    {
        private readonly IWatcherRepository _watcherRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IAlertRepository _alertRepository;
        private readonly IDeviceRepository _deviceRepository;

        public AlertingService(
            IWatcherRepository watcherRepository,
            ISubjectRepository subjectRepository,
            IAlertRepository alertRepository,
            IDeviceRepository deviceRepository)
        {
            _watcherRepository = watcherRepository;
            _subjectRepository = subjectRepository;
            _alertRepository = alertRepository;
            _deviceRepository = deviceRepository;
        }

        public async Task<HealthAlert> CreateAlertAsync(DeviceAlertDto alertDto)
        {
            var serialNumber = new Guid(alertDto.DeviceSerialNumber);
            var subjectId = await _deviceRepository.GetOwnerSubjectIdAsync(serialNumber);
            var subject = await _subjectRepository.SelectByIdAsync(subjectId);
            var message = BuildMessage(subject, alertDto);
            var alert = new Alert
            {
                SubjectId = subject.Id,
                Message = message,
                WasTrueAlert = null
            };
            var healthAlert = new HealthAlert
            {
                WatcherPhoneNumber = await _watcherRepository.GetWatcherPhoneNumberByIdAsync(subject.WatcherId.Value),
                Message = message
            };

            await _alertRepository.InsertAsync(alert);

            return healthAlert;
        }

        private string BuildMessage(Subject subject, DeviceAlertDto alertDto)
        {
            var message = $"An alert was detected for {subject.FirstName} {subject.LastName} in {alertDto.AlertMoment.ToShortDateString()} " +
                $"at {alertDto.AlertMoment.ToLongTimeString()}. The remote monitoring hub detected issues with the following parameter" +
                $"{(alertDto.Parameters.Length > 1 ? "s" : "")}: " +
                $"[{ParametersToMessage(alertDto.Parameters)}]. Please contact a specialized medic.";
            // TODO: make a dictionary with combinations of parameters to produce a diagnostic - if possible
            return message;
        }

        private string ParametersToMessage(IEnumerable<string> parameters)
        {
            var sb = new StringBuilder();
            foreach (var parameter in parameters)
            {
                var code = StringToEnumParser<SensorType>.ToEnum(parameter);
                var parameterMessage = ParameterToMessage(code);
                sb.Append(parameterMessage);
                sb.Append("; ");
            }
            sb.Remove(sb.Length - 2, 2);

            return sb.ToString();
        }

        private string ParameterToMessage(SensorType code)
        {
            switch (code)
            {
                case SensorType.Temperature:
                    return "temperature";
                case SensorType.ECG:
                    return "electrocardiogram";
                case SensorType.Pulse:
                    return "pulse";
                case SensorType.OxygenSaturation:
                    return "oxygen saturation";
                case SensorType.GSR:
                    return "galvanic skin response";
                default:
                    return "unknown parameter";
            }
        }
    }
}
