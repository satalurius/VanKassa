namespace VanKassa.Domain.ViewModels;

public class EmployeeOutletViewModel
{
    public int Id { get; set; }
    public string Address { get; set; } = string.Empty;

    protected bool Equals(EmployeeOutletViewModel other)
        => Id == other.Id
           && Address == other.Address;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != this.GetType()) return false;

        return Equals((EmployeeOutletViewModel)obj);
    }

    public override int GetHashCode()
        => HashCode.Combine(Id, Address);
}