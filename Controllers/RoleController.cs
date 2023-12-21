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
    public RoleController (IRepositoryWrapper repository, UserManager<IdentityUser> userManager) 
    {
        _repository = repository;
        _userManager = userManager;
    }
    
    [HttpPost("create")]
    public async Task<IActionResult> Create(string roleName = null)
    {
        if (roleName == null || string.IsNullOrEmpty(roleName)) return BadRequest("The \"role name\" field should not be empty");

        var checkRole = _repository.AspNetRole.GetAspNetRoleByName(roleName);
        if (checkRole != null) return BadRequest($"Role with name '{roleName}' already exists");
        
        _repository.AspNetRole.CreateAspNetRole(new IdentityRole(roleName));
        _repository.Save();
        return Ok();
    }
    
    [HttpPost("add-role")]
    public async Task<IActionResult> AddRoleToUser(AddRoleToUserDTO addRoleToUserDto)
    {
        var checkRole = _repository.AspNetRole.GetAspNetRoleById(addRoleToUserDto.RoleId);
        if (checkRole == null) return NotFound($"The role with id '{addRoleToUserDto.RoleId}' was not found");

        var checkUser = await _userManager.FindByIdAsync(addRoleToUserDto.UserId.ToString());
        if (checkUser == null) return NotFound($"The user with id '{addRoleToUserDto.UserId}' was not found");

        _userManager.AddToRoleAsync(checkUser, checkRole.Name);
        return Ok();
    }
    
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            if(id == null) return BadRequest("The \"id\" field should not be empty");
            
            var checkRole = _repository.AspNetRole.GetAspNetRoleById(id);
            if (checkRole == null) return NotFound($"The role with id '{id}' was not found");
            
            _repository.AspNetRole.DeleteAspNetRole(checkRole);
            _repository.Save();
            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(500, "Internal server error");
        }
    }
}