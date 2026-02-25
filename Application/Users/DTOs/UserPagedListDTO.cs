using Enterprise_E_Commerce_Management_System.ViewModels.User;

namespace Enterprise_E_Commerce_Management_System.Application.Users.DTOs
{
    public class UserPagedListDTO
    {
        public int Count { get; set; }
        public List<UserItemDTO> Users { get; set; }
    }
}
