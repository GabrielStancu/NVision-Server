using Core.Models;
using Microsoft.Extensions.Configuration;

namespace Infrastructure.Helpers
{
    public interface IProfilePictureUrlResolver
    {
        string Resolve(User user);
    }

    public class ProfilePictureUrlResolver : IProfilePictureUrlResolver
    {
        private readonly IConfiguration _config;

        public ProfilePictureUrlResolver(IConfiguration config)
        {
            _config = config;
        }
        public string Resolve(User user)
        {
            string fileName;
            fileName = string.IsNullOrEmpty(user.ProfilePictureSrc) ? "NoUser.png" : user.ProfilePictureSrc;
            return $"{_config["ProfilePicturesUrl"]}/{fileName}";
        }
    }
}
