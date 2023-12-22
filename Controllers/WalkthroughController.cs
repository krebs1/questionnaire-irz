using AutoMapper;
using Contracts;
using Entities.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using questionnaire.DTO;

namespace questionnaire.Controllers;

[Route("api/[controller]")]
[ApiController]
public class WalkthroughController : ControllerBase
{
    private IRepositoryWrapper _repository;
    private UserManager<IdentityUser> _userManager;
    private IMapper _mapper;

    public WalkthroughController(IRepositoryWrapper repository, IMapper mapper, UserManager<IdentityUser> userManager)
    {
        _repository = repository;
        _userManager = userManager;
        _mapper = mapper;
    }
    
    [HttpPost("start")]
    public async Task<IActionResult> Create(StartWalkthroughDTO startWalkthroughDto = null)
    {
        try
        {
            if(startWalkthroughDto == null) return BadRequest("Data should not be empty");

            var checkUser = await _userManager.FindByIdAsync(startWalkthroughDto.UserId);
            if(checkUser == null)
                return NotFound($"The user with id '{startWalkthroughDto.UserId}' was not found");

            var checkQuestionnaire =
                _repository.Questionnaire.GetQuestionnaireById(startWalkthroughDto.QuestionnaireId);
            if(checkQuestionnaire == null)
                return NotFound($"The questionnaire with id '{startWalkthroughDto.QuestionnaireId}' was not found");
            
            var walkthroughEntity = _mapper.Map<Walkthrough>(startWalkthroughDto);
            
            walkthroughEntity.WalkthroughStart = DateTime.Now.ToUniversalTime();
            walkthroughEntity.WalkthroughEnd = null;
            
            _repository.Walkthrough.CreateWalkthrough(walkthroughEntity);
            _repository.Save();
          
            return Ok(walkthroughEntity);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e}");
        }
    }
    
    [HttpPost("end")]
    public async Task<IActionResult> Create(Guid id)
    {
        try
        {
            if(id == null) return BadRequest("The \"id\" field should not be empty");

            var checkWalkthrough = _repository.Walkthrough.GetWalkthroughById(id);
            if(checkWalkthrough == null)
                return NotFound($"The walkthrough with id '{id}' was not found");

            checkWalkthrough.WalkthroughEnd = DateTime.Now.ToUniversalTime();
            
            _repository.Walkthrough.UpdateWalkthrough(checkWalkthrough);
            _repository.Save();
          
            return Ok(checkWalkthrough);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e}");
        }
    }
    
    [HttpPost("answer-free")]
    public async Task<IActionResult> AnswerFree(AnswerFreeQuestionDTO answerFreeQuestionDto)
    {
        try
        {
            if(answerFreeQuestionDto == null) return BadRequest("Data should not be empty");

            var checkWalkthrough = _repository.Walkthrough.GetWalkthroughById(answerFreeQuestionDto.WalkthroughId);
            if(checkWalkthrough == null)
                return NotFound($"The walkthrough with id '{answerFreeQuestionDto.WalkthroughId}' was not found");

            if (checkWalkthrough.WalkthroughEnd != null)
                return BadRequest($"The walkthrough with id '{answerFreeQuestionDto.WalkthroughId}' already ended");

            var checkQuestion = _repository.Question.GetQuestionById(answerFreeQuestionDto.QuestionId);
            if(checkQuestion == null)
                return NotFound($"The question with id '{answerFreeQuestionDto.QuestionId}' was not found");
            
            if(checkQuestion.QuestionType != "FREE")
                return BadRequest($"Free answer available for question with type 'FREE'");

            var createWalkthroughQuestionDTO = new CreateWalkthroughQuestionDTO
            (
                answerFreeQuestionDto.WalkthroughId,
                answerFreeQuestionDto.QuestionId
            );
            var walkthroughQuestionEntity = _mapper.Map<WalkthroughQuestion>(createWalkthroughQuestionDTO);
            walkthroughQuestionEntity.WalkthroughQuestionId = Guid.NewGuid();

            var createTextAnswerDTO = new CreateTextAnswerDTO
            (
                answerFreeQuestionDto.WalkthroughId,
                answerFreeQuestionDto.Answer
            );
            var textAnswerEntity = _mapper.Map<TextAnswer>(createTextAnswerDTO);
            textAnswerEntity.TextAnswerId = Guid.NewGuid();
            textAnswerEntity.WalkthroughQuestionId = walkthroughQuestionEntity.WalkthroughQuestionId;

            var merged = new
            {
                walkthroughQuestionId = walkthroughQuestionEntity.WalkthroughQuestionId,
                walkthroughId = walkthroughQuestionEntity.WalkthroughId,
                questionId = walkthroughQuestionEntity.QuestionId,
                textAnswerId = textAnswerEntity.TextAnswerId,
                textAnswerText = textAnswerEntity.TextAnswerText
            };

            _repository.WalkthroughQuestion.CreateWalkthroughQuestion(walkthroughQuestionEntity);
            _repository.Save();
            _repository.TextAnswer.CreateTextAnswer(textAnswerEntity);
            _repository.Save();

            return Ok(merged);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e}");
        }
    }
    
    [HttpPost("answer-select")]
    public async Task<IActionResult> AnswerSelect(AnswerSelectQuestionDTO answerSelectQuestionDto)
    {
        try
        {
            if(answerSelectQuestionDto == null) return BadRequest("Data should not be empty");

            var checkWalkthrough = _repository.Walkthrough.GetWalkthroughById(answerSelectQuestionDto.WalkthroughId);
            if(checkWalkthrough == null)
                return NotFound($"The walkthrough with id '{answerSelectQuestionDto.WalkthroughId}' was not found");

            if (checkWalkthrough.WalkthroughEnd != null)
                return BadRequest($"The walkthrough with id '{answerSelectQuestionDto.WalkthroughId}' already ended");

            var checkQuestion = _repository.Question.GetQuestionById(answerSelectQuestionDto.QuestionId);
            if(checkQuestion == null)
                return NotFound($"The question with id '{answerSelectQuestionDto.QuestionId}' was not found");
            
            if(checkQuestion.QuestionType != "SELECT")
                return BadRequest($"Select answer available for question with type 'SELECT'");

            var createWalkthroughQuestionDTO = new CreateWalkthroughQuestionDTO
            (
                answerSelectQuestionDto.WalkthroughId,
                answerSelectQuestionDto.QuestionId
            );
            var walkthroughQuestionEntity = _mapper.Map<WalkthroughQuestion>(createWalkthroughQuestionDTO);
            walkthroughQuestionEntity.WalkthroughQuestionId = Guid.NewGuid();
            
            _repository.WalkthroughQuestion.CreateWalkthroughQuestion(walkthroughQuestionEntity);
            _repository.Save();

            var selectedVariantsEntities = new List<SelectedVariant>();
            foreach (var selectedVariant in answerSelectQuestionDto.SelectedVariants)
            {
                var createSelectVariantDTO = new CreateSelectVariantDTO
                (
                    selectedVariant,
                    walkthroughQuestionEntity.WalkthroughQuestionId
                );

                var selectVariantEntity = _mapper.Map<SelectedVariant>(createSelectVariantDTO);
                selectVariantEntity.SelectedVariantId = Guid.NewGuid();
                selectedVariantsEntities.Add(selectVariantEntity);
                _repository.SelectedVariant.CreateSelectedVariant(selectVariantEntity);
            }
            _repository.Save();

            var merged = new
            {
                walkthroughQuestionId = walkthroughQuestionEntity.WalkthroughQuestionId,
                walkthroughId = walkthroughQuestionEntity.WalkthroughId,
                questionId = walkthroughQuestionEntity.QuestionId,
                selectedVariants = selectedVariantsEntities
            };
            
            return Ok(merged);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Internal server error: {e}");
        }
    }
}