using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using questionnaire.DTO;

namespace questionnaire.Controllers;

    [Route("api/[controller]")]
    [ApiController]
    public class QuestionController : ControllerBase
    {
        private IRepositoryWrapper _repository;
    
        private IMapper _mapper;
    
        public QuestionController (IRepositoryWrapper repository, IMapper mapper) 
        {
            _repository = repository;
            _mapper = mapper;
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

                var questionEntity = _mapper.Map<Question>(createQuestionDto);
            
                _repository.Question.CreateQuestion(questionEntity);
                _repository.Save();
          
                return Ok(questionEntity);
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

                checkQuestion.QuestionText = updateQuestionDto.QuestionText;
            
                _repository.Question.UpdateQuestion(checkQuestion);
                _repository.Save();
          
                return Ok(checkQuestion);
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
                
                _repository.Question.DeleteQuestion(checkQuestion);
                _repository.Save();
          
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Internal server error: {e}");
            }
        }
}