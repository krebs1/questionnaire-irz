using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using questionnaire.Contracts;
using questionnaire.DTO;

namespace questionnaire.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private IRepositoryWrapper _repository;

        private IQuestionService _questionService;
            
        private IMapper _mapper;
    
        public QuestionController (IRepositoryWrapper repository, IMapper mapper, IQuestionService questionService) 
        {
            _repository = repository;
            _mapper = mapper;
            _questionService = questionService;
        }
        
        [HttpPost()]
        public async Task<IActionResult> Create(CreateQuestionDTO createQuestionDto = null)
        {
            try
            {
                if(createQuestionDto == null)
                    return BadRequest("Data should not be empty");
                
                if(createQuestionDto.QuestionType != "SELECT" && createQuestionDto.QuestionType != "FREE")
                    return BadRequest("The \"type\" field must have the value \"SELECT\" or \"FREE\"");

                var checkQuestionnaire =
                    _repository.Questionnaire.GetQuestionnaireById(createQuestionDto.QuestionnaireId);
                if(checkQuestionnaire == null)
                    return NotFound($"The questionnaire with id '{createQuestionDto.QuestionnaireId}' was not found");

                var result = _questionService.Create(createQuestionDto);

                return Ok(result);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e}");
            }
        }
        
        [HttpPut()]
        public async Task<IActionResult> Update(UpdateQuestionDTO updateQuestionDto = null)
        {
            try
            {
                if(updateQuestionDto == null)
                    return BadRequest("Data should not be empty");

                var checkQuestion = _repository.Question.GetQuestionById(updateQuestionDto.QuestionId);
                if(checkQuestion == null)
                    return NotFound($"The question with id '{updateQuestionDto.QuestionId}' was not found");
                
                var result = _questionService.Update(updateQuestionDto);
                
                return Ok(result);
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
                var checkQuestion = _repository.Question.GetQuestionById(id);
                if (checkQuestion == null) return NotFound($"The question with id '{id}' was not found");
                _questionService.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e}");
            }
        }
}