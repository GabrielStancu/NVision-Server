namespace Infrastructure.DTOs
{
    public class WatcherHomepageDataRequestDto
    {
        public int WatcherId { get; set; }
        public SubjectSpecificationDto SubjectSpecificationDto { get; set; }
        public AlertSpecificationDto AlertSpecificationDto { get; set; }
    }
}
