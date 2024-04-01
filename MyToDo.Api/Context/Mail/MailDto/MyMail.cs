using System;

namespace MyToDo.Api.Context.Mail.MailDto
{
    public class MyMail
    {
        public string UserName { get; set; }
        public string EmailBody { get; set; }
        public DateTime SentDate { get; set; }
        public string EmailTitle { get; set; }
        public string FileName { get; set; }

    }
}
