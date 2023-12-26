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
            if(roleName == null) return BadRequest("Поле \"role name\" не должно быть пустым");
            
            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (!result.Succeeded) throw new Exception("Создание роли завершилось неудачно");
            
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Внутрення ошибка сервера");
        }
    }
    
    [HttpPost("add-role")]
    public async Task<IActionResult> AddRoleToUser(AddRoleToUserDTO addRoleToUserDto = null)
    {
        try
        {
            if(addRoleToUserDto == null) return BadRequest("Данные не должны быть пустыми");
            
            var checkRole = await _roleManager.FindByNameAsync(addRoleToUserDto.RoleName);
            if (checkRole == null) return NotFound($"Роль с именем '{addRoleToUserDto.RoleName}' не найдена");
        
            var checkUser = await _userManager.FindByIdAsync(addRoleToUserDto.UserId.ToString());
            if (checkUser == null) return NotFound($"Пользователь с id '{addRoleToUserDto.UserId}' не найден");

            var isInRole = await _userManager.IsInRoleAsync(checkUser, checkRole.Name);
            if (isInRole) return Ok("Пользователь уже находится в этой роли");

            var result = await _userManager.AddToRoleAsync(checkUser, checkRole.Name);
            if (!result.Succeeded) throw new Exception("Добавление роли завершилось неудачно");
        
            return Ok();
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Внутрення ошибка сервера");
        }
    }
    
    [HttpDelete("delete")]
    public async Task<IActionResult> Delete(string name = null)
    {
        try
        {
            if(name == null) return BadRequest("Поле \"id\" не должно быть пустым");

            var checkRole = await _roleManager.FindByNameAsync(name);
            if (checkRole == null) return NotFound($"Роль с именем '{name}' не найдена");

            var result = await _roleManager.DeleteAsync(checkRole);
            if (!result.Succeeded) throw new Exception("Удаление роли завершилось неудачно");
            
            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(500, "Внутрення ошибка сервера");
        }
    }
}