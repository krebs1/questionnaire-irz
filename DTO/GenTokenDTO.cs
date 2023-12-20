namespace questionnaire.DTO;

public class GenTokenDTO
{
    public string UserName { get; set; }

    public GenTokenDTO(string userName)
    {
        UserName = userName;
    }
}