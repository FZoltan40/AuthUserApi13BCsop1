using AuthUserApi.Models;
using AuthUserApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthUserApi.Controllers
{
    [Route("userrole")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly AuthuserContext _context;

        public UserRoleController(AuthuserContext context)
        {
            _context = context;
        }

        [HttpPost("assignRole")]
        public async Task<IActionResult> AssignRole([FromBody] NewUserRoleDto newUserRoleDto)
        {
            try
            {
                var user = await _context.Users.Include(u => u.Roles)
                    .SingleOrDefaultAsync(u => u.Id == newUserRoleDto.UserId);

                var role = await _context.Roles.FindAsync(newUserRoleDto.RoleId);

                if (user is null || role is null)
                {
                    return NotFound(new {message = "Hibás user vagy role Id"});
                }

                bool isCreated = user.Roles.Any(r => r.Id ==newUserRoleDto.RoleId);

                if (isCreated)
                {
                    return Conflict(new { message = "Ilyen szerepköre már van a usernek" });
                }

                user.Roles.Add(role);
                await _context.SaveChangesAsync();

                return StatusCode(201, new {message = "Sikeres összerendelés"});
            }
            catch (Exception exception)
            {
                return BadRequest(exception.Message);
            }
        }
    }
}
