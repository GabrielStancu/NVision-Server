using Infrastructure.DTOs;
using Infrastructure.Services;
using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace API.Hubs
{
    public class AlertHub : Hub
    {
        private readonly IAlertingService _alertingService;
        public AlertHub(IAlertingService alertingService)
        {
            _alertingService = alertingService;
        }

        public async Task<MessageResource.StatusEnum> SendAlertAsync(DeviceAlertDto alertDto)
        {
            var accountSid = ConfigurationManager.GetAccountSid();
            var authToken = ConfigurationManager.GetAuthToken();
            TwilioClient.Init(accountSid, authToken);

            var alert = await _alertingService.CreateAlertAsync(alertDto);
            var to = alert.WatcherPhoneNumber;
            var from = new PhoneNumber(ConfigurationManager.GetSenderPhoneNumber());
            var message = MessageResource.Create(
                    to: to,
                    from: from,
                    body: alert.Message
                );

            return message.Status;
        }
    }
}
