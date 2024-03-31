namespace MyToDo.Api.Context.Mail.MailDto
{
    /// <summary>
    ///Mail 
    /// </summary>
    public class EmailDto
    {
        public long FromUserID { get; set; }
        public string EmailBody { get; set; }
        public long ToUserID { get; set; }
        public string FileName { get; set; }

        public string EmailTitle { get; set; }
    }
}
