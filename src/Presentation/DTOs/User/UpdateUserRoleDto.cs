namespace backend.Presentation.DTOs.User;

public sealed class UpdateUserRoleDto
{
    public Guid RoleId { get; set; }
    public bool ToAdd { get; set; }
    private bool ToRemove => !ToAdd;
}