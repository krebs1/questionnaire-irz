using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using questionnaire.DTO;
using AutoMapper;
    
namespace questionnaire.Controllers;

[Route("api/[controller]")]
[ApiController]
public class QuestionnaireController : ControllerBase
{
    private IRepositoryWrapper _repository;
    
    private IMapper _mapper;
    
    public QuestionnaireController (IRepositoryWrapper repository, IMapper mapper) 
    {
        _repository = repository;
        _mapper = mapper;
    }
    
    [HttpPost()]
    public async Task<IActionResult> Create(CreateQuestionnaireDTO createQuestionnaireDto = null)
    {
        try
        {
            if(createQuestionnaireDto == null) return BadRequest("Data should not be empty");

            var questionnaireEntity = _mapper.Map<Questionnaire>(createQuestionnaireDto);
            
          _repository.Questionnaire.CreateQuestionnaire(questionnaireEntity);
          _repository.Save();
          
          return Ok(questionnaireEntity);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e}");
        }
    }
    [HttpPut()]
    public async Task<IActionResult> Update(UpdateQuestionnaireDTO updateQuestionnaireDto = null)
    {
        try
        {
            if(updateQuestionnaireDto == null) return BadRequest("Data should not be empty");

            var questionnaireEntity = _mapper.Map<Questionnaire>(updateQuestionnaireDto);
            
            _repository.Questionnaire.UpdateQuestionnaire(questionnaireEntity);
            _repository.Save();
          
            return Ok(questionnaireEntity);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e}");
        }
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(Guid id)
    {
        try
        {
            var checkQuestionnaire = _repository.Questionnaire.GetQuestionnaireById(id);
            if (checkQuestionnaire == null) return NotFound($"The questionnaire with id '{id}' was not found");
            _repository.Questionnaire.DeleteQuestionnaire(checkQuestionnaire);
            _repository.Save();
          
            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e}");
        }
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            if(id == null) return BadRequest("The \"id\" field should not be empty");

         var result = _repository.Questionnaire.GetQuestionnaireById(id);
         
            return Ok(result);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e}");
        }
    }
    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        try
        {
            return Ok(_repository.Questionnaire.GetAllQuestionnaire());
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e}");
        }
    }
}