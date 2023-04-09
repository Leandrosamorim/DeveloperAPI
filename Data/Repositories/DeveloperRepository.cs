using Data.Context;
using Domain.DeveloperNS;
using Domain.DeveloperNS.Interface;
using Domain.DeveloperNS.Query;


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
            _context.SaveChanges();
            return developer;
        }

        public async Task Delete(Guid uid)
        {
            _context.Developer.Remove(new Developer() { UId = uid });
            await _context.SaveChangesAsync();
        }

        public async Task<List<DeveloperDTO>> Get(DeveloperQuery query)
        {
            return _context.Developer
                .Where(x => (query.UId != null && query.UId.Contains(x.UId)) || (x.StackId == query.StackId && query.StackId > 0))
                .Select(x => new DeveloperDTO() { UId = x.UId, Name = x.Name, StackName = x.Stack.StackName, StackId = x.StackId })
                .ToList();
        }

        public async Task<List<DeveloperDTO>> GetAll()
        {
            return _context.Developer.Select(x => new DeveloperDTO() { UId = x.UId, Name = x.Name, StackName = x.Stack.StackName, StackId = x.StackId }).ToList();
        }

        public async Task<List<DeveloperContact>> GetWithContact(DeveloperQuery query)
        {
            return _context.Developer
                .Where(x => (x.UId != Guid.Empty && query.UId.Contains(x.UId)) || (x.StackId == query.StackId && query.StackId > 0))
                .Select(x => new DeveloperContact() { UId = x.UId, Name = x.Name, StackName = x.Stack.StackName, Contact = x.Contact })
                .ToList();
        }

        public async Task<Developer> Login(string userName, string password)
        {
            var dev = _context.Developer.Where(x => x.Login == userName).FirstOrDefault();
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
