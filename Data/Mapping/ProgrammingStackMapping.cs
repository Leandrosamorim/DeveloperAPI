using Domain.ProgrammingStackNS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Mapping
{
    public class ProgrammingStackMapping : IEntityTypeConfiguration<ProgrammingStack>
    {
        public void Configure(EntityTypeBuilder<ProgrammingStack> builder)
        {
            builder.HasData(new ProgrammingStack
            {
                StackId = 1,
                StackName = ".NET",
            }, new ProgrammingStack 
            {
                StackId = 2,
                StackName = "JAVA"
            });
        }
    }

}
