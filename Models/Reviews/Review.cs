using Enterprise_E_Commerce_Management_System.Models.Customers;
using Enterprise_E_Commerce_Management_System.Models.Products;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Enterprise_E_Commerce_Management_System.Models.Reviews
{
    public class Review
    {
        public int Id {  get; set; }

        [Required]
        public int ProductId {  get; set; }

        [Required]
        public virtual Product Product { get; set; }  
        
        [Required]
        public int CustomerId {  get; set; }

        [Required]
        public virtual Customer Customer { get; set; }

        public int Rating {  get; set; }
        public string? Comment {  get; set; }
        public DateTime ReviewDate {  get; set; }

    }
}
