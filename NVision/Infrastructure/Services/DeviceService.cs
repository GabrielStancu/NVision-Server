using AutoMapper;
using Core.Models;
using Core.Repositories;
using Infrastructure.DTOs;
using System;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IDeviceService
    {
        Task<SubjectDeviceDataDto> GetSubjectDeviceDataAsync(Guid serialNumber);
    }

    public class DeviceService : IDeviceService
    {
        private readonly IDeviceRepository _deviceRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public DeviceService(
            IDeviceRepository deviceRepository,
            ISubjectRepository subjectRepository,
            IMapper mapper)
        {
            _deviceRepository = deviceRepository;
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }
        public async Task<SubjectDeviceDataDto> GetSubjectDeviceDataAsync(Guid serialNumber)
        {
            var subjectId = await _deviceRepository.GetOwnerSubjectIdAsync(serialNumber);
            var subject = await _subjectRepository.SelectByIdAsync(subjectId);
            var subjectDeviceData = _mapper.Map<Subject, SubjectDeviceDataDto>(subject);
            return subjectDeviceData;
        }
    }
}
