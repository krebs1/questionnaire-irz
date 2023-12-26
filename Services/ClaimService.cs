using System.Security.Claims;
using questionnaire.Contracts;

namespace questionnaire.Services;

public class ClaimService : IClaimService
{
    public ClaimService()
    {
        
    }
    
    public IEnumerable<Claim> GenClaims(string type, IList<string> values)
    {
        return values.Select(value => new Claim(type, value)).ToList();
    }
}