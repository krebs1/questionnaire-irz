using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
    public interface IRepositoryWrapper 
    {
        IQuestionnaireRepository Questionnaire { get; }
        IQuestionRepository Question { get; }
        IVariantRepository Variant { get; }
        IWalkthroughRepository Walkthrough { get; }
        IWalkthroughQuestionRepository WalkthroughQuestion { get; }
        ITextAnswerRepository TextAnswer { get; }
        ISelectedVariantRepository SelectedVariant { get; }
        void Save(); 
    }
}
