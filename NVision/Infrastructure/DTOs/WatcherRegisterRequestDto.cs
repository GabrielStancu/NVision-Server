namespace Infrastructure.DTOs
{
    public class WatcherRegisterRequestDto : UserRegisterRequestDto 
    {
        public string PhoneNumber { get; set; }
    }
}
