using AutoMapper;
using Core.Models;
using Core.Repositories;
using Infrastructure.DTOs;
using Infrastructure.Helpers;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface ILoginService
    {
        Task<LoginResultDto> LoginAsync(LoginRequestDto loginRequestDto);
    }

    public class LoginService : ILoginService
    {
        private readonly IWatcherRepository _watcherRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IProfilePictureUrlResolver _profilePictureUrlResolver;
        private readonly IMapper _mapper;

        public LoginService(
            IWatcherRepository watcherRepository,
            ISubjectRepository subjectRepository,
            IProfilePictureUrlResolver profilePictureUrlResolver,
            IMapper mapper)
        {
            _watcherRepository = watcherRepository;
            _subjectRepository = subjectRepository;
            _profilePictureUrlResolver = profilePictureUrlResolver;
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

            if (watcher is null)
                return null;

            watcher.ProfilePictureSrc = _profilePictureUrlResolver.Resolve(watcher);
            return _mapper.Map<Watcher, LoginResultDto>(watcher);
        }

        private async Task<LoginResultDto> TryLoginSubjectAsync(LoginRequestDto loginRequestDto)
        {
            var subject = await _subjectRepository.LoginAsync(loginRequestDto.Username, loginRequestDto.Password);

            if (subject is null)
                return null;

            subject.ProfilePictureSrc = _profilePictureUrlResolver.Resolve(subject);
            return _mapper.Map<Subject, LoginResultDto>(subject);
        }
    }
}
