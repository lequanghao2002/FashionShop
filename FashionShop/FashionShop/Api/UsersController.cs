using FashionShop.Models.Domain;
using FashionShop.Models.DTO.UserDTO;
using FashionShop.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IUserRepository _userRepository;

        public UsersController(UserManager<User> userManager, IUserRepository userRepository) 
        {
            _userManager = userManager;
            _userRepository = userRepository;
        }

        [HttpGet("get-list-users")]
        public async Task<IActionResult> GetListUsers(string? searchByName, string? filterRole)
        {
            try
            {
                var listUsers = await _userRepository.GetAllUser(searchByName, filterRole);

                return Ok(listUsers);
            }
            catch
            {
                return BadRequest("Lấy danh sách người dùng không thành công");
            }
        }

        [HttpPost("register-admin")]
        public async Task<IActionResult> RegisterAdmin (RegisterRequestDTO registerRequestDTO)
        {
            try
            {
                var checkEmail = await _userManager.FindByEmailAsync(registerRequestDTO.Email);
                if(checkEmail != null)
                {
                    return BadRequest("Email đã tồn tại, vui lòng nhập email khác");
                }

                if (registerRequestDTO.Password != registerRequestDTO.RePassword)
                {
                    return BadRequest("Mật khẩu và nhập lại mật khẩu không khớp");
                }

                var accountAdmin = await _userRepository.RegisterAccountAdmin(registerRequestDTO);
                if (accountAdmin == true)
                {
                    return Ok("Tạo tài khoản admin thành công");
                }
                else 
                {
                    return BadRequest("Tạo tài khoản admin không thành công");
                }
            }
            catch
            {
                return BadRequest("Tạo tài khoản admin không thành công");
            }
        }

        [HttpPost("register-member")]
        public async Task<IActionResult> RegisterMember(RegisterRequestDTO registerRequestDTO)
        {
            try
            {
                var checkEmail = await _userManager.FindByEmailAsync(registerRequestDTO.Email);
                if (checkEmail != null)
                {
                    return BadRequest("Email đã tồn tại, vui lòng nhập email khác");
                }

                if (registerRequestDTO.Password != registerRequestDTO.RePassword)
                {
                    return BadRequest("Mật khẩu và nhập lại mật khẩu không khớp");
                }

                var accountMember = await _userRepository.RegisterAccountMember(registerRequestDTO);
                if (accountMember == true)
                {
                    return Ok("Tạo tài khoản nhân viên thành công");
                }
                else
                {
                    return BadRequest("Tạo tài khoản nhân viên không thành công");
                }
            }
            catch
            {
                return BadRequest("Tạo tài khoản nhân viên không thành công");
            }
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginRequestDTO loginRequestDTO)
        {
            try
            {
                var loginAccount = await _userRepository.Login(loginRequestDTO);

                if(loginAccount != null)
                {
                    return Ok(loginAccount);
                }
                else
                {
                    return BadRequest("Đăng nhập không thành công");
                }
            }
            catch
            {
                return BadRequest("Đăng nhập không thành công");
            }
        }

        [HttpPut("account-lock/{idAccount}")]
        public async Task<IActionResult> AccountLock(string idAccount)
        {
            try
            {
                var result = await _userRepository.AccountLock(idAccount);
                if (result == true)
                {
                    return Ok("Khóa tài khoản thành công");
                }
                else
                {
                    return BadRequest("Tài khoản này đã bị khóa");
                }
            }
            catch
            {
                return BadRequest("Thực hiện khóa không thành công");
            }
        }

        [HttpPut("account-unlock/{idAccount}")]
        public async Task<IActionResult> AccountUnLock(string idAccount)
        {
            try
            {
                var result = await _userRepository.AccountUnlock(idAccount);
                if (result == true)
                {
                    return Ok("Mở khóa tài khoản thành công");
                }
                else
                {
                    return BadRequest("Tài khoản này không bị khóa");
                }
            }
            catch
            {
                return BadRequest("Thực hiện mở khóa không thành công");
            }
        }
    }
}
