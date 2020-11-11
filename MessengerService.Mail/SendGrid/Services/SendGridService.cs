using MessengerService.Mail.SendGrid.Models;
using SendGrid;
using SendGrid.Helpers.Mail;
using System;
using System.Threading.Tasks;

namespace MessengerService.Mail.SendGrid.Services
{
    public class SendGridService : ISendGridService
    {
        public async Task<(bool IsSuccess, string ErrorMessage)> SendMail(SendGridData Data)
        {
            try
            {
                var sendGridClient = new SendGridClient(Data.SendGridAPIKey);
                var to = new EmailAddress(Data.To, Data.ToName);
                var title = Data.Title;
                var sender = new EmailAddress("roy360erick@gmail.com", "Erick Fernandez");
                var content = Data.Content;
                var objMessage = MailHelper.CreateSingleEmail(sender, to, title, content, content);

                await sendGridClient.SendEmailAsync(objMessage);

                return (true, null);
            }
            catch (Exception e)
            {
                return (false, e.Message);
            }
            
        }
    }
}
