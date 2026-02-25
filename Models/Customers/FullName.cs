using Microsoft.EntityFrameworkCore;

namespace Enterprise_E_Commerce_Management_System.Models.Customers
{
    [Owned]
    public class FullName
    {
        public string FirstName { get; set; }
        public string? MiddleName { get; set; }
        public string LastName { get; set; }

        public override string ToString()
        {
            return FirstName + " " + (string.IsNullOrEmpty(MiddleName) ?
                "" : MiddleName + " ") + LastName;
        }
    }
}
