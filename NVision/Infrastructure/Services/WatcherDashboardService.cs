using AutoMapper;
using Core.Models;
using Core.Repositories;
using Infrastructure.DTOs;
using Infrastructure.Helpers;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IWatcherDashboardService
    {
        Task<WatcherDashboardDataDto> GetWatcherDashboardDataAsync(WatcherTimeDto watcherTime);
    }
    public class WatcherDashboardService : IWatcherDashboardService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IAlertRepository _alertRepository;
        private readonly ISensorMeasurementRepository _sensorMeasurementRepository;
        private readonly IProfilePictureUrlResolver _profilePictureUrlResolver;
        private readonly IMapper _mapper;

        public WatcherDashboardService(
            ISubjectRepository subjectRepository, 
            IAlertRepository alertRepository,
            ISensorMeasurementRepository sensorMeasurementRepository,
            IProfilePictureUrlResolver profilePictureUrlResolver,
            IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _alertRepository = alertRepository;
            _sensorMeasurementRepository = sensorMeasurementRepository;
            _profilePictureUrlResolver = profilePictureUrlResolver;
            _mapper = mapper;
        }

        public async Task<WatcherDashboardDataDto> GetWatcherDashboardDataAsync(WatcherTimeDto watcherTime)
        {
            var cards = await GetDashboardCardsAsync(watcherTime);
            var subjects = await GetDashboardSubjectsAsync(watcherTime.WatcherId);
            var alerts = await GetDashboardAlertsAsync(watcherTime.WatcherId);

            return new WatcherDashboardDataDto
            {
                Cards = cards,
                Subjects = subjects,
                Alerts = alerts
            };
        }

        private async Task<IEnumerable<DashboardCardDataDto>> GetDashboardCardsAsync(WatcherTimeDto watcherTime)
        {
            return new List<DashboardCardDataDto>
            {
                new DashboardCardDataDto
                {
                    NumericValue = await GetWatchedSubjectsCountAsync(watcherTime.WatcherId),
                    PropertyName = "Watched Subjects"
                },
                new DashboardCardDataDto
                {
                    NumericValue = await GetWatcherMeasurementsAsync(watcherTime),
                    PropertyName = "Measurements Today"
                },
                new DashboardCardDataDto
                {
                    NumericValue = await GetWatcherAlertsCountAsync(watcherTime),
                    PropertyName = "Alerts This Week"
                },
                new DashboardCardDataDto
                {
                    NumericValue = await GetWatcherMeasuredSubjectsCountAsync(watcherTime),
                    PropertyName = "Subjects Measured Today"
                }
            };
        }

        private async Task<IEnumerable<DashboardSubjectDataDto>> GetDashboardSubjectsAsync(int watcherId)
        {
            var subjects = await _subjectRepository.GetWatcherDashboardSubjectsAsync(watcherId);
            var dashboardSubjects = new List<DashboardSubjectDataDto>();

            foreach (var subject in subjects)
            {
                subject.ProfilePictureSrc = _profilePictureUrlResolver.Resolve(subject);
                var dashboardSubject = _mapper.Map<Subject, DashboardSubjectDataDto>(subject);
                dashboardSubjects.Add(dashboardSubject);
            }

            return dashboardSubjects;
        }

        private async Task<IEnumerable<DashboardAlertDataDto>> GetDashboardAlertsAsync(int watcherId)
        {
            var alerts = await _alertRepository.GetWatcherDashboardAlertsAsync(watcherId);
            var dashboardAlerts = new List<DashboardAlertDataDto>();

            foreach(var alert in alerts)
            {
                var dashboardAlert = _mapper.Map<Alert, DashboardAlertDataDto>(alert);
                dashboardAlerts.Add(dashboardAlert);
            }

            return dashboardAlerts;
        }

        private async Task<int> GetWatchedSubjectsCountAsync(int watcherId)
        {
            int count = await _subjectRepository.GetWatcherSubjectsCountAsync(watcherId);
            return count;
        }

        private async Task<int> GetWatcherMeasurementsAsync(WatcherTimeDto watcherTime)
        {
            int count = await _sensorMeasurementRepository.GetWatcherMeasurementsCountLastDaysAsync(watcherTime.WatcherId, watcherTime.CurrentDate);
            return count;
        }

        private async Task<int> GetWatcherAlertsCountAsync(WatcherTimeDto watcherTime)
        {
            int count = await _alertRepository.GetWatcherAlertsCountAsync(watcherTime.WatcherId, watcherTime.CurrentDate);
            return count;
        }

        private async Task<int> GetWatcherMeasuredSubjectsCountAsync(WatcherTimeDto watcherTime)
        {
            var subjects = await _subjectRepository.GetWatcherSubjectsAsync(watcherTime.WatcherId);
            var count = await _sensorMeasurementRepository.GetMeasuredSubjectsCountAsync(subjects, watcherTime.CurrentDate);
            return count;
        }
    }
}
