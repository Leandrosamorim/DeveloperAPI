using Domain.ContactNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DeveloperNS
{
    public class DeveloperContact
    {
        public Guid UId { get; set; }
        public string Name { get; set; }
        public Contact Contact { get; set; }
        public string StackName { get; set; }
    }
}
