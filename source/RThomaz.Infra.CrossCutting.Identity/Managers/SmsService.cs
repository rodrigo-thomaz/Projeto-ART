using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Twilio;
using Twilio.Types;
using Twilio.Rest.Api.V2010.Account;

namespace RThomaz.Infra.CrossCutting.Identity.Managers
{
    public class SmsService : IIdentityMessageService
    {
        public async Task SendAsync(IdentityMessage message)
        {
            // Utilizando TWILIO como SMS Provider.
            // https://www.twilio.com/docs/quickstart/csharp/sms/sending-via-rest

            const string accountSid = "SKa70d4649794e3c61258d8583e2f2d7d0";
            const string authToken = "1tpddqw737fE32PcjwobBtYAIwM6MtdR";

            TwilioClient.Init(accountSid, authToken);

            var messageResource = await MessageResource.CreateAsync(
                to: new PhoneNumber(message.Destination),
                from: new PhoneNumber("+5511985393413"),
                body: message.Body);
        }
    }
}