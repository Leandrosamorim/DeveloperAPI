using Domain.DeveloperNS.Command;
using Domain.DeveloperNS.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DeveloperNS.Interface
{
    public interface IDeveloperService
    {
        public Task<Developer> Create(RegisterDeveloperCmd developer);
        public Task<Developer> Update(UpdateDeveloperCmd developer);
        public Task Delete(Guid uid);
        public Task<List<DeveloperDTO>> Get(DeveloperQuery query);
        public Task<List<DeveloperContact>> GetWithContact(DeveloperQuery query);
        public Task<List<DeveloperDTO>> GetAll();
        public Task<Developer> Login(DeveloperLoginCmd cmd);
    }
}
