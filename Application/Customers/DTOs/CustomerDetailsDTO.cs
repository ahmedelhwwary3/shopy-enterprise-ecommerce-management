namespace Enterprise_E_Commerce_Management_System.Application.Customers.DTOs
{
    public class CustomerDetailsDTO
    {
        public int Id { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public DateOnly CreatedDate { get; set; }

        public FullNameDTO FullName { get; set; }
        public AddressDTO Address { get; set; }
    }
}
