namespace AuthUserApi.Models.DTOs
{
    public class DeleteUserRoleDto
    {
        public Guid UserId { get; set; }
        public Guid RoleId { get; set; }
    }
}
