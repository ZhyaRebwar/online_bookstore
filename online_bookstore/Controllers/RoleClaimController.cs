using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using online_bookstore.Dtos;
using online_bookstore.Services;

namespace online_bookstore.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize(Roles = "Admin")]
    [Authorize(Roles = "User,Admin")]
    public class RoleClaimController : ControllerBase
    {

        private readonly RoleClaimService _roleClaimService;

        public RoleClaimController(RoleClaimService roleClaimService)
        {
            _roleClaimService = roleClaimService;
        }

        [HttpPost("CreateRole")]
        public async Task<IActionResult> CreateRole([FromBody] string roleName)
        {
            var result = await _roleClaimService.CreateRoleAsync(roleName);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok($"Role '{roleName}' created successfully.");
        }

        [HttpPost("AssignRoleToUser")]
        public async Task<IActionResult> AssignRoleToUser([FromBody] UserRoleDto dto)
        {
            var result = await _roleClaimService.AssignRoleToUserAsync(dto.UserId, dto.RoleName);
            if (!result.Succeeded)
                return BadRequest(result.Errors);

            return Ok($"User '{dto.UserId}' assigned to role '{dto.RoleName}'.");
        }
    }
}
