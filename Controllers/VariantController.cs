using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Mvc;
using questionnaire.DTO;

namespace questionnaire.Controllers;

[Route("api/[controller]")]
[ApiController]
public class VariantController : ControllerBase
{
    private IRepositoryWrapper _repository;
    
    private IMapper _mapper;
    
    public VariantController (IRepositoryWrapper repository, IMapper mapper) 
    {
        _repository = repository;
        _mapper = mapper;
    }
    [HttpPost()]
    public async Task<IActionResult> Create(CreateVariantDTO createVariantDTO = null)
    {
        try
        {
            if(createVariantDTO == null)
                return BadRequest("Данные не должны равняться null");

            var checkQuestion = _repository.Question.GetQuestionById(createVariantDTO.QuestionId);
            if(checkQuestion == null)
                return NotFound($"Вопрос с id '{createVariantDTO.QuestionId}' не найден");

            if (checkQuestion.QuestionType != "SELECT")
                return BadRequest("Вариант может быть добавлен только для вопроса с типом \"SELECT\"");

            var variantEntity = _mapper.Map<Variant>(createVariantDTO);
            
            _repository.Variant.CreateVariant(variantEntity);

            return Created(nameof(Create), variantEntity);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Внутрення ошибка сервера");
        }
    }
    [HttpPut()]
    public async Task<IActionResult> Update(UpdateVariantDTO updateVariantDTO = null)
    {
        try
        {
            if(updateVariantDTO == null)
                return BadRequest("Данные не должны равняться null");

            var checkVariant = _repository.Variant.GetVariantById(updateVariantDTO.VariantId);
            if(checkVariant == null)
                return NotFound($"Вариант с id '{updateVariantDTO.VariantId}' не найден");

            checkVariant.VariantText = updateVariantDTO.VariantText;
            
            _repository.Variant.UpdateVariant(checkVariant);

            return Ok(checkVariant);
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
            var checkVariant = _repository.Variant.GetVariantById(id);
            if (checkVariant == null) return NotFound($"Вариант с id '{id}' не найден");
            
            _repository.Variant.DeleteVariant(checkVariant);

            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Внутрення ошибка сервера");
        }
    }
}