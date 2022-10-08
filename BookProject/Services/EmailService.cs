using BookProject.Models;
using Microsoft.Extensions.Options;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace BookProject.Services
{
    public class EmailService : IEmailService
    {
        private const string templatePath = @"EmailTemplates/{0}.html";
        private readonly SMTPConfigModel _smtpConfig;
        public EmailService(IOptions<SMTPConfigModel> smtpConfig)
        {
            _smtpConfig = smtpConfig.Value;

        }

        public async Task SendTestEmail(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceholders("Hello {{UserName}}, This is a Test Email from Ani's Web library", userEmailOptions.Placeholders);
            userEmailOptions.Body = UpdatePlaceholders(GetEmailBody("TestEmail"), userEmailOptions.Placeholders);
            await SendEmail(userEmailOptions);
        }

        public async Task SendEmailForEmailConfirmation(UserEmailOptions userEmailOptions)
        {
            userEmailOptions.Subject = UpdatePlaceholders("Hello {{UserName}}, Confirm your E-Mail Address..!! ", userEmailOptions.Placeholders);
            userEmailOptions.Body = UpdatePlaceholders(
                GetEmailBody("EmailConfirmation"), userEmailOptions.Placeholders);
            await SendEmail(userEmailOptions);
        }
        private async Task SendEmail(UserEmailOptions userEmailOptions)
        {
            MailMessage mail = new MailMessage
            {
                
                Subject = userEmailOptions.Subject,
                Body = userEmailOptions.Body,
                From = new MailAddress(_smtpConfig.SenderAddress, _smtpConfig.SenderDisplayName),
                IsBodyHtml = _smtpConfig.IsBodyHTML
            };
            foreach (var toEmail in userEmailOptions.EmailAddresses)
            {
                mail.To.Add(toEmail);
            }
            NetworkCredential networkCredential = new NetworkCredential(_smtpConfig.UserName, _smtpConfig.Password);
            SmtpClient smtpclient = new SmtpClient
            {
                Host = _smtpConfig.Host,
                Port = _smtpConfig.Port,
                UseDefaultCredentials = _smtpConfig.UseDefaultCredentials,
                Credentials = networkCredential,
                EnableSsl = _smtpConfig.EnableSSL,


            };
            mail.BodyEncoding = Encoding.Default;
            await smtpclient.SendMailAsync(mail);
        }

        private string GetEmailBody(string templateName)
        {
            var body = File.ReadAllText(string.Format(templatePath, templateName));
            return body;
        }
        private string UpdatePlaceholders(string text , List<KeyValuePair<string,string>> keyValuePairs)
        {
            if(!string.IsNullOrEmpty(text) && keyValuePairs!=null)
            {
                foreach(var placeholders in keyValuePairs)
                {
                    //var check = 0;
                    if(text.Contains(placeholders.Key))
                    {
                        //check = 1;
                        text=text.Replace(placeholders.Key,placeholders.Value);
                    }
                }
            }
            return text;
        }
    }
    
}

