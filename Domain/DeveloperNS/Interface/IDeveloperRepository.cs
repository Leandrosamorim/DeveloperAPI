using Domain.DeveloperNS;
using Domain.DeveloperNS.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DeveloperNS.Interface
{
    public interface IDeveloperRepository
    {
        public Task<Developer> Create(Developer developer);
        public Task<Developer> Update(Developer developer);
        public Task Delete(Guid UId);
        public Task<List<DeveloperDTO>> Get(DeveloperQuery query);
        public Task<List<DeveloperContact>> GetWithContact(DeveloperQuery query);
        public Task<List<DeveloperDTO>> GetAll();
        public Task<Developer> Login(string userName, string password);
    }
}
