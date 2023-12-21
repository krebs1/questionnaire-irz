namespace questionnaire.DTO;

public class AddRoleToUserDTO
{
    public Guid UserId { get; set; }
    public string RoleName { get; set; }

    public AddRoleToUserDTO(Guid userId, string roleName)
    {
        UserId = userId;
        RoleName = roleName;
    }
}