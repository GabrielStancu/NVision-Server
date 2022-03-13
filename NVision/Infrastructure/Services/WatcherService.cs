using AutoMapper;
using Core.Models;
using Core.Repositories;
using Infrastructure.DTOs;
using Infrastructure.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IWatcherService
    {
        Task<WatcherDashboardDataDto> GetWatcherDashboardDataAsync(int watcherId);
        Task<IEnumerable<AlertDto>> GetWatcherAlertsAsync(int watcherId);
        Task<bool> AnswerAlertAsync(AlertAnswerDto alertAnswerDto);
        Task<IEnumerable<SubjectExtendedDataDto>> GetWatcherSubjectsAsync(int watcherId);
        Task<WatcherProfileDataDto> GetProfileDataAsync(int watcherId);
        Task<bool> SaveChangesAsync(WatcherProfileDataDto watcherProfile);
    }

    public class WatcherService : IWatcherService
    {
        private readonly IWatcherDashboardService _watcherDashboardService;
        private readonly IAlertRepository _alertRepository;
        private readonly IWatcherRepository _watcherRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IProfilePictureUrlResolver _profilePictureUrlResolver;
        private readonly IMapper _mapper;

        public WatcherService(
            IWatcherDashboardService watcherDashboardService, 
            IAlertRepository alertRepository, 
            IWatcherRepository watcherRepository,
            ISubjectRepository subjectRepository,
            IProfilePictureUrlResolver profilePictureUrlResolver,
            IMapper mapper)
        {
            _watcherDashboardService = watcherDashboardService;
            _alertRepository = alertRepository;
            _watcherRepository = watcherRepository;
            _subjectRepository = subjectRepository;
            _profilePictureUrlResolver = profilePictureUrlResolver;
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

        public async Task<IEnumerable<SubjectExtendedDataDto>> GetWatcherSubjectsAsync(int watcherId)
        {
            var subjects = await _subjectRepository.GetWatcherSubjectsAsync(watcherId);
            var subjectDtos = new List<SubjectExtendedDataDto>();

            foreach (var subject in subjects)
            {
                subject.ProfilePictureSrc = _profilePictureUrlResolver.Resolve(subject);
                subjectDtos.Add(_mapper.Map<Subject, SubjectExtendedDataDto>(subject));
            }
            return subjectDtos;
        }

        public async Task<WatcherProfileDataDto> GetProfileDataAsync(int watcherId)
        {
            var watcher = await _watcherRepository.SelectByIdAsync(watcherId);
            watcher.ProfilePictureSrc = _profilePictureUrlResolver.Resolve(watcher);
            return _mapper.Map<Watcher, WatcherProfileDataDto>(watcher);
        }

        public async Task<bool> SaveChangesAsync(WatcherProfileDataDto watcherProfile)
        {
            if (await _watcherRepository.ExistsUserAsync(watcherProfile.Id, watcherProfile.Username) ||
                await _subjectRepository.ExistsUserAsync(watcherProfile.Id, watcherProfile.Username))
                return false;
            if (!watcherProfile.Password.Equals(watcherProfile.RepeatedPassword))
                return false;

            var watcher = _mapper.Map<WatcherProfileDataDto, Watcher>(watcherProfile);
            await _watcherRepository.UpdateAsync(watcher);
            return true;
        }
    }
}
