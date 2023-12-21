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
            if(createVariantDTO == null) return BadRequest("Data should not be empty");

            var variantEntity = _mapper.Map<Variant>(createVariantDTO);
            
            _repository.Variant.CreateVariant(variantEntity);
            _repository.Save();
          
            return Ok(variantEntity);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e}");
        }
    }
    [HttpPut()]
    public async Task<IActionResult> Update(UpdateVariantDTO updateVariantDTO = null)
    {
        try
        {
            if(updateVariantDTO == null) return BadRequest("Data should not be empty");

            var updateEntity = _mapper.Map<Variant>(updateVariantDTO);
            
            _repository.Variant.UpdateVariant(updateEntity);
            _repository.Save();
          
            return Ok(updateEntity);
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
            var checkVariant = _repository.Variant.GetVariantById(id);
            if (checkVariant == null) return NotFound($"The questionnaire with id '{id}' was not found");
            _repository.Variant.DeleteVariant(checkVariant);
            _repository.Save();
          
            return NoContent();
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e}");
        }
    }
}