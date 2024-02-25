using Data.Context;
using Domain.DeveloperNS;
using Domain.DeveloperNS.Interface;
using Domain.DeveloperNS.Query;
using Microsoft.EntityFrameworkCore;


namespace Data.Repositories
{
    public class DeveloperRepository : IDeveloperRepository
    {
        private readonly DeveloperDBContext _context;

        public DeveloperRepository(DeveloperDBContext context)
        {
            _context = context;
        }


        public async Task<Developer> Create(Developer developer)
        {
            await _context.Developer.AddAsync(developer);
            _context.SaveChangesAsync();
            return developer;
        }

        public async Task Delete(Guid uid)
        {
            _context.Developer.Remove(new Developer() { UId = uid });
            await _context.SaveChangesAsync();
        }

        public async Task<List<DeveloperDTO>> Get(DeveloperQuery query)
        {
            return await _context.Developer
                .Where(x => (query.UId != null && query.UId.Contains(x.UId)) || (x.StackId == query.StackId && query.StackId > 0))
                .Select(x => new DeveloperDTO() { UId = x.UId, Name = x.Name, StackName = x.Stack.StackName, StackId = x.StackId })
                .ToListAsync();
        }

        public async Task<List<DeveloperDTO>> GetAll()
        {
            return await _context.Developer.Select(x => new DeveloperDTO() { UId = x.UId, Name = x.Name, StackName = x.Stack.StackName, StackId = x.StackId }).ToListAsync();
        }

        public async Task<List<DeveloperContact>> GetWithContact(DeveloperQuery query)
        {
            return await _context.Developer
                .Where(x => (x.UId != Guid.Empty && query.UId.Contains(x.UId)) || (x.StackId == query.StackId && query.StackId > 0))
                .Include(x => x.Contact)
                .Include(x => x.Stack)
                .Select(x => new DeveloperContact() { UId = x.UId, Name = x.Name, StackName = x.Stack.StackName, Contact = x.Contact })
                .ToListAsync();
        }

        public async Task<Developer> Login(string userName, string password)
        {
            var dev = _context.Developer.FirstOrDefault(x => x.Login == userName);
            if (dev != null)
                return BCrypt.Net.BCrypt.Verify(password, dev.Password) ? dev : null ;
                
            return dev;
        }

        public async Task<Developer> Update(Developer developer)
        {
            _context.Developer.Update(developer);
            await _context.SaveChangesAsync();
            return developer;
        }
    }
}
