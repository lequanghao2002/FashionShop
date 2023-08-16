using FashionShop.Models.DTO.RolesDTO;
using FashionShop.Repositories;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FashionShop.Api
{
    [Route("api/[controller]")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly IRoleRepository _roleRepository;
        public RolesController(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        [HttpGet("get-list-roles")]
        public async Task<IActionResult> GetListRoles()
        {
            try
            {
                var listRoles = await _roleRepository.GetAll();
                return Ok(listRoles);
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpGet("get-role-by-id/{id}")]
        public async Task<IActionResult> GetRoleById(string id)
        {
            try
            {
                var roleById = await _roleRepository.GetById(id);
                if (roleById != null)
                {
                    return Ok(roleById);
                }
                else
                {
                    return BadRequest($"Không tìm thấy role có id = {id}");
                }
            }
            catch
            {
                return BadRequest();
            }
        }

        [HttpPost("create-role")]
        public async Task<IActionResult> CreateRole(CreateRoleDTO createRoleDTO)
        {
            try
            {
                var createRole = await _roleRepository.Create(createRoleDTO);
                return Ok(createRole);
            }
            catch
            {
                return BadRequest("Tạo role không thành công");
            }
        }

        [HttpPut("update-role/{id}")]
        public async Task<IActionResult> Updaterole(CreateRoleDTO createRoleDTO, string id)
        {
            try
            {
                var updateRole = await _roleRepository.Update(createRoleDTO, id);
                if (updateRole != null)
                {
                    return Ok(updateRole);
                }
                else
                {
                    return BadRequest($"Không tìm thấy role có id = {id}");
                }
            }
            catch
            {
                return BadRequest("Chỉnh sửa role không thành công");
            }
        }

        [HttpDelete("delete-role/{id}")]
        public async Task<IActionResult> DeleteRole(string id)
        {
            try
            {
                var deleteRole = await _roleRepository.Delete(id);
                if (deleteRole != null)
                {
                    return Ok(deleteRole);
                }
                else
                {
                    return BadRequest($"Không tìm thấy role có id = {id}");
                }
            }
            catch
            {
                return BadRequest("Xóa role không thành công");
            }
        }
    }
}
