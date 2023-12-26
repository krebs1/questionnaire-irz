using Microsoft.AspNetCore.Mvc;
using questionnaire.DTO;
using questionnaire.Contracts;

namespace questionnaire.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionnaireController : ControllerBase
{
    private IQuestionnaireService _questionnaireService;
    public QuestionnaireController (IQuestionnaireService questionnaireService)
    {
        _questionnaireService = questionnaireService;
    }
    
    [HttpPost()]
    public async Task<IActionResult> Create(CreateQuestionnaireDTO createQuestionnaireDto = null)
    {
        try
        {
            if(createQuestionnaireDto == null) return BadRequest("Данные не должны равняться null");

            var result = _questionnaireService.Create(createQuestionnaireDto);
          
            return Created(nameof(Create), result);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Внутрення ошибка сервера");
        }
    }
    
    [HttpPut()]
    public async Task<IActionResult> Update(UpdateQuestionnaireDTO updateQuestionnaireDto = null)
    {
        try
        {
            if(updateQuestionnaireDto == null) return BadRequest("Данные не должны равняться null");

            var result = _questionnaireService.Update(updateQuestionnaireDto);
          
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Внутрення ошибка сервера");
        }
    }
    
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            if(id == null) return BadRequest("Поле \"id\" не должно быть пустым");
            
            _questionnaireService.Delete(id);
          
            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Внутрення ошибка сервера");
        }
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            if(id == null) return BadRequest("Поле \"id\" не должно быть пустым");

            var result = _questionnaireService.GetById(id);
         
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Внутрення ошибка сервера");
        }
    }
    
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            return Ok(_questionnaireService.GetAll());
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Внутрення ошибка сервера");
        }
    }
}