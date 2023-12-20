using Contracts;
using questionnaire.DTO;
using Microsoft.AspNetCore.Mvc;
namespace questionnaire.Controllers;

[Route("api/[controller]")]
[ApiController]
public class RoleController : ControllerBase
{
    private IRepositoryWrapper _repository;
    
    public RoleController (IRepositoryWrapper repository) 
    {
        _repository = repository;
    }
    
    [HttpPost("CreateController")]
    public async Task<IActionResult> Create(RegisterDTO user)
    {
        _repository.
        _repository.Save();
    }
}