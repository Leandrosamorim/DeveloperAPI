using Domain.ContactNS;

namespace Domain.OrganizationNS
{
    public class Organization
    {
        public Guid UId { get; set; }
        public string Name { get; set; }
        public Contact Contact { get; set; }
        public string Description { get; set; }
    }
}
