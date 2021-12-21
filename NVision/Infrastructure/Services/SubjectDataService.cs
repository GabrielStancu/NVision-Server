using AutoMapper;
using Core.Models;
using Core.Repositories;
using Infrastructure.DTOs;
using Infrastructure.Filtering.Filters;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class SubjectDataService : ISubjectDataService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly ISensorMeasurementFilter _sensorMeasurementFilter;
        private readonly IMapper _mapper;

        public SubjectDataService(
            ISubjectRepository subjectRepository,
            ISensorMeasurementFilter sensorMeasurementFilter,
            IMapper mapper)
        {
            _subjectRepository = subjectRepository;
            _sensorMeasurementFilter = sensorMeasurementFilter;
            _mapper = mapper;
        }
        public async Task<SubjectWithMeasurementsReplyDto> GetSubjectWithMeasurementsAsync(SubjectWithMeasurementsRequestDto request)
        {
            var subject = await _subjectRepository.GetSubjectWithMeasurementsAsync(request.SubjectId);
            subject.SensorMeasurements = _sensorMeasurementFilter.Filter(subject.SensorMeasurements, request.SensorMeasurementSpecificationDto);

            return _mapper.Map<Subject, SubjectWithMeasurementsReplyDto>(subject);
        }
    }
}
