using AutoMapper;
using Core.Models;
using Core.Repositories;
using Infrastructure.DTOs;
using Infrastructure.Filtering.Filters;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class WatcherDataService : IWatcherDataService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IAlertRepository _alertRepository;
        private readonly IMapper _mapper;
        private readonly IAlertFilter _alertFilter;
        private readonly ISensorMeasurementFilter _sensorMeasurementFilter;
        private readonly ISubjectFilter _subjectFilter;

        public WatcherDataService(
            ISubjectRepository subjectRepository, 
            IAlertRepository alertRepository,
            IMapper mapper,
            IAlertFilter alertFilter,
            ISensorMeasurementFilter sensorMeasurementFilter,
            ISubjectFilter subjectFilter)
        {
            _subjectRepository = subjectRepository;
            _alertRepository = alertRepository;
            _mapper = mapper;
            _alertFilter = alertFilter;
            _sensorMeasurementFilter = sensorMeasurementFilter;
            _subjectFilter = subjectFilter;
        }

        public async Task<WatcherHomepageDataReplyDto> GetWatcherHomepageDataAsync(WatcherHomepageDataRequestDto request)
        {
            var subjects = await GetSubjectsAsync(request);
            var alerts = await GetAlertsAsync(request);
            int subjectsCount = await GetSubjectsCount(request.WatcherId);

            return new WatcherHomepageDataReplyDto
            {
                Alerts = alerts,
                Subjects = subjects,
                SubjectsCount = subjectsCount
            };
        }

        public async Task<AlertDto> AnswerAndGetNextAlertAsync(AlertAnswerDto alertAnswerDto)
        {
            var nextAlert = await _alertRepository.AnswerAndGetNextAlertAsync(alertAnswerDto.AlertId, alertAnswerDto.WasAlertAccurate);

            return _mapper.Map<Alert, AlertDto>(nextAlert);
        }

        private async Task<IEnumerable<SubjectWithoutMeasurementsDto>> GetSubjectsAsync(WatcherHomepageDataRequestDto request)
        {
            var subjects = await _subjectRepository.GetWatcherSubjectsAsync(request.WatcherId);
            var subjectsWithoutMeasurements = new List<SubjectWithoutMeasurementsDto>();

            subjects = _subjectFilter.Filter(subjects, request.SubjectSpecificationDto);
            foreach (var subject in subjects)
            {
                subjectsWithoutMeasurements.Add(_mapper.Map<Subject, SubjectWithoutMeasurementsDto>(subject));
            }

            return subjectsWithoutMeasurements;
        }

        private async Task<IEnumerable<AlertDto>> GetAlertsAsync(WatcherHomepageDataRequestDto request)
        {
            var alerts = await _alertRepository.GetUnansweredWatcherAlerts(request.WatcherId);
            var alertsDto = new List<AlertDto>();

            alerts = _alertFilter.Filter(alerts, request.AlertSpecificationDto);
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
