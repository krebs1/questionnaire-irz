using System.Security.Claims;

namespace questionnaire.Contracts;

public interface IClaimService
{
    public IEnumerable<Claim> GenClaims(string type, IList<string> values);
}