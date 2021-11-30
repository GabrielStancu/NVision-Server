using AutoMapper;
using Core.Models;
using Core.Repositories;
using Infrastructure.DTOs;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class RegisterService : IRegisterService
    {
        private readonly IWatcherRepository _watcherRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public RegisterService(
            IWatcherRepository watcherRepository,
            ISubjectRepository subjectRepository,
            IMapper mapper)
        {
            _watcherRepository = watcherRepository;
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }

        public async Task<bool> RegisterUserAsync(WatcherRegisterRequestDto watcherRegisterRequestDto)
        {
            var watcher = _mapper.Map<WatcherRegisterRequestDto, Watcher>(watcherRegisterRequestDto);
            return await RegisterUserAsync(watcher);
        }

        public async Task<bool> RegisterUserAsync(SubjectRegisterRequestDto subjectRegisterRequestDto)
        {
            var subject = _mapper.Map<SubjectRegisterRequestDto, Subject>(subjectRegisterRequestDto);
            return await RegisterUserAsync(subject);
        }

        private async Task<bool> RegisterUserAsync(Watcher watcher)
        {
            var existsWatcher = await _watcherRepository.ExistsUserAsync(watcher.Username);
            var existsSubject = await _subjectRepository.ExistsUserAsync(watcher.Username);

            if (existsWatcher == false && existsSubject == false)
            {
                await _watcherRepository.InsertAsync(watcher);
                return true;
            }

            return false;
        }

        private async Task<bool> RegisterUserAsync(Subject subject)
        {
            var existsWatcher = await _watcherRepository.ExistsUserAsync(subject.Username);
            var existsSubject = await _subjectRepository.ExistsUserAsync(subject.Username);

            if (existsWatcher == false && existsSubject == false)
            {
                await _subjectRepository.InsertAsync(subject);
                return true;
            }

            return false;
        }

    }
}
