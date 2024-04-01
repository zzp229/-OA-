using System;

namespace MyToDo.Api.Context.Mail.MailDto
{
    public class MyMessage
    {
        public int EmailID { get; set; }
        public bool IsRead { get; set; }
        public string UserName { get; set; }
        public string EmailTitle { get; set; }
        public DateTime SentDate { get; set; }
    }
}
