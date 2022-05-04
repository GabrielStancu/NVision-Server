using Infrastructure.DTOs;
using Infrastructure.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DeviceController : ControllerBase
    {
        private readonly IDeviceService _deviceService;
        private readonly IAlertingService _alertingService;

        public DeviceController(IDeviceService deviceService, IAlertingService alertingService)
        {
            _deviceService = deviceService;
            _alertingService = alertingService;
        }

        [HttpGet]
        public async Task<ActionResult<SubjectDeviceDataDto>> GetSubjectData(Guid serialNumber)
        {
            var subjectData = await _deviceService.GetSubjectDeviceDataAsync(serialNumber);
            return Ok(subjectData);
        }

        [HttpPost]
        public async Task<MessageResource.StatusEnum> SendAlert(DeviceAlertDto alertDto)
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
