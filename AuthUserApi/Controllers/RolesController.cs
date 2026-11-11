using AuthUserApi.Models;
using AuthUserApi.Models.DTOs;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AuthUserApi.Controllers
{
    [Route("roles")]
    [ApiController]
    public class RolesController : ControllerBase
    {
        private readonly AuthuserContext _context;

        public RolesController(AuthuserContext context)
        {
            _context = context;
        }

        [HttpPost("newRole")]
        public async Task<IActionResult> NewRole([FromBody] NewRoleDto newRoleDto)
        {
            var role = new Role
            {
                Id = Guid.NewGuid(),
                Name = newRoleDto.Name,
              
            };

            if (role is not null)
            {
                await _context.Roles.AddAsync(role);
                await _context.SaveChangesAsync();

                return StatusCode(201, new { message = "Felvéve", result = role });
            }

            return StatusCode(400, new { message = "Hiba", result = role });
        }

        [HttpGet("allRole")]
        public async Task<IActionResult> AllRole()
        {
            var roles = await _context.Roles.ToListAsync();

            if (roles is not null)
            {
                return StatusCode(200, new { message = "Lekérdezve", result = roles });
            }

            return StatusCode(400, new { message = "Hiba", result = roles });
        }

        [HttpGet("byId")]
        public async Task<IActionResult> RoleById([FromQuery] Guid id)
        {
            var role = await _context.Roles.SingleOrDefaultAsync(u => u.Id == id);

            if (role is not null)
            {
                return StatusCode(200, new { message = "Lekérdezve", result = role });
            }

            return StatusCode(404, new { message = "Nincs találat", result = role });
        }

        [HttpPut("updateRole")]
        public async Task<IActionResult> UpdateUser([FromQuery] Guid id, UpdateRoleDto updateRoleDto)
        {
            var role = await _context.Roles.FirstOrDefaultAsync(u => u.Id == id);

            if (role is not null)
            {
                role.Name = updateRoleDto.Name;
             
                _context.Roles.Update(role);
                await _context.SaveChangesAsync();

                return StatusCode(200, new { message = "Frissítve", result = role });
            }

            return StatusCode(404, new { message = "Nincs találat", result = role });
        }

        [HttpDelete("deleteRole")]
        public async Task<IActionResult> DeleteUser([FromQuery] Guid id)
        {
            var role = await _context.Roles.FindAsync(id);

            if (role is not null)
            {
                _context.Roles.Remove(role);
                await _context.SaveChangesAsync();

                return StatusCode(200, new { message = "Tötrölve", result = role.Name });
            }

            return StatusCode(404, new { message = "Nincs találat", result = role });

        }
    }
}
