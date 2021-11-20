using Microsoft.Extensions.Options;
using Project.BLL.Abstract;
using Project.ENTITIES.Concrete;
using Project.ENTITIES.DTOs;
using Project.SHARED.Utilities.Results.Abstract;
using Project.SHARED.Utilities.Results.ComplexTypes;
using Project.SHARED.Utilities.Results.Concrete;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Project.BLL.Concrete
{
    public class MailManager : IMailService
    {
        private readonly SmtpSettings _smtpSettings;
        public MailManager(IOptions<SmtpSettings> smtpSettings)
        {
            _smtpSettings = smtpSettings.Value;
        }
        public IResult Send(EmailSendDto emailSendDto)
        {
            MailMessage message = new MailMessage
            {
                From = new MailAddress(_smtpSettings.SenderEmail),
                To = { new MailAddress(emailSendDto.Email) },
                Subject = emailSendDto.Subject,
                IsBodyHtml = true, //epostanın değiştirilebilmesi için true hale getirdik. Daha güzel gözükmesini sağlar..
                Body = emailSendDto.Message
            };
            SmtpClient smtpClient = new SmtpClient
            {
                Host = _smtpSettings.Server,
                Port = _smtpSettings.Port,
                EnableSsl = true, //güvenlik
                UseDefaultCredentials = false, //kendi hesap bilgilerimizi vereceğiz
                Credentials = new NetworkCredential(_smtpSettings.Username, _smtpSettings.Password),
                DeliveryMethod = SmtpDeliveryMethod.Network

            };
            smtpClient.Send(message);
            return new Result(ResultStatus.Success,
                              $"E-Postanız başarıyla gönderilmiştir.");
        }

        public IResult SendContactEmail(EmailSendDto emailSendDto)
        {
            MailMessage message = new MailMessage();
            message.From = new MailAddress(_smtpSettings.SenderEmail);
            message.To.Add("naciye.ceren097@gmail.com");
            message.Subject = emailSendDto.Subject;
            message.IsBodyHtml = true; //epostanın değiştirilebilmesi için true hale getirdik. Daha güzel gözükmesini sağlar..
            message.Body = $"Gönderen Kişi: {emailSendDto.Name}, Gönderen E-Posta Adresi: {emailSendDto.Email} \n {emailSendDto.Message}";

            SmtpClient smtpClient = new SmtpClient();

            smtpClient.Host = _smtpSettings.Server;
            smtpClient.Port = _smtpSettings.Port;
            smtpClient.EnableSsl = true; //güvenlik
            smtpClient.UseDefaultCredentials = false; //kendi hesap bilgilerimizi vereceğiz
            smtpClient.Credentials = new NetworkCredential(_smtpSettings.SenderEmail, _smtpSettings.Password);
            smtpClient.DeliveryMethod = SmtpDeliveryMethod.Network;

            smtpClient.Send(message);
            return new Result(ResultStatus.Success,
                              $"E-Postanız başarıyla gönderilmiştir.");
        }
    }
}
