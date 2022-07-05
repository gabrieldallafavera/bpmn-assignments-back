namespace Api.Models.Email
{
    public class EmailRequest
    {
        [Required, EmailAddress]
        public string To { get; set; } = string.Empty;
        [EmailAddress]
        public string? Cc { get; set; }
        [Required]
        public string Subject { get; set; } = string.Empty;
        [Required]
        public string Body { get; set; } = string.Empty;
        public IList<AttachmentRequest>? Attachments { get; set; }
    }
}
