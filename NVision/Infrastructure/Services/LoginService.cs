using AutoMapper;
using Core.Models;
using Core.Repositories;
using Infrastructure.DTOs;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public class LoginService : ILoginService
    {
        private readonly IWatcherRepository _watcherRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;

        public LoginService(
            IWatcherRepository watcherRepository,
            ISubjectRepository subjectRepository,
            IMapper mapper)
        {
            _watcherRepository = watcherRepository;
            _subjectRepository = subjectRepository;
            _mapper = mapper;
        }
        public async Task<LoginResultDto> LoginAsync(LoginRequestDto loginRequestDto)
        {
            var user = await TryLoginWatcherAsync(loginRequestDto);

            if (user != null)
                return user;

            user = await TryLoginSubjectAsync(loginRequestDto);

            return user;
        }

        private async Task<LoginResultDto> TryLoginWatcherAsync(LoginRequestDto loginRequestDto)
        {
            var watcher = await _watcherRepository.LoginAsync(loginRequestDto.Username, loginRequestDto.Password);

            return watcher is null ? null : _mapper.Map<Watcher, LoginResultDto>(watcher);
        }

        private async Task<LoginResultDto> TryLoginSubjectAsync(LoginRequestDto loginRequestDto)
        {
            var subject = await _subjectRepository.LoginAsync(loginRequestDto.Username, loginRequestDto.Password);

            return subject is null ? null : _mapper.Map<Subject, LoginResultDto>(subject);
        }
    }
}
