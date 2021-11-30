using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Twilio;
using Twilio.AspNet.Core;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SmsController : TwilioController
    {
        [HttpGet]
        public ActionResult<string> SendSms()
        {
            var accountSid = ConfigurationManager.GetAccountSid();
            var authToken = ConfigurationManager.GetAuthToken();
            TwilioClient.Init(accountSid, authToken);

            var to = new PhoneNumber(ConfigurationManager.GetReceiverPhoneNumber());
            var from = new PhoneNumber(ConfigurationManager.GetSenderPhoneNumber());

            var message = MessageResource.Create(
                to: to,
                from: from,
                body: "Yet it's me again. You're wondering how I ended up here..."
                );

            return message.Sid;
        }
    }
}
