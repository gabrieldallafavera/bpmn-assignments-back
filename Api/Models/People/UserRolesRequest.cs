namespace Api.Models.People
{
    public class UserRolesRequest : BaseModel
    {
        public string Role { get; set; } = string.Empty;
    }
}
