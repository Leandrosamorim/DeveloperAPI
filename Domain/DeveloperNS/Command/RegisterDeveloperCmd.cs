using Domain.ContactNS;
using Domain.ProgrammingStackNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DeveloperNS.Command
{
    public class RegisterDeveloperCmd
    {
        public string Name { get; set; }
        public Contact Contact { get; set; }
        public int StackId { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
    }
}
