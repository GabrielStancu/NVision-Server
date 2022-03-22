using AutoMapper;
using Core.Models;
using Core.Repositories;
using Infrastructure.DTOs;
using Infrastructure.Helpers;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IUserService
    {
        Task<UserDisplayDataDto> GetUserDisplayDataAsync(UserDisplayDataRequestDto request);
    }

    public class UserService : IUserService
    {
        private readonly IWatcherRepository _watcherRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IMapper _mapper;
        private readonly IProfilePictureUrlResolver _resolver;

        public UserService(
            IWatcherRepository watcherRepository, 
            ISubjectRepository subjectRepository, 
            IMapper mapper,
            IProfilePictureUrlResolver resolver)
        {
            _watcherRepository = watcherRepository;
            _subjectRepository = subjectRepository;
            _mapper = mapper;
            _resolver = resolver;
        }
        public async Task<UserDisplayDataDto> GetUserDisplayDataAsync(UserDisplayDataRequestDto request)
        {
            return request.UserType switch
            {
                UserType.Watcher => await GetWatcherDisplayDataAsync(request.UserId),
                UserType.Subject => await GetSubjectDisplayDataAsync(request.UserId),
                _ => null,
            };
        }
        private async Task<UserDisplayDataDto> GetWatcherDisplayDataAsync(int id)
        {
            var watcher = await _watcherRepository.SelectByIdAsync(id);
            var watcherData = _mapper.Map<Watcher, UserDisplayDataDto>(watcher);
            watcherData.ProfilePictureSrc = _resolver.Resolve(watcher);

            return watcherData;
        }
        private async Task<UserDisplayDataDto> GetSubjectDisplayDataAsync(int id)
        {
            var subject = await _subjectRepository.SelectByIdAsync(id);
            var subjectData = _mapper.Map<Subject, UserDisplayDataDto>(subject);
            subjectData.ProfilePictureSrc = _resolver.Resolve(subject);

            return subjectData;
        }
    }
}
