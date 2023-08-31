using System.ComponentModel.DataAnnotations;
using FashionShop.Models.DTO.UserDTO;
using FashionShop.Models.Domain;

namespace FashionShop.Models.DTO.UserDTO
{
    public class RegisterRequestDTO
    {
        [Required(ErrorMessage = "Vui lòng nhập họ và tên")]
        public string FullName { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập email")]
        [EmailAddress(ErrorMessage = "Địa chỉ email không đúng")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Vui lòng nhập so dien thoai")]
        public string PhoneNumber { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập mật khẩu")]
        [MinLength(3, ErrorMessage = "Mật khẩu phải có ít nhất 3 ký tự")]
        public string Password { get; set; }


        [Required(ErrorMessage = "Vui lòng nhập lại mật khẩu")]
        public string RePassword { get; set; }
    }
}
