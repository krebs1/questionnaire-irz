using File = Entities.Models.File;

namespace questionnaire.Contracts;

public interface IFileRepository
{
    void CreateFile(File file);
}