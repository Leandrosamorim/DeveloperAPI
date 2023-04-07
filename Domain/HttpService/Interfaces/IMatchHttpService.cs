using Domain.OrganizationNS;

namespace Domain.HttpService.Interfaces
{
    public interface IMatchHttpService
    {
        public Task<IEnumerable<OrganizationDTO>> GetOrganizationsToMatch(Guid developerUId);
        public Task<IEnumerable<Organization>> GetMyMatches(Guid uids);
        public Task<bool> MatchOrganization(Guid developerUId, Guid organizationUId);
    }
}
