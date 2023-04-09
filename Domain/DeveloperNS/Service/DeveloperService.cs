using Domain.DeveloperNS.Command;
using Domain.DeveloperNS.Interface;
using Domain.DeveloperNS.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DeveloperNS.Service
{
    public class DeveloperService : IDeveloperService
    {

        private readonly IDeveloperRepository _developerRepository;

        public DeveloperService(IDeveloperRepository developerRepository)
        {
            _developerRepository = developerRepository;
        }

        public Task<Developer> Create(RegisterDeveloperCmd developer)
        {
            var dev = DeveloperFactory.CreateDeveloper(developer.Name, developer.Contact, developer.StackId, developer.Login, developer.Password);
            return _developerRepository.Create(dev);
        }

        public Task Delete(Guid uid)
        {
            return _developerRepository.Delete(uid);
        }

        public Task<List<DeveloperDTO>> Get(DeveloperQuery query)
        {
            return _developerRepository.Get(query);
        }

        public async Task<List<DeveloperDTO>> GetAll()
        {
            return await _developerRepository.GetAll();
        }

        public async Task<List<DeveloperContact>> GetWithContact(DeveloperQuery query)
        {
            return await _developerRepository.GetWithContact(query);
        }

        public Task<Developer> Login(DeveloperLoginCmd cmd)
        {
            return _developerRepository.Login(cmd.Login, cmd.Password);
        }

        public Task<Developer> Update(UpdateDeveloperCmd developer)
        {
            var dev = new Developer() { UId = developer.UId, Name = developer.Name, Contact = developer.Contact, StackId= developer.StackId, Login = developer.Login, Password = developer.Password };
            return _developerRepository.Update(dev);
        }
    }
}
