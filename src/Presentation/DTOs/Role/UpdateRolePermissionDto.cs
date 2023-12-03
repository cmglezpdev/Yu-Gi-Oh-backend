namespace backend.Presentation.DTOs.Role;

public class UpdateRolePermissionDto
{
    public Guid ClaimId { get; set; }
    public bool ToAdd { get; set; }
    public bool ToRemove => !ToAdd;
}