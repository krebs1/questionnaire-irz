using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace questionnaire.Controllers;

public class TestController
{
    [HttpPost("authorize")]
    [Authorize]
    public string Authorize()
    {
        return "Authorize endpoint";
    }
    
    [HttpPost("admin")]
    [Authorize(Roles = "admin")]
    public string Admin()
    {
        return "Admin endpoint";
    }
    
    [HttpPost("user")]
    [Authorize(Roles = "user")]
    public string User()
    {
        return "User endpoint";
    }
    
    [HttpPost("admin-user")]
    [Authorize(Roles = "admin,user")]
    public string AdminUser()
    {
        return "Admin and user endpoint";
    }
}