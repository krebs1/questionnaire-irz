using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using questionnaire.DTO;
using Microsoft.AspNetCore.Mvc;
namespace questionnaire.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private IRepositoryWrapper _repository;

    UserManager<IdentityUser> _userManager;
    private RoleManager<IdentityRole> _roleManager;
    public RoleController (IRepositoryWrapper repository, UserManager<IdentityUser> userManager, RoleManager<IdentityRole> roleManager) 
    {
        _repository = repository;
        _userManager = userManager;
        _roleManager = roleManager;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> Create(string roleName = null)
    {
        try
        {
            if(roleName == null) return BadRequest("The \"role name\" field should not be empty");
            
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (!result.Succeeded) throw new Exception("Role creating is not success");
            
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e}");
        }
    }
    
    [HttpPost("add-role")]
    public async Task<IActionResult> AddRoleToUser(AddRoleToUserDTO addRoleToUserDto = null)
    {
        try
        {
            if(addRoleToUserDto == null) return BadRequest("Data should not be empty");
            
            var checkRole = await _roleManager.FindByNameAsync(addRoleToUserDto.RoleName);
            if (checkRole == null) return NotFound($"The role with name '{addRoleToUserDto.RoleName}' was not found");
        
            var checkUser = await _userManager.FindByIdAsync(addRoleToUserDto.UserId.ToString());
            if (checkUser == null) return NotFound($"The user with id '{addRoleToUserDto.UserId}' was not found");

            var isInRole = await _userManager.IsInRoleAsync(checkUser, checkRole.Name);
            if (isInRole) return Ok("User already in role");

            var result = await _userManager.AddToRoleAsync(checkUser, checkRole.Name);
            if (!result.Succeeded) throw new Exception("Adding role to user is not success");
        
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e}");
        }
    }
    
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(string name = null)
    {
        try
        {
            if(name == null) return BadRequest("The \"id\" field should not be empty");

            var checkRole = await _roleManager.FindByNameAsync(name);
            if (checkRole == null) return NotFound($"The role with name '{name}' was not found");

            var result = await _roleManager.DeleteAsync(checkRole);
            if (!result.Succeeded) throw new Exception("Role deletion is not success");
            
            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(500, "Internal server error");
        }
    }
}