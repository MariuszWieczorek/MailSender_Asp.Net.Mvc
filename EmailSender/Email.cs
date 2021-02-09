using EmailSender.Extensions;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace EmailSender
{
    public class Email
    {
        private SmtpClient _smtp;
        private MailMessage _mail;

        private string _hostSmtp;
        private bool _enableSsl;
        private int _port;
        private string _senderEmail;
        private string _senderEmailPassword;
        private string _senderName;

        public Email(EmailParams emailParams)
        {
            _hostSmtp = emailParams.HostSmtp;
            _enableSsl = emailParams.EnableSsl;
            _port = emailParams.Port;
            _senderEmail = emailParams.SenderEmail;
            _senderEmailPassword = emailParams.SenderEmailPassword;
            _senderName = emailParams.SenderName;
        }

        public async Task Send(string subject, string body, List<string> emailRecipients)
        {
            _mail = new MailMessage();
            _mail.From = new MailAddress(_senderEmail, _senderName);
            _mail.Subject = subject;

            foreach(var address in emailRecipients)
                _mail.To.Add(new MailAddress(address));

            _mail.IsBodyHtml = true;
            _mail.BodyEncoding = Encoding.UTF8;
            _mail.SubjectEncoding = Encoding.UTF8;

            // też by zadziałało, ale chcemy to zrobić lepiej
            // _mail.Body = body;
            // aby mail trafił do skrzynki odbiorczej, a nie do spamu jest mnóstwo zasad
            // jakich należy się trzymać
            // przede wszystkim jest zasada aby wysyłać maila w dwóch wersjach
            // wysyłamy widok bez tagów html
            // oprócz tego wysyłamy widok z tagami html z tagami <html>, <head>, <body>

            _mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString(body.StripHTML()
                , null, MediaTypeNames.Text.Plain));

            _mail.AlternateViews.Add(AlternateView.CreateAlternateViewFromString($@"
            <html>    
                <head>
                </head>
                <body>
            <div style='font-size: 16px; padding: 10px; font-family: Arial; line-height: 1.4'>
                {body}
            </div>
            </body>
            </html>
            "
                , null, MediaTypeNames.Text.Html));

            _smtp = new SmtpClient()
            {
                Host = _hostSmtp,
                EnableSsl = _enableSsl,
                Port = _port,
                DeliveryMethod = SmtpDeliveryMethod.Network,
                UseDefaultCredentials=false,
                Credentials = new NetworkCredential(_senderEmail,_senderEmailPassword) 
            };

            _smtp.SendCompleted += OnSendCompleted;
            
            // jeżeli użyć metody asynchronicznej
            // musimy użyć słowa kluczowego await
            // w sygnaturze metody async i zamiast void Task
            await _smtp.SendMailAsync(_mail);
        }

        private void OnSendCompleted(object sender, AsyncCompletedEventArgs e)
        {
            _smtp.Dispose();
            _mail.Dispose();
        }
    }
}
