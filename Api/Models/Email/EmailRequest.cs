namespace Api.Models.Email
{
    public class EmailRequest
    {
        public string To { get; set; } = string.Empty;
        public string? Cc { get; set; }
        public string Subject { get; set; } = string.Empty;
        public string Body { get; set; } = string.Empty;
        public IList<AttachmentRequest>? Attachments { get; set; }
    }
}
