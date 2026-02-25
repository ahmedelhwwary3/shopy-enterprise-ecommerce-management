using Enterprise_E_Commerce_Management_System.Application.Customers.DTOs;
using Enterprise_E_Commerce_Management_System.Application.Customers.Mapping;
using Enterprise_E_Commerce_Management_System.Models.Carts;
using Enterprise_E_Commerce_Management_System.Models.Orders;
using Enterprise_E_Commerce_Management_System.Models.Payments;
using Enterprise_E_Commerce_Management_System.Models.Reviews;
using Enterprise_E_Commerce_Management_System.Models.Customers;
using Enterprise_E_Commerce_Management_System.ViewModels.Customer;
using System.ComponentModel.DataAnnotations;
using Enterprise_E_Commerce_Management_System.Models.Countries;

namespace Enterprise_E_Commerce_Management_System.Models.Customers
{
    public class Customer
    {
        public int Id { get; set; }

        [Required]
        public FullName FullName { get; set; }

        [Required]
        public Address Address { get; set; }

        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public bool IsActive { get; set; } = true;
        public DateOnly CreateDate { get; set; }

        //public int CountryId {  get; set; }   //Exists in Address
        //public virtual Country Country { get; set; } //Exists in Address

        public virtual ICollection<Cart> Carts {  get; set; }
            = new HashSet<Cart>();
        public virtual ICollection<Order>Orders { get; set; }
            = new HashSet<Order>();

        public virtual ICollection<Payment> Payments { get; set; }
            = new HashSet<Payment>();

        public virtual ICollection<Review>Reviews { get; set; }
            = new HashSet<Review>();


        public static bool operator ==(Customer dbCustomer,CustomerFormViewModel vmCutomer)
        {
            if (ReferenceEquals(dbCustomer, null) && ReferenceEquals(vmCutomer, null))
                return true;

            if (ReferenceEquals(dbCustomer, null) || ReferenceEquals(vmCutomer, null))
                return false;
            return AddressEqualsDTO(dbCustomer.Address, vmCutomer.Address)&&
                   dbCustomer.Email==vmCutomer.Email&&
                   dbCustomer.PhoneNumber==vmCutomer.PhoneNumber&&
                   FullNameEqualsDTO(dbCustomer.FullName,vmCutomer.FullName);
        }
        public static bool operator !=(Customer dbCustomer, CustomerFormViewModel dtoCutomer)
        {

            if (ReferenceEquals(dbCustomer, null) && ReferenceEquals(dtoCutomer, null))
                return false;

            if (ReferenceEquals(dbCustomer, null) || ReferenceEquals(dtoCutomer, null))
                return true;
            return AddressEqualsDTO(dbCustomer.Address, dtoCutomer.Address) &&
                   dbCustomer.Email == dtoCutomer.Email &&
                   dbCustomer.PhoneNumber == dtoCutomer.PhoneNumber &&
                   FullNameEqualsDTO(dbCustomer.FullName, dtoCutomer.FullName);
        }
 
        private static bool FullNameEqualsDTO(FullName domain,FullNameViewModel viewModel)
        {
            return domain.LastName==viewModel.LastName&&
                domain.FirstName==viewModel.FirstName&&
                domain.MiddleName==viewModel.MiddleName;
        }
        private static bool AddressEqualsDTO(Address domain, AddressViewModel viewModel)
        {
            return domain.PostalCode == viewModel.PostalCode &&
                   domain.Street == viewModel.Street &&
                   domain.City == viewModel.City &&
                   domain.CountryId == viewModel.CountryId;
        }
    }
}
