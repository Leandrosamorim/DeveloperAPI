﻿using Domain.ContactNS;
using Domain.ProgrammingStackNS;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DeveloperNS.Command
{
    public class UpdateDeveloperCmd
    {
        public Guid UId { get; set; }
        public string Name { get; set; }
        public Contact Contact { get; set; }
        public int StackId { get; set; }
    }
}
