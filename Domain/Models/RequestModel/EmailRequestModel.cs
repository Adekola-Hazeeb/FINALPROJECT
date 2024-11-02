namespace FINALPROJECT.Domain.Models.RequestModel
{
    public class EmailRequestModel
    {
        public string ToEmail { get; set; }
        public string ToName { get; set; }
        public string Subject { get; set; }
        public string HtmlContent { get; set; }
    }
}
