namespace VanKassa.Domain.ViewModels;

public class EmployeeRoleViewModel
{
    public int RoleId { get; set; }
    public string RoleName { get; set; } = string.Empty;


    protected bool Equals(EmployeeRoleViewModel other)
        => RoleId == other.RoleId
           && RoleName == other.RoleName;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;

        return Equals((EmployeeRoleViewModel)obj);
    }

    public override int GetHashCode()
       => HashCode.Combine(RoleId, RoleName);
}