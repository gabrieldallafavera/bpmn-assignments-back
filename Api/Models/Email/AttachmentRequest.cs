namespace Api.Models.Email
{
    public class AttachmentRequest
    {
        public string FileName { get; set; } = string.Empty;
        public string FileBase64 { get; set; } = string.Empty;
    }
}
