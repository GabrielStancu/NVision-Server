using AutoMapper;
using Core.Models;
using Core.Repositories;
using Infrastructure.DTOs;
using Infrastructure.Helpers;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface ISubjectService
    {
        Task<SubjectProfileDataDto> GetProfileDataAsync(int subjectId);
        Task<bool> SaveChangesAsync(SubjectProfileDataDto subjectProfile);
    }

    public class SubjectService : ISubjectService
    {
        private readonly ISubjectRepository _subjectRepository;
        private readonly IWatcherRepository _watcherRepository;
        private readonly IMapper _mapper;
        private readonly IProfilePictureUrlResolver _resolver;

        public SubjectService(
            ISubjectRepository subjectRepository, 
            IWatcherRepository watcherRepository,
            IMapper mapper,
            IProfilePictureUrlResolver resolver)
        {
            _subjectRepository = subjectRepository;
            _watcherRepository = watcherRepository;
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
    }
}
