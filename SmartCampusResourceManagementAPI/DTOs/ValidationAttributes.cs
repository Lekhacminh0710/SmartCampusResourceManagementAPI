using System.ComponentModel.DataAnnotations;

namespace SmartCampusResourceManagementAPI.DTOs
{
    public class NotInFutureAttribute : ValidationAttribute
    {
        public NotInFutureAttribute()
        {
            ErrorMessage = "PublishedDate khong duoc lon hon ngay hien tai.";
        }

        public override bool IsValid(object? value)
        {
            if (value is null)
            {
                return true;
            }

            if (value is DateTime date)
            {
                return date <= DateTime.Now;
            }

            return false;
        }
    }
}
