namespace Api.Models.Email
{
    public class AttachmentDto
    {
        public string FileName { get; set; } = string.Empty;
        public string FileBase64 { get; set; } = string.Empty;
    }
}
