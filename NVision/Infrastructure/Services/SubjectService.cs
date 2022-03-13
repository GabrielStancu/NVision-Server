using AutoMapper;
using Core.Models;
using Core.Repositories;
using Infrastructure.DTOs;
using Infrastructure.Helpers;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface ISubjectService
    {
        Task<SubjectProfileDataDto> GetProfileDataAsync(int subjectId);
        Task<bool> SaveChangesAsync(SubjectProfileDataDto subjectProfile);
        Task<MeasurementsReplyDto> GetMeasurementsAsync(MeasurementsRequestDto request);
    }

    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IWatcherRepository _watcherRepository;
        private readonly ISensorMeasurementRepository _sensorMeasurementRepository;
        private readonly IMapper _mapper;
        private readonly IProfilePictureUrlResolver _resolver;

        public SubjectService(
            ISubjectRepository subjectRepository, 
            IWatcherRepository watcherRepository,
            ISensorMeasurementRepository sensorMeasurementRepository,
            IMapper mapper,
            IProfilePictureUrlResolver resolver)
        {
            _subjectRepository = subjectRepository;
            _watcherRepository = watcherRepository;
            _sensorMeasurementRepository = sensorMeasurementRepository;
            _mapper = mapper;
            _resolver = resolver;
        }
        public async Task<SubjectProfileDataDto> GetProfileDataAsync(int subjectId)
        {
            var subject = await _subjectRepository.GetSubjectWithWatcherAsync(subjectId);
            subject.ProfilePictureSrc = _resolver.Resolve(subject);
            var subjectProfile = _mapper.Map<Subject, SubjectProfileDataDto>(subject);
            var watchers = await _watcherRepository.SelectAllAsync();
            foreach (var watcher in watchers)
            {
                var watcherOption = _mapper.Map<Watcher, WatcherOptionDto>(watcher);
                (subjectProfile.Watchers as List<WatcherOptionDto>).Add(watcherOption);
            }
            return subjectProfile;
        }
        public async Task<bool> SaveChangesAsync(SubjectProfileDataDto subjectProfile)
        {
            if (await _watcherRepository.ExistsUserAsync(subjectProfile.Id, subjectProfile.Username) ||
                await _subjectRepository.ExistsUserAsync(subjectProfile.Id, subjectProfile.Username))
                return false;
            if (!subjectProfile.Password.Equals(subjectProfile.RepeatedPassword))
                return false;

            var subject = _mapper.Map<SubjectProfileDataDto, Subject>(subjectProfile);
            subject.Watcher = null;
            await _subjectRepository.UpdateAsync(subject);
            return true;
        }
        public async Task<MeasurementsReplyDto> GetMeasurementsAsync(MeasurementsRequestDto request)
        {
            var subject = await _subjectRepository.SelectByIdAsync(request.SubjectId);
            var measurements = await _sensorMeasurementRepository.GetFilteredMeasurementsAsync(request.SensorTypes, request.StartDate, request.EndDate);
            var summarySubjectData = _mapper.Map<Subject, SubjectSummarizedDataDto>(subject);
            var mappedMeasurements = new List<SensorMeasurementDto>();

            foreach (var measurement in measurements)
            {
                var mappedMeasurement = _mapper.Map<SensorMeasurement, SensorMeasurementDto>(measurement);
                mappedMeasurements.Add(mappedMeasurement);
            }

            return new MeasurementsReplyDto
            {
                SummarizedDataDto = summarySubjectData,
                Measurements = mappedMeasurements
            };
        }
    }
}
