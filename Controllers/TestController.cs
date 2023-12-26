using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace questionnaire.Controllers;

public class TestController
{
    [HttpPost("authorize")]
    [Authorize]
    public string Authorize()
    {
        return "Эндпоинт для проверки авторизации";
    }
    
    [HttpPost("admin")]
    [Authorize(Roles = "admin")]
    public string Admin()
    {
        return "Эндпоинт для проверки на роль admin";
    }
    
    [HttpPost("user")]
    [Authorize(Roles = "user")]
    public string User()
    {
        return "Эндпоинт для проверки на роль user";
    }
    
    [HttpPost("admin-user")]
    [Authorize(Roles = "admin,user")]
    public string AdminUser()
    {
        return "Эндпоинт для проверки на роль admin и (или) user";
    }
}