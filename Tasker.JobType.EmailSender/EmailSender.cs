namespace Tasker.JobType.EmailSender
{
    using System.Configuration;
    using System.Net;
    using System.Net.Mail;
    using Core;

    public class EmailSender : IJobType
    {
        public string Description
        {
            get { return "Отправляет сообщение на e-mail"; }
        }

        public void Execute(JobParameters parameters)
        {
            using (var smtpClient = new SmtpClient
            {
                Host = ConfigurationManager.AppSettings["SMTPHost"],
                Port = int.Parse(ConfigurationManager.AppSettings["SMTPPort"]),
                EnableSsl = bool.Parse(ConfigurationManager.AppSettings["SMTPUseSSL"]),
                Timeout = int.Parse(ConfigurationManager.AppSettings["SMTPTimeout"]),
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential
                {
                    UserName = ConfigurationManager.AppSettings["SMTPUser"],
                    Password = ConfigurationManager.AppSettings["SMTPPassword"]
                }
            })
            {
                using (var mailMessage = new MailMessage(parameters["Sender"], parameters["Recipient"]))
                {
                    mailMessage.Subject = parameters["Subject"];
                    mailMessage.Body = parameters["Body"];
                    smtpClient.Send(mailMessage);
                }
            }
        }
    }
}
