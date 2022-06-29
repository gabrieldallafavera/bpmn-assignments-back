using System.ComponentModel.DataAnnotations;

namespace Api.Models.Email
{
    public class AttachmentRequest
    {
        public string FileName { get; set; } = string.Empty;
        [RegularExpression(@"^(?:[A-Za-z0-9+/]{4})*(?:[A-Za-z0-9+/]{2}==|[A-Za-z0-9+/]{3}=)?$", ErrorMessage = "Insira somento o base64 do arquivo, não pode conter \"data:*/*;base64,\"")]
        public string FileBase64 { get; set; } = string.Empty;
    }
}
