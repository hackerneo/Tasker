namespace Tasker.JobType.EmailSender
{
    using System.Net;
    using System.Net.Mail;
    using Core;

    class EmailSender: IJobType
    {
        public string Id { get; }
        public string Name { get; }
        public string Description { get; }

        public void Execute(JobParameters parameters)
        {
            using (var smtpClient = new SmtpClient
            {
                Host = "smtp.gmail.com",
                Port = 587,
                EnableSsl = true,
                Timeout = 10000,
                UseDefaultCredentials = false,
                Credentials = new NetworkCredential {UserName = "", Password = ""}
            })
            {
                using (var mailMessage = new MailMessage(parameters["sender"], parameters["recipient"]))
                {
                    mailMessage.Subject = parameters["Subject"];
                    mailMessage.Body = parameters["Body"];
                    smtpClient.Send(mailMessage);
                }
            }
        }
    }
}
