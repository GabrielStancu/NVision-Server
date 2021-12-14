using AutoMapper;
using Core.Models;
using Core.Repositories;
using Infrastructure.DTOs;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class WatcherDataService : IWatcherDataService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IAlertRepository _alertRepository;
        private readonly IMapper _mapper;

        public WatcherDataService(
            ISubjectRepository subjectRepository, 
            IAlertRepository alertRepository,
            IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _alertRepository = alertRepository;
            _mapper = mapper;
        }

        public async Task<WatcherHomepageDataDto> GetWatcherHomepageDataAsync(int watcherId)
        {
            var subjects = await GetSubjectsAsync(watcherId);
            var alerts = await GetAlertsAsync(watcherId);
            int subjectsCount = await GetSubjectsCount(watcherId);

            return new WatcherHomepageDataDto
            {
                Alerts = alerts,
                Subjects = subjects,
                SubjectsCount = subjectsCount
            };
        }

        public async Task<SubjectWithMeasurementsDto> GetSubjectWithMeasurementsAsync(int subjectId)
        {
            var subject = await _subjectRepository.GetSubjectWithMeasurementsAsync(subjectId);

            return _mapper.Map<Subject, SubjectWithMeasurementsDto>(subject);
        }

        private async Task<IEnumerable<SubjectWithoutMeasurementsDto>> GetSubjectsAsync(int watcherId)
        {
            var subjects = await _subjectRepository.GetWatcherSubjectsAsync(watcherId);
            var subjectsWithoutMeasurements = new List<SubjectWithoutMeasurementsDto>();

            foreach (var subject in subjects)
            {
                subjectsWithoutMeasurements.Add(_mapper.Map<Subject, SubjectWithoutMeasurementsDto>(subject));
            }

            return subjectsWithoutMeasurements;
        }

        private async Task<IEnumerable<AlertDto>> GetAlertsAsync(int watcherId)
        {
            var alerts = await _alertRepository.GetWatcherAlerts(watcherId);
            var alertsDto = new List<AlertDto>();

            foreach (var alert in alerts)
            {
                alertsDto.Add(_mapper.Map<Alert, AlertDto>(alert));
            }

            return alertsDto;
        }

        private async Task<int> GetSubjectsCount(int watcherId)
        {
            return await _subjectRepository.GetWatcherSubjectsCount(watcherId);
        }
    }
}
