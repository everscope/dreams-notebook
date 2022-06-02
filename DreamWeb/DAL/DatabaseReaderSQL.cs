using System.Text;
using DreamWeb.DAL.Entities;
using Microsoft.EntityFrameworkCore;

namespace DreamWeb.DAL
{
    public class DatabaseReaderSQL : IDatabaseReader
    {
        private readonly DreamsContext _context;
        private readonly char[] internalIdChars = "abcdefghigklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();

        public DatabaseReaderSQL(DreamsContext context)
        {
            _context = context;
        }

        public Task<UserAccount> GetUserAccountByUsernameAsync(string username)
        {
            return _context.UserAccounts.Include(p => p.Dreams).AsNoTracking()
                .FirstAsync(p => p.UserName == username);
        }

        public async Task AddDreamAsync(Dream dream)
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

        public async Task<Dream> GetDreamByIdAsync(string id)
        {
            return await _context.DreamPublications.FirstAsync(p => p.Id == id);
        }

        private string CreateInternalDreamId()
        {
            Random random = new Random();
            StringBuilder idBuilder = new StringBuilder();
            for (int i = 0; i < 12; i++)
            {
                idBuilder.Append(internalIdChars[random.Next(0, internalIdChars.Length)]);
            }

            string id = idBuilder.ToString();

            if (_context.DreamPublications.Any(p => p.Id == id))
            {
                id = CreateInternalDreamId();
            }

            return id;
        }
    }
}
