using DreamWeb.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DreamWeb.DAL
{
    public class DatabaseReaderSQL : IDatabaseReader
    {
        private readonly DreamsContext _context;

        public DatabaseReaderSQL(DreamsContext context)
        {
            _context = context;
        }

        public Task<UserAccount> GetUserAccountByUsernameAsync(string username)
        {
            return _context.UserAccounts.Include(p => p.Dreams).AsNoTracking()
                .FirstAsync(p => p.UserName == username);
        }

        public async Task AddDreamAsync(DreamPublication dream)
        {
            await _context.DreamPublications.AddAsync(dream);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteDreamAsync(string username, string dreamId)
        {
            var dream = _context.Users.First(p => p.UserName == username).Dreams.First(p => p.Id == dreamId);
            _context.DreamPublications.Remove(dream);
            await _context.SaveChangesAsync();
        }

        public async Task<DreamPublication> GetDreamByIdAsync(string id)
        {
            return await _context.DreamPublications.FirstAsync(p => p.Id == id);
        }
    }
}
