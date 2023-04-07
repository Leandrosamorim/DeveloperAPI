using Domain.ProgrammingStackNS;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DeveloperNS.Query
{
    public class DeveloperQuery
    {
        public IEnumerable<Guid>? UId { get; set; }
        public int? StackId { get; set; }
    }
}
