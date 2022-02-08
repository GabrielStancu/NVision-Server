using AutoMapper;
using Core.Models;
using Core.Repositories;
using Infrastructure.DTOs;
using Infrastructure.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IWatcherDashboardService
    {
        Task<WatcherDashboardDataDto> GetWatcherDashboardDataAsync(int watcherId);
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

        public async Task<WatcherDashboardDataDto> GetWatcherDashboardDataAsync(int watcherId)
        {
            var cards = await GetDashboardCardsAsync(watcherId);
            var subjects = await GetDashboardSubjectsAsync(watcherId);
            var alerts = await GetDashboardAlertsAsync(watcherId);

            return new WatcherDashboardDataDto
            {
                Cards = cards,
                Subjects = subjects,
                Alerts = alerts
            };
        }

        private async Task<IEnumerable<DashboardCardDataDto>> GetDashboardCardsAsync(int watcherId)
        {
            return new List<DashboardCardDataDto>
            {
                new DashboardCardDataDto
                {
                    NumericValue = await GetWatchedSubjectsCountAsync(watcherId),
                    PropertyName = "Watched Subjects"
                },
                new DashboardCardDataDto
                {
                    NumericValue = await GetWatcherMeasurementsAsync(watcherId),
                    PropertyName = "Measurements This Week"
                },
                new DashboardCardDataDto
                {
                    NumericValue = await GetWatcherAlertsCountAsync(watcherId),
                    PropertyName = "Alerts This Week"
                },
                new DashboardCardDataDto
                {
                    NumericValue = await GetWatcherMeasuredSubjectsCountAsync(watcherId),
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

        private async Task<int> GetWatcherMeasurementsAsync(int watcherId)
        {
            int count = await _sensorMeasurementRepository.GetWatcherMeasurementsCountLastDaysAsync(watcherId);
            return count;
        }

        private async Task<int> GetWatcherAlertsCountAsync(int watcherId)
        {
            int count = await _alertRepository.GetWatcherAlertsCountAsync(watcherId);
            return count;
        }

        private async Task<int> GetWatcherMeasuredSubjectsCountAsync(int watcherId)
        {
            var subjects = await _subjectRepository.GetWatcherSubjectsAsync(watcherId);
            var count = await _sensorMeasurementRepository.GetMeasuredSubjectsCountAsync(subjects);
            return count;
        }
    }
}
