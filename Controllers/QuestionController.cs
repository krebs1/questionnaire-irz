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
        private IQuestionService _questionService;
        private IQuestionnaireService _questionnaireService;

        public QuestionController (IQuestionService questionService, IQuestionnaireService questionnaireService) 
        {
            _questionService = questionService;
            _questionnaireService = questionnaireService;
        }
        
        [HttpPost()]
        public async Task<IActionResult> Create(CreateQuestionDTO createQuestionDto = null)
        {
            try
            {
                if(createQuestionDto == null)
                    return BadRequest("Данные не должны равняться null");
                
                if(createQuestionDto.QuestionType != "SELECT" && createQuestionDto.QuestionType != "FREE")
                    return BadRequest("Поле \"type\" должно иметь значение \"SELECT\" или \"FREE\"");

                var checkQuestionnaire = _questionnaireService.GetById(createQuestionDto.QuestionnaireId);
                if(checkQuestionnaire == null)
                    return NotFound($"Анкета с id '{createQuestionDto.QuestionnaireId}' не найдена");

                var result = _questionService.Create(createQuestionDto);

                return Created(nameof(Create),result);
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Внутрення ошибка сервера");
            }
        }
        
        [HttpPut()]
        public async Task<IActionResult> Update(UpdateQuestionDTO updateQuestionDto = null)
        {
            try
            {
                if(updateQuestionDto == null)
                    return BadRequest("Данные не должны равняться null");

                var checkQuestion = _questionService.GetById(updateQuestionDto.QuestionId);
                if(checkQuestion == null)
                    return NotFound($"Вопрос с id '{updateQuestionDto.QuestionId}' не найден");
                
                var result = _questionService.Update(updateQuestionDto);
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
                var checkQuestion = _questionService.GetById(id);
                if(checkQuestion == null)
                    return NotFound($"Вопрос с id '{id}' не найден");
                
                _questionService.Delete(id);
                return NoContent();
            }
            catch (Exception e)
            {
                return StatusCode(500, $"Внутрення ошибка сервера");
            }
        }
}