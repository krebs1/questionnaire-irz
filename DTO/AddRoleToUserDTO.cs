namespace questionnaire.DTO;

public class AddRoleToUserDTO
{
    public Guid UserId { get; set; }
    public Guid RoleId { get; set; }

    public AddRoleToUserDTO(Guid userId, Guid roleId)
    {
        UserId = userId;
        RoleId = roleId;
    }
}