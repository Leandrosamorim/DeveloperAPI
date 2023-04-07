using Domain.ContactNS;
using Domain.ProgrammingStackNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DeveloperNS
{
    public class DeveloperDTO
    {
        public Guid UId { get; set; }
        public string Name { get; set; }
        public int StackId { get; set; }
        public string StackName { get; set; }
    }
}
