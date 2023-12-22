using Entities.Models;

namespace Contracts;

public interface ITextAnswerRepository
{
    void CreateTextAnswer(TextAnswer textAnswer);
}