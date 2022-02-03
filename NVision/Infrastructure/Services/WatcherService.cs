using AutoMapper;
using Core.Models;
using Core.Repositories;
using Infrastructure.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IWatcherService
    {
        Task<WatcherDashboardDataDto> GetWatcherDashboardDataAsync(int watcherId);
        Task<IEnumerable<AlertDto>> GetWatcherAlertsAsync(int watcherId);
        Task<bool> AnswerAlertAsync(AlertAnswerDto alertAnswerDto);
    }

    public class WatcherService : IWatcherService
    {
        private readonly IWatcherDashboardService _watcherDashboardService;
        private readonly IAlertRepository _alertRepository;
        private readonly IMapper _mapper;

        public WatcherService(IWatcherDashboardService watcherDashboardService, IAlertRepository alertRepository, IMapper mapper)
        {
            _watcherDashboardService = watcherDashboardService;
            _alertRepository = alertRepository;
            _mapper = mapper;
        }
        public async Task<WatcherDashboardDataDto> GetWatcherDashboardDataAsync(int watcherId)
        {
            var dashboardData = await _watcherDashboardService.GetWatcherDashboardDataAsync(watcherId);
            return dashboardData;
        }
        public async Task<IEnumerable<AlertDto>> GetWatcherAlertsAsync(int watcherId)
        {
            var alerts = await _alertRepository.GetWatcherAlertsAsync(watcherId);
            var alertDtos = new List<AlertDto>();

            foreach (var alert in alerts)
            {
                alertDtos.Add(_mapper.Map<Alert, AlertDto>(alert));
            }
            return alertDtos;
        }

        public async Task<bool> AnswerAlertAsync(AlertAnswerDto alertAnswerDto)
        {
            bool answeredAlert = await _alertRepository.AnswerAlertAsync(alertAnswerDto.AlertId, alertAnswerDto.WasTrueAlert);
            return answeredAlert;
        }
    }
}
