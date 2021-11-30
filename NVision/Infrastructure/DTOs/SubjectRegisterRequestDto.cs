namespace Infrastructure.DTOs
{
    public class SubjectRegisterRequestDto : UserRegisterRequestDto
    {
        public string Address { get; set; }
        public int WatcherId { get; set; }
        public bool IsPatient { get; set; }
    }
}
