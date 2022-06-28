namespace Api.Models.People
{
    public class UserRoleRequest : BaseModel
    {
        public string Role { get; set; } = string.Empty;
    }
}
