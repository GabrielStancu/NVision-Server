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
                Timestamp = alertDto.AlertMoment.ToUniversalTime(),
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
            var utcAlertMoment = alertDto.AlertMoment.ToUniversalTime();
            var message = $"An alert was detected for {subject.FirstName} {subject.LastName} in {utcAlertMoment.ToShortDateString()} " +
                $"at {utcAlertMoment.ToLongTimeString()} (UTC). The remote monitoring hub detected issues with the following parameter" +
                $"{(alertDto.Parameters.Length > 1 ? "s" : "")}: " +
                $"[{ParametersToMessage(alertDto.Parameters)}]. Please contact a specialized medic.";
            return message;
        }

        private string ParametersToMessage(IEnumerable<string> parameters)
        {
            var sb = new StringBuilder();
            foreach (var parameter in parameters)
            {
                var parameterMessage = CodesDiagnose.ContainsKey(parameter)
                    ? CodesDiagnose[parameter] : "Unknown issue";
                sb.Append(parameterMessage);
                sb.Append("; ");
            }
            sb.Remove(sb.Length - 2, 2);

            return sb.ToString();
        }

        private static Dictionary<string, string> CodesDiagnose { get; set; }
            = new Dictionary<string, string>()
            {
                ["TMP_M_ISO_L"] = "Unexpected low registered temperature",
                ["TMP_M_ISO_H"] = "Unexpected high registered temperature",
                ["TMP_P_ISO_L"] = "Unexpected low predicted temperature",
                ["TMP_P_ISO_H"] = "Unexpected high predicted temperature",
                ["TMP_M_FAR_L"] = "Decreasing registered temperature",
                ["TMP_M_FAR_H"] = "Increasing registered temperature",
                ["TMP_P_FAR_L"] = "Decreasing predicted temperature",
                ["TMP_P_FAR_H"] = "Increasing predicted temperature",

                ["GSR_M_ISO_H"] = "Unexpected high registered level of stress",
                ["GSR_P_ISO_H"] = "Unexpected high predicted level of stress",
                ["GSR_M_FAR_H"] = "Increasing registered level of stress",
                ["GSR_P_FAR_H"] = "Increasing predicted level of stress",

                ["BPM_M_ISO_B"] = "Unexpected low registered pulse (Bradycardia)",
                ["BPM_M_ISO_T"] = "Unexpected high registered pulse (Tachycardia)",
                ["BPM_P_ISO_B"] = "Unexpected low predicted pulse (Bradycardia)",
                ["BPM_P_ISO_T"] = "Unexpected high predicted pulse (Tachycardia)",
                ["BPM_M_FAR_B"] = "Decreasing registered pulse (Bradycardia)",
                ["BPM_M_FAR_T"] = "Increasing high registered pulse (Tachycardia)",
                ["BPM_P_FAR_B"] = "Decreasing predicted pulse (Bradycardia)",
                ["BPM_P_FAR_T"] = "Increasing predicted pulse (Tachycardia)",

                ["OXY_M_ISO_H"] = "Unexpected registered blood oxygen saturation (Hypoxic)",
                ["OXY_M_ISO_SH"] = "Unexpected registered blood oxygen saturation (Severly Hypoxic)",
                ["OXY_P_ISO_H"] = "Unexpected predicted blood oxygen saturation (Hypoxic)",
                ["OXY_P_ISO_SH"] = "Unexpected predicted blood oxygen saturation (Severly Hypoxic)",
                ["OXY_M_FAR_H"] = "Deteriorating registered blood oxygen saturation (Hypoxic)",
                ["OXY_M_FAR_SH"] = "Deteriorating registered blood oxygen saturation (Severly Hypoxic)",
                ["OXY_P_FAR_H"] = "Deteriorating predicted blood oxygen saturation (Hypoxic)",
                ["OXY_P_FAR_SH"] = "Deteriorating predicted blood oxygen saturation (Severly Hypoxic)",

                ["ECG_QRS_BB"] = "Electrocardiogram branch block detected",
                ["ECG_PR_AB"] = "Electrocardiogram atrioventricular block detected",
                ["ECG_QT_S"] = "Electrocardiogram short QT syndrome detected",
                ["ECG_QT_L"] = "Electrocardiogram long QT syndrome detected",
                ["ECG_HRV_T"] = "Low electrocardiogram heart rate (Tachycardia)",
                ["ECG_HRV_B"] = "High electrocardiogram heart rate (Bradycardia)"
            };
    }
}
