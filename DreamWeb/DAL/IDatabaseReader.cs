using DreamWeb.DAL.Entities;

namespace DreamWeb.DAL
{
    public interface IDatabaseReader
    {
        public Task<UserAccount> GetUserAccountByUsernameAsync(string username);
        public Task AddDreamAsync(Dream dream, string username);
        public Task DeleteDreamAsync(string username, string dreamId);
        public Task<Dream> GetDreamByIdAsync(string id);
        public Task<bool> IsEmailTaken(string email);
        public Task<bool> IsUsernameTaken(string username);
    }
}
