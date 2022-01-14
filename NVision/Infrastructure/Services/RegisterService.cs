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

        public async Task<bool> RegisterUserAsync(UserRegisterRequestDto userRegisterRequestDto)
        {
            switch (userRegisterRequestDto.UserType)
            {
                case UserType.Watcher:
                    var watcher = _mapper.Map<UserRegisterRequestDto, Watcher>(userRegisterRequestDto);
                    return await RegisterNewUserAsync(watcher);
                case UserType.Subject:
                    var subject = _mapper.Map<UserRegisterRequestDto, Subject>(userRegisterRequestDto);
                    return await RegisterNewUserAsync(subject);
                default:
                    return false;
            }
        }

        private async Task<bool> RegisterNewUserAsync(Watcher watcher)
        {
            if (!(await AlreadyRegisteredUserAsync(watcher.Username)))
            {
                await _watcherRepository.InsertAsync(watcher);
                return true;
            }

            return false;
        }

        private async Task<bool> RegisterNewUserAsync(Subject subject)
        {
            if (!(await AlreadyRegisteredUserAsync(subject.Username)))
            {
                await _subjectRepository.InsertAsync(subject);
                return true;
            }

            return false;
        }

        private async Task<bool> AlreadyRegisteredUserAsync(string username)
        {
            var existsWatcher = await _watcherRepository.ExistsUserAsync(username);
            var existsSubject = await _subjectRepository.ExistsUserAsync(username);

            return existsWatcher || existsSubject;
        }
    }
}
