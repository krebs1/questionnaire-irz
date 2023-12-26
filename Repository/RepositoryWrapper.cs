using Contracts;
using Entities;
using questionnaire.Contracts;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper 
    { 
        private RepositoryContext _repoContext;

        private IQuestionnaireRepository _questionnaire;
        private IQuestionRepository _question;
        private IVariantRepository _variant;
        private IWalkthroughRepository _walkthrough;
        private IWalkthroughQuestionRepository _walkthroughQuestion;
        private ITextAnswerRepository _textAnswer;
        private ISelectedVariantRepository _selectedVariant;
        private IFileRepository _file;
        private IAspNetRoleRepository _aspNetRole;
        private IAspNetUserRepository _aspNetUser;

        public IQuestionnaireRepository Questionnaire
        {
            get
            {
                if (_questionnaire == null)
                    _questionnaire = new QuestionnaireRepository(_repoContext); 
                return _questionnaire;
            }
        }
        
        public IQuestionRepository Question
        {
            get
            {
                if (_question == null)
                    _question = new QuestionRepository(_repoContext);
                return _question;
            }
        }
        
        public IVariantRepository Variant
        {
            get
            {
                if (_variant == null)
                    _variant = new VariantRepository(_repoContext);
                return _variant;
            }
        }
        
        public IWalkthroughRepository Walkthrough
        {
            get
            {
                if (_walkthrough == null)
                    _walkthrough = new WalkthroughRepository(_repoContext);
                return _walkthrough;
            }
        }
        
        public IWalkthroughQuestionRepository WalkthroughQuestion
        {
            get
            {
                if (_walkthroughQuestion == null)
                    _walkthroughQuestion = new WalkthroughQuestionRepository(_repoContext);
                return _walkthroughQuestion;
            }
        }
        
        public ITextAnswerRepository TextAnswer
        {
            get
            {
                if (_textAnswer == null)
                    _textAnswer = new TextAnswerRepository(_repoContext);
                return _textAnswer;
            }
        }
        
        public ISelectedVariantRepository SelectedVariant
        {
            get
            {
                if (_selectedVariant == null)
                    _selectedVariant = new SelectedVariantRepository(_repoContext);
                return _selectedVariant;
            }
        }

        public IFileRepository File
        {
            get
            {
                if (_file == null)
                    _file = new FileRepository(_repoContext);
                return _file;
            }
        }
        
        public IAspNetRoleRepository AspNetRole
        {
            get
            {
                if (_aspNetRole == null)
                    _aspNetRole = new AspNetRoleRepository(_repoContext);
                return _aspNetRole;
            }
        }

        public IAspNetUserRepository AspNetUser
        {
            get
            {
                if (_aspNetUser == null)
                    _aspNetUser = new AspNetUserRepository(_repoContext);
                return _aspNetUser;
            }
        }
        
        public RepositoryWrapper(RepositoryContext repositoryContext) 
        { 
            _repoContext = repositoryContext; 
        } 
        
        public void Save() 
        {
            _repoContext.SaveChanges();
        } 
    }
}
