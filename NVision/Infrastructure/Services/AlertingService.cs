using Core.Models;
using Core.Repositories;
using Infrastructure.DTOs;
using Infrastructure.Helpers;
using System.Threading.Tasks;

namespace Infrastructure.Services
{
    public interface IAlertingService
    {
        Task<HealthAlert> CreateAlertAsync(AlertDto alertDto);
    }

    public class AlertingService : IAlertingService
    {
        private readonly IWatcherRepository _watcherRepository;
        private readonly ISubjectRepository _subjectRepository;
        private readonly IAlertRepository _alertRepository;

        public AlertingService(
            IWatcherRepository watcherRepository,
            ISubjectRepository subjectRepository,
            IAlertRepository alertRepository)
        {
            _watcherRepository = watcherRepository;
            _subjectRepository = subjectRepository;
            _alertRepository = alertRepository;
        }

        public async Task<HealthAlert> CreateAlertAsync(AlertDto alertDto)
        {
            var subject = await _subjectRepository.SelectByIdAsync(alertDto.SubjectId);
            var message = $"An alert was detected for {subject.FirstName} {subject.LastName} in {alertDto.AlertMoment.ToShortDateString()} at {alertDto.AlertMoment.ToLongTimeString()}. The remote monitoring hub detected the following issue: <{alertDto.Message}>.";
            var alert = new Alert
            {
                SubjectId = subject.Id,
                Message = message,
                WasTrueAlert = null
            };           
            var healthAlert = new HealthAlert
            {
                WatcherPhoneNumber = await _watcherRepository.GetWatcherPhoneNumberByIdAsync(subject.WatcherId),
                Message = message
            };

            await _alertRepository.InsertAsync(alert);

            return healthAlert;
        }
    }
}
