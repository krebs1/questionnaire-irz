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
            if(startWalkthroughDto == null) return BadRequest("Данные не должны равняться null");

            var checkUser = await _userManager.FindByIdAsync(startWalkthroughDto.UserId);
            if(checkUser == null)
                return NotFound($"Пользователь с id '{startWalkthroughDto.UserId}' не найден");

            var checkQuestionnaire =
                _repository.Questionnaire.GetQuestionnaireById(startWalkthroughDto.QuestionnaireId);
            if(checkQuestionnaire == null)
                return NotFound($"Анкета с id '{startWalkthroughDto.QuestionnaireId}' не найдена");
            
            var walkthroughEntity = _mapper.Map<Walkthrough>(startWalkthroughDto);
            
            walkthroughEntity.WalkthroughStart = DateTime.Now.ToUniversalTime();
            walkthroughEntity.WalkthroughEnd = null;
            
            _repository.Walkthrough.CreateWalkthrough(walkthroughEntity);
            //_repository.Save();
          
            return Created(nameof(Create), walkthroughEntity);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Внутрення ошибка сервера");
        }
    }
    
    [HttpPost("end")]
    public async Task<IActionResult> Create(Guid id)
    {
        try
        {
            if(id == null) return BadRequest("Поле \"id\" не должно быть пустым");

            var checkWalkthrough = _repository.Walkthrough.GetWalkthroughById(id);
            if(checkWalkthrough == null)
                return NotFound($"Прохождение с id '{id}' не найдено");

            checkWalkthrough.WalkthroughEnd = DateTime.Now.ToUniversalTime();
            
            _repository.Walkthrough.UpdateWalkthrough(checkWalkthrough);
            //_repository.Save();
          
            return Ok(checkWalkthrough);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Внутрення ошибка сервера");
        }
    }
    
    [HttpPost("answer-free")]
    public async Task<IActionResult> AnswerFree(AnswerFreeQuestionDTO answerFreeQuestionDto)
    {
        try
        {
            if(answerFreeQuestionDto == null) return BadRequest("Данные не должны равняться null");

            var checkWalkthrough = _repository.Walkthrough.GetWalkthroughById(answerFreeQuestionDto.WalkthroughId);
            if(checkWalkthrough == null)
                return NotFound($"Прохождение с id '{answerFreeQuestionDto.WalkthroughId}' не найдено");

            if (checkWalkthrough.WalkthroughEnd != null)
                return BadRequest($"Прохождение с id '{answerFreeQuestionDto.WalkthroughId}' уже завершено");

            var checkQuestion = _repository.Question.GetQuestionById(answerFreeQuestionDto.QuestionId);
            if(checkQuestion == null)
                return NotFound($"Вопрос с id '{answerFreeQuestionDto.QuestionId}' не найден");
            
            if(checkQuestion.QuestionType != "FREE")
                return BadRequest($"Свободный ответ доступен только для вопроса с типом 'FREE'");

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
            //_repository.Save();
            _repository.TextAnswer.CreateTextAnswer(textAnswerEntity);
            //_repository.Save();

            return Ok(merged);
        }
        catch (Exception e)
        {
            return StatusCode(500, $"Внутрення ошибка сервера");
        }
    }
    
    [HttpPost("answer-select")]
    public async Task<IActionResult> AnswerSelect(AnswerSelectQuestionDTO answerSelectQuestionDto)
    {
        try
        {
            if(answerSelectQuestionDto == null) return BadRequest("Данные не должны равняться null");

            var checkWalkthrough = _repository.Walkthrough.GetWalkthroughById(answerSelectQuestionDto.WalkthroughId);
            if(checkWalkthrough == null)
                return NotFound($"Прохождение с id '{answerSelectQuestionDto.WalkthroughId}' не найдено");

            if (checkWalkthrough.WalkthroughEnd != null)
                return BadRequest($"Прохождение с id '{answerSelectQuestionDto.WalkthroughId}' уже завершено");

            var checkQuestion = _repository.Question.GetQuestionById(answerSelectQuestionDto.QuestionId);
            if(checkQuestion == null)
                return NotFound($"Вопрос с id '{answerSelectQuestionDto.QuestionId}' не найден");
            
            if(checkQuestion.QuestionType != "SELECT")
                return BadRequest($"Выбор варинта ответа доступен только для вопроса с типом 'SELECT'");

            var createWalkthroughQuestionDTO = new CreateWalkthroughQuestionDTO
            (
                answerSelectQuestionDto.WalkthroughId,
                answerSelectQuestionDto.QuestionId
            );
            var walkthroughQuestionEntity = _mapper.Map<WalkthroughQuestion>(createWalkthroughQuestionDTO);
            walkthroughQuestionEntity.WalkthroughQuestionId = Guid.NewGuid();
            
            _repository.WalkthroughQuestion.CreateWalkthroughQuestion(walkthroughQuestionEntity);
            //_repository.Save();

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
            //_repository.Save();

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
            return StatusCode(500, $"Внутрення ошибка сервера");
        }
    }
}