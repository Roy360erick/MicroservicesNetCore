using MessengerService.Mail.SendGrid.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace MessengerService.Mail.SendGrid.Services
{
    public interface ISendGridService
    {
        Task<(bool IsSuccess, string ErrorMessage)> SendMail(SendGridData Data);
    }
}
