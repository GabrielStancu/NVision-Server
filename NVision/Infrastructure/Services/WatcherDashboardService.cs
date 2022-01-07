using AutoMapper;
using Core.Models;
using Core.Repositories;
using Infrastructure.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class WatcherDashboardService : IWatcherDashboardService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IAlertRepository _alertRepository;
        private readonly IMapper _mapper;

        public WatcherDashboardService(
            ISubjectRepository subjectRepository, 
            IAlertRepository alertRepository,
            IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _alertRepository = alertRepository;
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
            await Task.Delay(10);
            return new List<DashboardCardDataDto>
            {
                new DashboardCardDataDto
                {
                    NumericValue = 2,
                    PropertyName = "Watched Subjects"
                },
                new DashboardCardDataDto
                {
                    NumericValue = 230,
                    PropertyName = "Measurements This Week"
                },
                new DashboardCardDataDto
                {
                    NumericValue = 5,
                    PropertyName = "Alerts This Week"
                },
                new DashboardCardDataDto
                {
                    NumericValue = 1,
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
    }
}
