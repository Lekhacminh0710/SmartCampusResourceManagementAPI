namespace SmartCampusResourceManagementAPI.Models
{
    public enum UserRole
    {
        Admin,
        Staff,
        Lecturer
    }

    public enum Department
    {
        IT,
        Academic,
        Business
    }

    public enum ResourceStatus
    {
        Inactive = 0,
        Active = 1
    }
}
