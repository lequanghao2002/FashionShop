using FashionShop.Data;
using FashionShop.Models.Domain;
using FashionShop.Models.DTO.UserDTO;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace FashionShop.Repositories
{
    public interface IUserRepository
    {
        public Task<List<GetUserDTO>> GetAllUser(string? searchByName, string? filterRole);
        public Task<bool> RegisterAccountAdmin(RegisterRequestDTO registerRequestDTO);

        public Task<bool> RegisterAccountMember(RegisterRequestDTO registerRequestDTO);

        public Task<bool> RegisterAccountCustomer(RegisterRequestDTO registerRequestDTO);

        public Task<string> Login(LoginRequestDTO loginRequestDTO);

        public Task<bool> AccountLock(string idAccount);
        public Task<bool> AccountUnlock(string idAccount);

    }
    public class UserRepository : IUserRepository
    {
        private readonly FashionShopDBContext _fashionShopDBContext;
        private readonly UserManager<User> _userManager;
        private readonly ITokenRepository _tokenRepository;

        public UserRepository(FashionShopDBContext fashionShopDBContext, UserManager<User> userManager, ITokenRepository tokenRepository) {
            _fashionShopDBContext = fashionShopDBContext;
            _userManager = userManager;
            _tokenRepository = tokenRepository;
        }

        public async Task<List<GetUserDTO>> GetAllUser(string searchByName, string filterRole)
        {
            var listUsersDomain = _fashionShopDBContext.Users.AsQueryable();

            if(!searchByName.IsNullOrEmpty())
            {
                listUsersDomain = listUsersDomain.Where(u => u.FullName.Contains(searchByName));
            }

            if (!filterRole.IsNullOrEmpty())
            {
                listUsersDomain = listUsersDomain.Where(u => _fashionShopDBContext.UserRoles
                        .Where(ur => ur.RoleId == filterRole)
                        .Select(ur => ur.UserId)
                        .Contains(u.Id)
                );
            }

            var listUsersDTO = await listUsersDomain.Select(user => new GetUserDTO
            {
                ID = user.Id,
                FullName = user.FullName,
                Email = user.Email,
                Role = _fashionShopDBContext.UserRoles
                .Where(ur => ur.UserId == user.Id)
                .Join(_fashionShopDBContext.Roles, ur => ur.RoleId, r => r.Id, (ur, r) => r.Name)
                .First(),
                LockoutEnabled = user.LockoutEnabled,
            }).OrderByDescending(u => u.ID).ToListAsync();

            return listUsersDTO;
        }

        public async Task<bool> RegisterAccountAdmin(RegisterRequestDTO registerRequestDTO)
        {
            var admin = new User
            {
                FullName = registerRequestDTO.FullName,
                UserName = registerRequestDTO.Email,
                Email = registerRequestDTO.Email,
                LockoutEnabled = false,
            };

            var result = await _userManager.CreateAsync(admin, registerRequestDTO.Password);

            if(result.Succeeded) {
                result = await _userManager.AddToRoleAsync(admin, "Quản trị viên");
            
                if(result.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> RegisterAccountMember(RegisterRequestDTO registerRequestDTO)
        {
            var admin = new User
            {
                FullName = registerRequestDTO.FullName,
                UserName = registerRequestDTO.Email,
                Email = registerRequestDTO.Email,
                LockoutEnabled = false,
            };

            var result = await _userManager.CreateAsync(admin, registerRequestDTO.Password);

            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(admin, "Nhân viên");

                if (result.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<bool> RegisterAccountCustomer(RegisterRequestDTO registerRequestDTO)
        {
            var admin = new User
            {
                FullName = registerRequestDTO.FullName,
                UserName = registerRequestDTO.Email,
                Email = registerRequestDTO.Email,
                LockoutEnabled = false,
            };

            var result = await _userManager.CreateAsync(admin, registerRequestDTO.Password);

            if (result.Succeeded)
            {
                result = await _userManager.AddToRoleAsync(admin, "Khách hàng");

                if (result.Succeeded)
                {
                    return true;
                }
            }
            return false;
        }

        public async Task<string> Login(LoginRequestDTO loginRequestDTO)
        {
            var checkUser = await _userManager.FindByEmailAsync(loginRequestDTO.Email);

            if(checkUser != null && checkUser.LockoutEnabled == false)
            {
                var checkPassword = await _userManager.CheckPasswordAsync(checkUser, loginRequestDTO.Password);

                if (checkPassword)
                {
                    var roles = await _userManager.GetRolesAsync(checkUser);
                    if(roles != null) 
                    {
                        var jwtToken = _tokenRepository.CreateJWTToken(checkUser, roles.ToList());

                        return jwtToken;
                    }
                }
            }

            return null;
        }

        public async Task<bool> AccountLock(string idAccount)
        {
            var checkUser = await _userManager.FindByIdAsync(idAccount);

            if (checkUser != null && checkUser.LockoutEnabled == false)
            {
                checkUser.LockoutEnabled = true;
                _fashionShopDBContext.SaveChanges();

                return true;
            }

            return false;
        }

        public async Task<bool> AccountUnlock(string idAccount)
        {
            var checkUser = await _userManager.FindByIdAsync(idAccount);

            if (checkUser != null && checkUser.LockoutEnabled == true)
            {
                checkUser.LockoutEnabled = false;
                _fashionShopDBContext.SaveChanges();

                return true;
            }

            return false;
        }
    }
}
