using Entities;
using questionnaire.Contracts;
using File = Entities.Models.File;

namespace Repository;

public class FileRepository : RepositoryBase<File>, IFileRepository
{
    public FileRepository(RepositoryContext repositoryContext)
        : base(repositoryContext) {}

    public void CreateFile(File file)
    {
        Create(file);
    }
}